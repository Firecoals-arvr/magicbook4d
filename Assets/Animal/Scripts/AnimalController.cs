using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

namespace Firecoals.Animal
{

    public class AnimalController : AnimalAnimation
    {
        //TODO Specify when jump anim or walk anim should be play
        protected void FixedUpdate()
        {
            if (Input.touchCount > 0 && Input.touchCount < 2)
            {
                var touchTemp = Input.GetTouch(0);
                //TouchPosition = touchTemp.position;
            }
            if (Input.GetMouseButtonDown(0))//TODO && not hover UI
            {
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
                        TouchPosition = hit.point;
                        CanMove = true;
                        Item.transform.position = TouchPosition;
                        //TODO SetActive(touch effect) = true

                    }
                }
            }
        }  
    }

}

