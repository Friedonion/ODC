using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
public class Fireball : MonoBehaviourPunCallbacks
{
    
        public GameObject target;
        public float BulletDamage;
        public int BulletSpeed;
        public GameObject Splash;
        public string str;

        private void OnTriggerEnter(Collider other)
        {
            if (other == target.GetComponent<Collider>())
            {
            GameObject splash = Instantiate(Splash, transform.position, Quaternion.identity);
            splash.GetComponent<Splash>().SplashDamage = BulletDamage;
            splash.GetComponent<Splash>().str = str;
            Destroy(splash, 0.5f);
            Destroy(this.gameObject);
            Debug.Log("qwer");
        }
            if (target == null || target.activeSelf == false)
            {
                Destroy(this.gameObject);
            }
        }



        void Update()
        {
            Vector3 TargetPosition = target.transform.position;
            transform.LookAt(TargetPosition);
            Vector3 NewPosition = Vector3.MoveTowards(transform.position, TargetPosition, BulletSpeed * Time.deltaTime);
            transform.position = NewPosition;
        }
  }

