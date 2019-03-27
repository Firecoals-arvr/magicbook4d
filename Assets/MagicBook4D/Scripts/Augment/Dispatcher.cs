using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Firecoals.Augmentation
{
    public class Dispatcher : MonoBehaviour
    {
        private static Dispatcher _instance;

        private readonly List<Action> _pending = new List<Action>();
        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                _instance = this;
            }
        }
        public static Dispatcher Instance
        {
            get { return _instance; }
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

        private void Update()
        {
            this.InvokePending();
        }
    }
}