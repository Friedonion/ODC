using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Photon.Pun;
public class GetDamage : MonoBehaviourPunCallbacks
{

   
    public float StartHealth;
    public float Health;
    public GameObject HealthBar;

    void Start()
    {
        photonView.RPC("start", RpcTarget.AllBuffered);
    }
    [PunRPC]
    private void start()
    {
        Health = StartHealth;
    }
    [PunRPC]
    private void sethealth(float damage)
    {
        Health -= damage;
        HealthBar.GetComponent<Image>().fillAmount = Health / StartHealth;
    }

    public void GetD(float damage)
    {
        photonView.RPC("sethealth", RpcTarget.AllBuffered, damage);
        if (Health <= 0)
        {
            Debug.Log("dead");
            PhotonNetwork.Destroy(gameObject);
        }
    }
}
