using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using Photon.Pun;
public class bullet : MonoBehaviourPunCallbacks
{
    public GameObject target;
    public float BulletDamage;
    public int BulletSpeed;
    private void OnTriggerEnter(Collider other)
    {
        if (!PhotonNetwork.IsMasterClient)
            return;
        if (other == target.GetComponent<Collider>())
        {
            other.GetComponent<GetDamage>().GetD(BulletDamage);
            PhotonNetwork.Destroy(gameObject);
        }
    }
    public void setter(float damage, GameObject target)
    {
        BulletDamage = damage;
        this.target = target;
    }

    void Update()
    {
        if (!PhotonNetwork.IsMasterClient)
            return;
        if (target == null || target.activeSelf == false)
            PhotonNetwork.Destroy(this.gameObject);
        Vector3 TargetPosition = target.transform.position;
        transform.LookAt(TargetPosition);
        transform.position = Vector3.MoveTowards(transform.position, TargetPosition, BulletSpeed * Time.deltaTime);
    }
}
