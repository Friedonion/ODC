using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using Photon.Pun;
using Photon.Realtime;

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

public class JsonWave : MonoBehaviourPunCallbacks
{
    WaveData waveData;
    public GameObject enemyPoint;
    public GameObject enemyPoint2;
    public string[] enemy = {"chocoSword","chocoWizard","chocoArcher"};
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
                PhotonNetwork.Instantiate(enemy[k], enemyPoint.transform.position, Quaternion.identity);
                Debug.Log(enemy[k]);
                GameObject dummyenemy = PhotonNetwork.Instantiate(enemy[k], enemyPoint2.transform.position, Quaternion.identity);
                dummyenemy.GetComponent<Enemy>().photonView.RPC("reverse", RpcTarget.AllBuffered);
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
