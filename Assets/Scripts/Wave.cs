using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
    public GameObject enemy;
    public GameObject enemyPoint;
    void Start()
    {
        InvokeRepeating("Spawn", 0.0f, 3.0f);
    }

    
    public void Spawn()
    {
        Instantiate(enemy, enemyPoint.GetComponent<Transform>().position, Quaternion.identity);
    }
}
