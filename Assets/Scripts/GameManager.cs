//using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject ETower;
    public GameObject PTower1;
    public GameObject PTower2;
    private bool end = false;
    private void Awake()
    {
        Screen.SetResolution(960, 540, false);
    }
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
        if(PTower1 == null || PTower2 == null)
        {
            Debug.Log("Lose");
            end = true;
            Time.timeScale = 0;
        }
    }
}

