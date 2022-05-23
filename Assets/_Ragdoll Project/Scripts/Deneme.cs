using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deneme : MonoBehaviour
{
    public Animator animator;

    public float speed;
    public float strafeSpeed;

    public float jumpForce;
    public bool isGrounded;
    public Rigidbody hips;

    public readonly int Walk = Animator.StringToHash("isWalk");
    
    public readonly int Run = Animator.StringToHash("isRun");
    private readonly int Left = Animator.StringToHash("isLeft");
    private readonly int Right = Animator.StringToHash("isRight");
    private readonly int Jump = Animator.StringToHash("isJump");

    public int activeAnimation;
    void Start()
    {
        hips = GetComponent<Rigidbody>();
        
    }

    void FixedUpdate()
    {
     
        
        if (Input.GetKey(KeyCode.W))
        {
            
            if (Input.GetKey(KeyCode.LeftShift))
            {
                animator.SetBool(Walk, false);
                animator.SetBool(Run, true);
                activeAnimation = Run;
                hips.AddForce(hips.transform.forward * speed * 2);

            }
            else
            {
                animator.SetBool(Walk, true);
                animator.SetBool(Run, false);
                activeAnimation = Walk;
                hips.AddForce(hips.transform.forward * speed);

            }
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            animator.SetBool(Walk, false);
            animator.SetBool(Run, false);

        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            animator.SetTrigger(Left);
            activeAnimation = Left;
            hips.AddForce(-hips.transform.right * strafeSpeed);
        }
        if (Input.GetKey(KeyCode.S))
        {
            animator.SetBool(Walk,true);
            activeAnimation = Walk;
            hips.AddForce(-hips.transform.forward * speed);
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            animator.SetBool(Walk, false);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            animator.SetTrigger(Right);
            
            activeAnimation = Right;
            hips.AddForce(hips.transform.right * strafeSpeed);

        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded)
            {
                hips.AddForce(new Vector3(0, jumpForce, 0));
                animator.SetTrigger(Jump);
                isGrounded = false;
            }
        }
        
    }

}
