using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllDamage : MonoBehaviour
{
    public int damage, mintneed, choconeed;
    public GameObject particle;

    public void Attack()
    {
        GameObject[] Enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (mintneed<=transform.parent.GetChild(0).GetComponent<UImanager>().mint && choconeed<=transform.parent.GetChild(0).GetComponent<UImanager>().choco)
        {
            transform.parent.GetChild(0).GetComponent<UImanager>().mint -= mintneed;
            transform.parent.GetChild(0).GetComponent<UImanager>().choco -= choconeed;
            foreach (GameObject Enemy in Enemies)
            {
                Debug.Log(Enemy.name);
                    Enemy.GetComponent<GetDamage>().GetD(damage);
                    GameObject Effect = Instantiate(particle, Enemy.GetComponent<Transform>().position, Quaternion.identity);
                    Destroy(Effect, 1.0f);
            }
        }
    }
}
