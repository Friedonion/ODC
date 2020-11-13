using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Splash : MonoBehaviour
{
    public float SplashDamage;
    public string str;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == str)
        {
           
            other.GetComponent<GetDamage>().GetD(SplashDamage);
        }
    }
}
