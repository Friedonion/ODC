using System.Collections;
using System.Collections.Generic;
using System.Reflection;

using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float Damage;
    public float range;
    public float speed;
    float currentSpeed;
    public Animator anim;
    public GameObject Target;
    
    void Start()
    {
        currentSpeed = speed;
        anim = GetComponent<Animator>();
        anim.SetInteger("Player", 0);
        InvokeRepeating("UpdateTarget", 0f, 0.2f);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(this.gameObject.transform.position, range);
    }
  
    public void SingleAttack()
    {
        Target.GetComponent<GetDamage>().GetD(Damage);
    }

    public void ATTACK()
    {
        anim.SetInteger("Player", 1);
        currentSpeed = 0.0f;
    }

    public void IDLE()
    {
        
        anim.SetInteger("Player", 2);
        currentSpeed = speed;
    }

    void UpdateTarget() // 공격 대상 설정 
    {
        if (Target == null)
        {
            GameObject[] Enemies = GameObject.FindGameObjectsWithTag("Player");
            float shortestDistance = Mathf.Infinity;
            GameObject nearestEnemy = null;
            foreach (GameObject Enemy in Enemies)
            {
                float distance = Vector3.Distance(transform.position, Enemy.transform.position);
                if (distance < shortestDistance)
                {
                    shortestDistance = distance;
                    nearestEnemy = Enemy;
                }
            }

            if (nearestEnemy != null && shortestDistance <= range)
            {
                Target = nearestEnemy;
                ATTACK();
            }
            else
            {
                Target = null;
                IDLE();

            }
        }
    }
    void Update()
    {
        transform.Translate(Vector3.right * currentSpeed * Time.deltaTime);
    }
}
