using System.Collections;
using System.Collections.Generic;
using Firecoals.Animal;
using UnityEngine;

public class Test : MonoBehaviour
{
    public delegate void Environment();
    public event Environment MoveEventStarted;
    public GameObject ObjEnvironment { get; set; }
    private  bool CheckActive;
    public UIButton button;
    // Start is called before the first frame update
    public void Update()
    {
        Onclick();
    }
    public void Onclick()
    {
        EventDelegate onpick = new EventDelegate(this, "OnMoveEventStarted");
        EventDelegate.Set(button.onClick, onpick);
    }

    protected virtual void Start()
    {
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
