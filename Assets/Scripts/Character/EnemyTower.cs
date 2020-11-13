using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTower : MonoBehaviour
{
    public float range;
    public float Damage;
    public GameObject Target;
    public GameObject Bullet;
    public GameObject trans;
    public float AttackSpeed;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(this.gameObject.transform.position, range);
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
               
            }
            else
            {
                Target = null;
            }
        }
    }
    public void MakeBullet()
    {
        if(Target!=null)
        {
            GameObject bullet = Instantiate(Bullet, trans.transform.position, Quaternion.identity);
            bullet.GetComponent<bullet>().BulletDamage = Damage;
            bullet.GetComponent<bullet>().target = Target;
            Destroy(bullet, 0.5f);
        }
    }
    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.2f); //0.2초에 한번씩만 0초부터
        InvokeRepeating("MakeBullet", 0f, AttackSpeed);
    }

}
