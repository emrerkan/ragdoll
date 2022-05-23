using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimbCollision : MonoBehaviour
{
    Deneme playerController;
    void Start()
    {
        playerController = GameObject.FindObjectOfType<Deneme>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        playerController.isGrounded = true;
    }
}
