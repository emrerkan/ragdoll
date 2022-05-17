using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class TestCollision : MonoBehaviour
{
    public static Action onDown;
    [SerializeField] Collider myCollider;
    [SerializeField] float respawnTime = 30f;
    Rigidbody[] rigidbodies;
    bool bIsRagdoll = false;
    Animator animator;
    public Material deadMaterial;
    void Start()
    {
        rigidbodies = GetComponentsInChildren<Rigidbody>();
        animator = GetComponent<Animator>();
    }
    public void OnDown()
    {
        animator.SetTrigger("Falling");
    }
    public void OnUp()
    {
        if (!bIsRagdoll)
        {
            ToggleRagdoll(false);
            StartCoroutine(GetBackUp());
            transform.SetParent(null);
            onDown?.Invoke();

        }
    }
    private void ToggleRagdoll(bool bisAnimating)
    {
        bIsRagdoll = !bisAnimating;
        myCollider.enabled = bisAnimating;

        foreach (Rigidbody ragdollBone in rigidbodies)
        {
            ragdollBone.isKinematic = bisAnimating;
            ragdollBone.useGravity = !bisAnimating;

        }
        GetComponent<Animator>().enabled = bisAnimating;

    }
    private IEnumerator GetBackUp()
    {
        yield return new WaitForSeconds(respawnTime);
        ToggleRagdoll(true);
    }

    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        var zombie = collision.gameObject.GetComponent<Zombie>();
        if(zombie != null)
        {
            Debug.Log(collision.gameObject.name);
            Debug.Log("I'm dead");
            GetComponent<MeshRenderer>().material = deadMaterial;
        }
    }
}
