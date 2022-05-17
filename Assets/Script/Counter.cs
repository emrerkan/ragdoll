using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Counter : MonoBehaviour
{
    [SerializeField] Text counterText;
    public int saveCounter;

    private void Start()
    {
        SaveRagdolls.count += Save;
    }
    private void OnDestroy()
    {
        SaveRagdolls.count -= Save;
    }
    void Save(int x)
    {
        saveCounter = saveCounter + x;
        CounterText(saveCounter);
    }
    void CounterText(int x)
    {
        counterText.text = saveCounter.ToString();
    }
}
