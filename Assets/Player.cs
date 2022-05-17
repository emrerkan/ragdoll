using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    
    [SerializeField] private Animator animator;
    
    void Start()
    {
        RagdollCollider(false);
        RagdollPhysics(true);

    }
    void RagdollPhysics(bool s)
    {
        Rigidbody[] rb = GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody item in rb)
        {
            item.isKinematic = s;
        }
    }
    void RagdollCollider(bool s)
    {
        Collider[] tempcol = GetComponentsInChildren<Collider>();

        foreach (Collider item in tempcol)
        {
            item.enabled = s;
        }
    }
    void OnPlayerDown()
    {
        //playeri tuttu
        RagdollCollider(false);
        RagdollPhysics(true);
    }
    void OnPlayerUp()
    {
        // playeri býraktý
        RagdollCollider(true);
        RagdollPhysics(false);
    }

   
}
