using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using UnityEngine;
using Photon.Pun;
public class PlayerUnit : MonoBehaviourPunCallbacks
{
    public float range;
    public float Damage;
    public float speed;
    float currentSpeed;
    public Animator anim;
    public GameObject Target;
    public GameObject Splash;
    public GameObject Bullet;
    public GameObject trans;

    void Start()
    {
        currentSpeed = speed;
        Debug.Log("speed : "+speed);
        anim = GetComponent<Animator>();
        anim.SetInteger("Player", 0);
        InvokeRepeating("UpdateTarget", 0f, 0.2f); //0.2초에 한번씩만 0초부터
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(this.gameObject.transform.position, range);
    }

    public void SplashDamage()
    {
        GameObject splash = Instantiate(Splash, trans.transform.position, Quaternion.identity);
        splash.GetComponent<Splash>().SplashDamage = Damage;
        splash.GetComponent<Splash>().str = "Enemy";

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
    public void MakeBullet()
    {
        if (Target != null)
        {
            GameObject bullet = Instantiate(Bullet, trans.transform.position, Quaternion.identity);
            bullet.GetComponent<bullet>().setter(Damage, Target);
        }
    }

    void UpdateTarget() // 공격 대상 설정 
    {
        if (Target == null)
        {
            GameObject[] Enemies = GameObject.FindGameObjectsWithTag("Enemy");
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
    [PunRPC]
    public void reverse()
    {
        transform.localScale = new Vector3(-1, 1, 1);
        speed *= -1;
    }
    void Update()
    {
        transform.Translate(Vector3.left * currentSpeed * Time.deltaTime);
     }
}
