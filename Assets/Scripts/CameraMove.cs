using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public float left, right;
    public float speed;
    void Update()
    {
        if(Input.GetKey(KeyCode.A)||Input.GetKey(KeyCode.LeftArrow))
        {
            if (transform.position.x > left)
            {
                transform.Translate(Vector3.left * speed * Time.deltaTime);
            }
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            if (transform.position.x < right)
            {
                transform.Translate(Vector3.right * speed * Time.deltaTime);
            }
        }
    }
}
