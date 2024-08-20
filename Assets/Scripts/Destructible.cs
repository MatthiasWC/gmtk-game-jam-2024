using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Destructible : MonoBehaviour
{
    public delegate void CallBack();
    private List<CallBack> callBacks = new List<CallBack>();

    public void Destruct()
    {
        foreach (CallBack cb in callBacks)
        {
            cb();
        }
        Destroy(gameObject);
    }

    public void AddCallback(CallBack cb)
    {
        callBacks.Add(cb);
    }
}
