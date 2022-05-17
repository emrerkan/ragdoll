using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SaveRagdolls : MonoBehaviour
{
    
    public static Action<int> count;
    private void OnTriggerExit(Collider other)
    {
        count?.Invoke(1);
    }
    
}
