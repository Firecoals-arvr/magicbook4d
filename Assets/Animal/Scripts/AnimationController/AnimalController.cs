using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

namespace Firecoals.Animal
{

    public class AnimalController : AnimalAnimation
    {
        public bool Jump { get; set; }
        public int DemJump { get; set; }

        public bool isFrog;
        public  bool CanEat { get; set; }
        public float ScaleTemp;
        protected void FixedUpdate()
        {
            if (Input.GetMouseButtonDown(0))//TODO && not hover UI
            {
                CanEat = false;
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
                        if(Item!=null)
                        Item.transform.position = hit.point;
                        
                            //TODO SetActive(touch effect) = true
                    }
                }
            }
        }
    
    }

}

