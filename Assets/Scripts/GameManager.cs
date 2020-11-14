//using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject ETower;
    public GameObject PTower;
    private bool end = false;
    public GameObject enemy;
    public GameObject enemyPoint;
    void Start()
    {
        InvokeRepeating("GameIsEnd", 0.0f, 0.2f);
    }

    public void GameIsEnd()
    {
        if(ETower == null)
        {
            Debug.Log("end");
            end = true;
            Time.timeScale = 0;
        }
        if(PTower == null)
        {
            Debug.Log("Lose");
            end = true;
            Time.timeScale = 0;
        }
    }
}

