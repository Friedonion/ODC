using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UImanager : MonoBehaviour
{
    private static int max_mobnum = 20, max_cardlen = 10;
    public int mobnum = 3, cardlen = 10, choco_per_second = 1, mint_per_second = 2,max_choco=100,max_mint=100;
    public GameObject[] mobset = new GameObject[max_mobnum];
    public Sprite[] mobimg = new Sprite[max_mobnum];
    public int[] choconeed = new int[max_mobnum], mintneed = new int[max_mobnum];
    private int[] mobtag = new int[max_cardlen];
    public float choco = 0, mint = 0;
    public Text t1, t2;
    public GameObject chocobar, mintbar, spawnpoint;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < cardlen; i++)
        {
            mobtag[i] = Random.Range(0, mobnum);
            transform.GetChild(i).GetComponent<Image>().sprite = mobimg[mobtag[i]];
        }
    }

    // Update is called once per frame
    void Update()
    {
        choco += Time.deltaTime * choco_per_second;
        mint += Time.deltaTime * mint_per_second;
        if (choco > max_choco)
            choco = max_choco;
        if (mint > max_mint)
            mint = max_mint;
        for (int i = 0; i < cardlen; i++)
        {
            if (choconeed[mobtag[i]] < choco && mintneed[mobtag[i]] < mint)
                transform.GetChild(i).GetComponent<Image>().color = new Color(1, 1, 1, 1);
            else
                transform.GetChild(i).GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
        }
        chocobar.GetComponent<Image>().fillAmount = choco / max_choco;
        mintbar.GetComponent<Image>().fillAmount = mint / max_mint;
        t1.text = "Choco : " + (int)choco;
        t2.text = "Mint : " + (int)mint;

        for (int i = 0; i < cardlen; i++)
            if (transform.GetChild(i).GetComponent<CardClick>().clicked)
            {
                transform.GetChild(i).GetComponent<CardClick>().clicked = false;
                if (choconeed[mobtag[i]] <= choco && mintneed[mobtag[i]] <= mint)
                {
                    choco -= choconeed[mobtag[i]];
                    mint -= mintneed[mobtag[i]];
                    Instantiate(mobset[mobtag[i]], spawnpoint.transform.position, Quaternion.identity);
                    mobtag[i] = (mobtag[i] + Random.Range(1, mobnum)) % mobnum;
                    transform.GetChild(i).GetComponent<Image>().sprite = mobimg[mobtag[i]];
                }
            }

    }
}
