    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    public float Speed = 5f;
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            float yAxis = Input.GetAxis("Mouse X");
            float zAxis = Input.GetAxis("Mouse Y");
            this.transform.eulerAngles += new Vector3(zAxis, yAxis, 0);
        }
       
        Movement();
    }
    void Movement()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        this.gameObject.transform.position = new Vector3(this.transform.position.x + x,transform.position.y,this.transform.position.z + z);
    }

}
