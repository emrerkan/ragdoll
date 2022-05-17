using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : MonoBehaviour
{
    
    NavMeshAgent agent;
    Animator animator;
    [SerializeField] bool isAttack;
    [SerializeField] GameObject obje;

    [Header("Test")]
    public Transform ragdollObject;
    public Transform testRagdollObject;
    private List<Transform> ragdolls;
    public LayerMask rayLayer;
 //   public GameObject rayçýkýþ;
    public float viewRadius;
    [Range(0, 360)]
    public float viewAngle;
    public int index;
    public bool attackAnim;
    private void Awake()
    {
       
    }
    void Start()
    {
        index = 999;
        ragdolls = new List<Transform>();
        isAttack = false;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        SetRagdollList();
        AnimationToRagdoll.onDown += SetRagdollList;
        
    }
    
    private void Update()
    {

        
        if (!isAttack)
        {
            FindsObject();
            Movement();
        }
        else
        {
            if (!attackAnim)
            {
                CheckRemainingDistance();
            }
        }
    }
    void CheckRemainingDistance()
    {
        if(agent.remainingDistance <= .5f)
        {
            attackAnim = false;
            //agent.enabled = false;

            animator.SetTrigger("Attack");

        }
    }
    void SetRagdollList()
    {
        for (int i = 0; i < ragdollObject.childCount; i++)
        {
            ragdolls.Add(ragdollObject.GetChild(i));
        }
    }
    void SetRagdollList(Transform t)
    {
        if (t.GetHashCode() == testRagdollObject.GetHashCode())
        {
            Debug.Log("Obje tutuldu baþka yere gidiyor.");
            agent.isStopped = true;
            agent.ResetPath();
            isAttack = false;
        }
        SetRagdollList();
        
    }
    void GoNextPoint()
    {
        Debug.Log("(Zombie)Go Next Point");
        Vector3 v = RandomNavSphere(20f, transform);
        agent.destination = v;
        //agent.SetDestination(v);
        obje.transform.position = v;
    }
    // Random Position
    public Vector3 RandomNavSphere(float walkRadius, Transform agent)
    {
        Vector3 randomDirection = Random.insideUnitSphere * walkRadius;
        randomDirection += agent.transform.position;

        NavMeshHit hit;
        if (!NavMesh.SamplePosition(randomDirection, out hit, walkRadius, NavMesh.AllAreas))
        {
           
            return RandomNavSphere(walkRadius, agent);

        }

        Vector3 finalPosition = hit.position;


        return finalPosition;
    }
    // Random Next Point
    void Movement()
    {
        if (!isAttack)
        {
            if (agent.remainingDistance < 0.5f)
            {
                //Debug.Log("Buraya giriyorum");
                agent.isStopped = true;
                
                StartCoroutine(Delay());
            }
        }
    }
    // Wait the character when came to next point
    IEnumerator Delay()
    {
        
        transform.Rotate(0, 75f * Time.deltaTime, 0);
        yield return new WaitForSeconds(Random.Range(4, 10));
        if (agent.enabled && agent.remainingDistance < 0.5f && !isAttack)
        {
            
            GoNextPoint();
            agent.isStopped = false;
        }

    }
    // Starting attack When Zombie see character.
    void Attacking(Transform target)
    {
        testRagdollObject = target;
        agent.isStopped = true; // Navmesh is stop.
        agent.ResetPath(); //
        Debug.Log("path reset is done.");
        obje.transform.position = target.position;
        agent.destination = target.position;
    }
    
    void FindsObject()
    {
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, rayLayer);
        
        for (int i = 0; i < targetsInViewRadius.Length; i++)
        {
            
            float dstToTarget = FindCloseCharacter();
            Vector3 dirToTarget = FindToTarget();
            if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2 && index != 999)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, dirToTarget, out hit, dstToTarget + 2))
                {
                    Debug.Log(this.gameObject.name + " isimli obje :" +  hit.collider.name);
                    if (hit.collider.tag == "Player" && !isAttack)
                    {
                        isAttack = true;
                        Attacking(hit.transform);
                    }
                }
            }
        }
    }
    float FindCloseCharacter()
    {
        for (int i = 0; i < ragdolls.Count; i++)
        {
            if (ragdolls[i].gameObject.activeInHierarchy)
            {
                index = i;
                break;
            }

        }
        if(index != 999)
        {
            float minDistance = Vector3.Distance(ragdolls[index].position, transform.position);
            for (int i = 0; i < ragdolls.Count; i++)
            {
                if ((minDistance < Vector3.Distance(ragdolls[i].position, transform.position)) && ragdolls[i].gameObject.activeInHierarchy)
                {
                    index = i;
                    minDistance = Vector3.Distance(ragdolls[i].position, transform.position);
                }
            }
            return minDistance;
        }
        else
        {
            return -1;
        }
        

        // Find Close Character

    }
    Vector3 FindToTarget()
    {
        if(index != 999)
        {
            Vector3 distance = (ragdolls[index].position - transform.position).normalized;
            return distance;
        }
        else
        {
            return Vector3.zero;
        }

    }
    void FindObject()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, Vector3.forward);

        Debug.DrawRay(transform.position,Vector3.forward * 5f);
        
        
        if(Physics.Raycast(ray,out hit, 10f))
        {
            if(hit.transform.tag == "Player")
            {
                Debug.Log("Attacking!!!!!");
                isAttack = true;
                agent.isStopped = true;
                agent.speed = agent.speed + 2f;
                agent.acceleration = agent.acceleration + 3f;    
                Attacking(hit.transform);
            }
            
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision Enter : " + collision.gameObject.name);
        var ragdoll = collision.gameObject.GetComponentInParent<AnimationToRagdoll>();
        if(ragdoll != null)
        {
            attackAnim = true;
            ragdoll.Dead();
            Debug.Log("Ragdoll ile çarpýþtým þu an onu vuruyorum.");
        }
    }
}