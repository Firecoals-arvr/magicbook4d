using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dispatcher : MonoBehaviour
{
    private static Dispatcher instance;

    private readonly List<Action> _pending = new List<Action>();

    public static Dispatcher Instance
    {
        get
        {
            return instance;
        }
    }

    public void Invoke(Action fn)
    {
        lock (this._pending)
        {
            this._pending.Add(fn);
        }
    }

    private void InvokePending()
    {
        lock (this._pending)
        {
            foreach (Action action in this._pending)
            {
                action();
            }

            this._pending.Clear();
        }
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    private void Update()
    {
        this.InvokePending();
    }
}
