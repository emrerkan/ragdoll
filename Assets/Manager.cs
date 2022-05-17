using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Manager : MonoBehaviour
{
    
    [SerializeField] GameObject ragdolls;
    [SerializeField] Transform ragdollParent;
    [SerializeField] Collider area;
    
    public int listArray;
    public List<GameObject> ragdollList;
    public int count;
    void Start()
    {
        ragdollList = new List<GameObject>();
        
        for (int i = 0; i < listArray; i++)
        {
            GameObject g = Instantiate(ragdolls, ragdollParent);
            g.name = "Character_" + i;
            g.transform.position = Vector3.one * 999;
            g.SetActive(false);
            ragdollList.Add(g);
        }
        
        count = SetCount();
        AnimationToRagdoll.onDown += SetRagdollList;

    }
    private void OnDestroy()
    {
        AnimationToRagdoll.onDown -= SetRagdollList;

    }
    void SetRagdollList(Transform t)
    {
        ragdollList = new List<GameObject>();
        for (int i = 0; i < ragdollParent.childCount; i++)
        {
            ragdollList.Add(ragdollParent.GetChild(i).gameObject);
        }

    }
    private int SetCount()
    {
        return (ragdollList.Count-1);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            CreateRagdollObject();
        }
    }
    Vector3 RandomRagdollObjectSpawn()
    {
        Vector3 randomVec = Random.insideUnitSphere * 10f;
        randomVec.y = -2f;
        if (area.bounds.Contains(randomVec))
        {
            return randomVec;
        }
        else
        {
            return RandomRagdollObjectSpawn();
        }
        
    }
    void CreateRagdollObject()
    {
        //Vector3 randomVec = Vector3.right * Random.Range(-3, 3) + Vector3.up*(-2) + Vector3.forward * Random.Range(-4, 4);

        Vector3 randomVec = RandomRagdollObjectSpawn();
        if(count >= 0)
        {
            if (!ragdollList[count].activeInHierarchy)
            {   
                ragdollList[count].transform.position = randomVec;
                ragdollList[count].SetActive(true);
            }
            else
            {
                count = count - 1;
                CreateRagdollObject();
            }
        }
        else
        {
            GameObject g = InstantiateRagdollObject();

            g.transform.position = randomVec;
            g.SetActive(true);
        }
        count = count - 1;
    }
    GameObject InstantiateRagdollObject()
    {
        GameObject g = Instantiate(ragdolls, ragdollParent);
        g.name = "Character_" + count;
        g.transform.position = Vector3.one * 999;
        g.SetActive(false);
        ragdollList.Add(g);
        count = SetCount();
        return ragdollList[count];
    }
}
