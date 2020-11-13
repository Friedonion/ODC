using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GetDamage : MonoBehaviour
{

   
    public float StartHealth;
    public float Health;
    public GameObject HealthBar;

    void Start()
    {
        Health = StartHealth;
    }
    public void GetD(float damage)
    {
        Health -= damage;
        HealthBar.GetComponent<Image>().fillAmount = Health / StartHealth;
        if (Health <= 0)
        {
            Destroy(gameObject);
        }
    }

}
