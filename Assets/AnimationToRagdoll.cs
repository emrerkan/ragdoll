using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;
public class AnimationToRagdoll : MonoBehaviour
{
    public static Action<Transform> onDown;
    [SerializeField] Collider myCollider;
    NavMeshAgent agent;
    Rigidbody[] rigidbodies;
    Collider[] colliders;
    bool bIsRagdoll = false;
    bool mouseDown;
    Animator animator;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        rigidbodies = GetComponentsInChildren<Rigidbody>();
        colliders = GetComponentsInChildren<Collider>();
        animator = GetComponent<Animator>();
        ToggleRagdoll(false);
    }
    private void Update()
    {
        if (!mouseDown)
        {
            Movement();
        }
        
    }
    void Movement()
    {
        if (!mouseDown)
        {
            if (agent.remainingDistance < 0.5f)
            {
                agent.isStopped = true;

                StartCoroutine(Delay());
            }
        }
    }
    void GoNextPoint()
    {
        Debug.Log("(AnimationToRagdoll)Go Next Point");
        Vector3 v = RandomNavSphere(20f, transform);
        agent.destination = v;
        
    }
    IEnumerator Delay()
    {

        transform.Rotate(0, 75f * Time.deltaTime, 0);
        yield return new WaitForSeconds(UnityEngine.Random.Range(4, 10));
        if (agent.enabled && agent.remainingDistance < 0.5f && !mouseDown)
        {
            GoNextPoint();
            agent.isStopped = false;
        }

    }
    public Vector3 RandomNavSphere(float walkRadius, Transform agent)
    {
        Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * walkRadius;
        randomDirection += agent.transform.position;

        NavMeshHit hit;
        if (!NavMesh.SamplePosition(randomDirection, out hit, walkRadius, NavMesh.AllAreas))
        {

            return RandomNavSphere(walkRadius, agent);

        }

        Vector3 finalPosition = hit.position;


        return finalPosition;
    }
    public void OnDown()
    {
        mouseDown = true;
        agent.enabled = false;
        animator.SetTrigger("Falling");

    }
    public void OnUp()
    {
        if (bIsRagdoll)
        {

            ToggleRagdoll(true);
            transform.SetParent(null);
            onDown?.Invoke(this.gameObject.transform);

        }
    }
    
    private void ToggleRagdoll(bool s)
    {
        bIsRagdoll = !s;
        if(rigidbodies.Length > 0)
        {
            foreach (Rigidbody ragdollBone in rigidbodies)
            {
                ragdollBone.isKinematic = !s; 
                ragdollBone.useGravity = s;
                ragdollBone.detectCollisions = s;
            }
        }
        if(colliders.Length > 0)
        {
            foreach (Collider colliderBone in colliders)
            {
                colliderBone.enabled = s; 
            }
        }
        myCollider.enabled = !s;
        GetComponent<Animator>().enabled = !s;
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        var zombie = collision.gameObject.GetComponent<Zombie>();
        if(zombie != null)
        {
            Debug.Log("I'm dead");
        }
    }
    public void Dead()
    {
        animator.SetTrigger("Death");
    }
}

// https://answers.unity.com/questions/633480/how-to-detect-child-object-collisions-on-parent.html