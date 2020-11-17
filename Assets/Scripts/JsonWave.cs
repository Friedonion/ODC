﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;

[Serializable]
public class WaveData
{
    public int len;
    public float time;
    public float delay;
    public Waves waves;

}


[Serializable]
public class WaveD
{ 
    public List<int> row;
}

[Serializable]
public class Waves
{
    public List<WaveD> wave;
}

public class JsonWave : MonoBehaviour
{
    WaveData waveData;
    public GameObject enemyPoint;
    public GameObject enemyPoint2;
    public GameObject[] enemy;
    public int level;
    float timer = 0;
    float timer2 = 0;
    int i = 0;
    int j = 0;
    int k=0;
    public void getData(string path, string file_name)
    {
        FileStream fileStream = new FileStream(string.Format("{0}/{1}.json", path, file_name), FileMode.Open);
        byte[] data = new byte[fileStream.Length];
        fileStream.Read(data, 0, data.Length);
        fileStream.Close();
        string json_board_data = Encoding.UTF8.GetString(data);
        waveData = JsonUtility.FromJson<WaveData>(json_board_data);
    }
    void Start()
    {
        getData(Application.dataPath + "/levelData", "Board_" + level);
    }
  
            
 

    public void Sqawn()
    { 
    
    }
    void Update()
    {
        timer += Time.deltaTime;
        if(timer>=waveData.time)
        {
            timer2 += Time.deltaTime;
            if (timer2 >= waveData.delay)
            {
                k = waveData.waves.wave[i].row[j];
                Instantiate(enemy[k], enemyPoint.GetComponent<Transform>().position, Quaternion.identity);
                GameObject dummyenemy = Instantiate(enemy[k], enemyPoint2.GetComponent<Transform>().position, Quaternion.identity);
                dummyenemy.GetComponent<Enemy>().speed = -(dummyenemy.GetComponent<Enemy>().speed);
                dummyenemy.transform.localScale = new Vector3(1, 1, 1);
                j++;
                timer2 = 0;
                if (waveData.waves.wave[i].row.Count <= j)
                {
                    j = 0;
                    i++;
                    timer = 0;
                    if (waveData.waves.wave.Count<=i)
                    {
                        i = 0;
                    }
                }
            }
        }
            
 
    }
}
