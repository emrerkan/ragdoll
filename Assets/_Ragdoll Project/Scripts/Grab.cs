using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grab : MonoBehaviour
{
    public Animator animator;

    GameObject grabbedObject;

    Rigidbody rb;

    public int isLeftOrRight;

    public bool alreadyGrabbing = false;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(isLeftOrRight))
        {
            if(isLeftOrRight == 0)
            {
                animator.SetBool("isLeftHandUp", true);
            }
            else if (isLeftOrRight == 1)
            {
                animator.SetBool("isRightHandUp", true);
            }
            if(grabbedObject != null)
            {
                FixedJoint fj = grabbedObject.AddComponent<FixedJoint>();
                fj.connectedBody = rb;
                fj.breakForce = 9001;
            }
            
        }
        else if (Input.GetMouseButtonUp(isLeftOrRight))
        {
            if (isLeftOrRight == 0)
            {
                animator.SetBool("isLeftHandUp", false);
            }
            else if (isLeftOrRight == 1)
            {
                animator.SetBool("isRightHandUp", false);
            }
            if(grabbedObject != null)
            {
                Destroy(grabbedObject.GetComponent<FixedJoint>());
            }
            grabbedObject = null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Item"))
        {
            grabbedObject = other.gameObject;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        grabbedObject = null;
    }
}
