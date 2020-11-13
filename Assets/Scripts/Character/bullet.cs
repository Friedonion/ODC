using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public GameObject target;
    public float BulletDamage;
    public int BulletSpeed;
    private void OnTriggerEnter(Collider other)
    {
        if (other == target.GetComponent<Collider>())
        {
            other.GetComponent<GetDamage>().GetD(BulletDamage);
            Destroy(this.gameObject);
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
