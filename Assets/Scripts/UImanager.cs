using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
public class UImanager : MonoBehaviour
{
    public int mobnum = 4, cardlen = 5;
    private static int max_mobnum = 20, max_cardlen = 10;
    private string[] mobset = { "mintSword", "mintShield", "mintHammer", "mintArcher" };
    public Sprite[] mobimg = new Sprite[max_mobnum];
    private int[] mobtag = new int[max_cardlen];
    public int[] choconeed = new int[max_mobnum], mintneed = new int[max_mobnum];
    public GameObject[] img = new GameObject[5];
    public Text Chocotext, Minttext;
    public GameObject chocobar, mintbar, spawnpoint, gage;
    void Awake()
    {
        for (int i = 0; i < cardlen; i++)
        {
            mobtag[i] = Random.Range(0, mobnum);
            img[i].GetComponent<Image>().sprite = mobimg[mobtag[i]];
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < cardlen; i++)
        {
            if (choconeed[mobtag[i]] < gage.GetComponent<Gage>().choco && mintneed[mobtag[i]] < gage.GetComponent<Gage>().mint)
                img[i].GetComponent<Image>().color = new Color(1, 1, 1, 1);
            else
                img[i].GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
        }
        chocobar.GetComponent<Image>().fillAmount = gage.GetComponent<Gage>().choco / gage.GetComponent<Gage>().max_choco;
        mintbar.GetComponent<Image>().fillAmount = gage.GetComponent<Gage>().mint / gage.GetComponent<Gage>().max_mint;
        Chocotext.text = "Choco : " + (int)gage.GetComponent<Gage>().choco;
        Minttext.text = "Mint : " + (int)gage.GetComponent<Gage>().mint;

        for (int i = 0; i < cardlen; i++)
            if (img[i].GetComponent<CardClick>().clicked)
            {
                img[i].GetComponent<CardClick>().clicked = false;
                if (choconeed[mobtag[i]] <= gage.GetComponent<Gage>().choco && mintneed[mobtag[i]] <= gage.GetComponent<Gage>().mint)
                {
                    gage.GetComponent<Gage>().AddToChoco(-1 * choconeed[mobtag[i]]);
                    gage.GetComponent<Gage>().AddToMint(-1 * mintneed[mobtag[i]]);
                    GameObject m = PhotonNetwork.Instantiate(mobset[mobtag[i]], spawnpoint.GetComponent<Transform>().position, Quaternion.identity);
                    m.GetComponent<PlayerUnit>().photonView.RPC("reverse",RpcTarget.AllBuffered);
                    mobtag[i] = (mobtag[i] + Random.Range(1, mobnum)) % mobnum;
                    img[i].GetComponent<Image>().sprite = mobimg[mobtag[i]];
                }
            }

    }
}
