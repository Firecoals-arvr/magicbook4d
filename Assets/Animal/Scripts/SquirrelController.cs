using System.Collections;
using System.Collections.Generic;
using Firecoals.Animal;
using UnityEngine;

public class SquirrelController : AnimalController
{

    public bool Jump { get; set; }
    public int DemJump { get; set; }
    public GameObject Item1;
    public GameObject Item2;
    public string movingAnimName;
    public string stopAnimName;
    public string idleAnimName;
    protected void Update()
    {
        if (CanMove)
        {
            SmoothMove(DestinationPosition, movingAnimName, stopAnimName,idleAnimName);
        }
    }
    protected void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0))//TODO && not hover UI
        {
            DemJump = 0;
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var hit))
            {

                if (hit.collider.gameObject)
                {
                    Debug.LogWarning("hit " + hit.collider.gameObject.name);
                }
                if (hit.collider.gameObject.layer == 9)
                {
                    Debug.LogWarning("hit " + hit.point + " " + hit.collider.gameObject.name);
                    DestinationPosition = hit.point;
                    CanMove = true;
                    IsMoving = true;
                    Jump = true;
                    Item1.SetActive(true);
                    Item2.SetActive(false);
                    Item1.transform.position = DestinationPosition;
                    //Item.transform.position = DestinationPosition;
                    //TODO SetActive(touch effect) = true

                }
            }
        }
    }
}
