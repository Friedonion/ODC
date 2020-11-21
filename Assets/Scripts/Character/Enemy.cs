using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Photon.Pun;
using UnityEngine;

public class Enemy : MonoBehaviourPunCallbacks
{
    public float Damage;
    public float range;
    public float speed;
    float currentSpeed;
    public Animator anim;
    public GameObject Target;
    public GameObject Bullet;
    public GameObject trans; 
    
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
                IDLE();
            }
        }
    }

    public void MakeBullet()
    {
        if (Target != null)
        {
            GameObject bullet = PhotonNetwork.Instantiate("bullet", trans.transform.position, Quaternion.identity);
            bullet.GetComponent<bullet>().setter(Damage, Target);
        }
    }
    public void MakeFireball()
    {
        if (Target != null)
        {
            GameObject bullet = PhotonNetwork.Instantiate("fireball", trans.transform.position, Quaternion.identity);
            bullet.GetComponent<Fireball>().BulletDamage = Damage;
            bullet.GetComponent<Fireball>().target = Target;
            bullet.GetComponent<Fireball>().str = "Player";
        }
    }
    [PunRPC]
    public void reverse()
    {
        transform.localScale = new Vector3(1, 1, 1);
        speed *= -1;
    }
    void Update()
    {
        if (!PhotonNetwork.IsMasterClient)
            return;
        transform.Translate(Vector3.right * currentSpeed * Time.deltaTime);
    }
}
