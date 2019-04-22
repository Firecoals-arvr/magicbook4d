using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowOrHideNamePanel : DefaultTrackableEventHandler
{
    // Start is called before the first frame update
    private GameObject ObjBangTen;
    protected  void Start()
    {
        ObjBangTen = transform.parent.GetComponentInChildren<BangTen>().gameObject;
    }
}