using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Destructible : MonoBehaviour
{
    [SerializeField] private GameObject ps;

    public delegate void CallBack();
    private List<CallBack> callBacks = new List<CallBack>();

    /*private void Start()
    {
        ps = GetComponent<ParticleSystem>();
    }*/

    public void Destruct()
    {
        foreach (CallBack cb in callBacks)
        {
            cb();
        }
        Instantiate(ps, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    public void AddCallback(CallBack cb)
    {
        callBacks.Add(cb);
    }
}
