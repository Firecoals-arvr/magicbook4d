using System;
using System.Collections;
using System.Collections.Generic;
using Firecoals.Animal;
using UnityEngine;

public class OnoffEnvironment : MonoBehaviour
{
    public delegate void Environment();
    public event Environment MoveEventStarted;
    public GameObject ObjEnvironment { get; set; }
    private bool CheckActive;
    public GameObject button { get; set; }
    // Start is called before the first frame update
    public void Update()
    {
        Onclick();
    }
   private void  Onclick()
    {
            EventDelegate onpick = new EventDelegate(this, "OnMoveEventStarted");
            EventDelegate.Set(button.GetComponent<UIButton>().onClick, onpick);     
    }
    protected virtual void Awake()
    {
        button = GameObject.FindGameObjectWithTag("Environment");
        ObjEnvironment = transform.parent.GetComponentInChildren<EnvironmentAnimal>().gameObject;
    }
    public void OnMoveEventStarted()
    {
        if (CheckActive)
        {
            ObjEnvironment.SetActive(true);
        }
        else
        {
            ObjEnvironment.SetActive(false);
        }
        Debug.Log("<color=red>" + CheckActive + "</color>");
        CheckActive = !CheckActive;
        MoveEventStarted?.Invoke();
    }
}
