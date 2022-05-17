using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DenemeScript : MonoBehaviour
{

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            transform.position += Vector3.right;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
        
    }
}