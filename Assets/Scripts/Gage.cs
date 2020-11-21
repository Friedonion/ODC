using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
public class Gage : MonoBehaviourPunCallbacks
{
    public int choco_per_second = 3, mint_per_second = 2, max_choco = 100, max_mint = 100;
    public float choco = 0, mint = 0;
    void Update()
    {
        AddToChoco(Time.deltaTime * choco_per_second);
        AddToMint(Time.deltaTime * mint_per_second);
    }

    public void AddToChoco(float val)
    {
        choco = (choco + val) > max_choco ? max_choco : (choco + val);
    }
    public void AddToMint(float val)
    {
        float tmp = mint + val;
        tmp = tmp > max_mint ? max_mint : tmp;
        photonView.RPC("setmint", RpcTarget.AllBuffered, tmp-mint);
    }

    [PunRPC]
    public void setmint(float val)
    {
        mint += val;
    }

}
