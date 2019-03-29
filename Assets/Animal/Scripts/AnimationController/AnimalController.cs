using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

namespace Firecoals.Animal
{

    public class AnimalController : AnimalAnimation
    {
        //TODO Specify when jump anim or walk anim should be play
        private Vector2 firtstouchpos;
        private Vector2 touchpos;

        void Update()
        {
            //RotateCreature(firtstouchpos, touchpos);
            
        }
        protected void FixedUpdate()
        {
            if (Input.GetMouseButtonDown(0))//TODO && not hover UI
            {
                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out var hit))
                {

                    if (hit.collider.gameObject)
                    {
                        firtstouchpos = touchpos;
                        Debug.LogWarning("hit " + hit.collider.gameObject.name);
                    }
                    if (hit.collider.gameObject.layer == 9)
                    {
                        Debug.LogWarning("hit " + hit.point + " " + hit.collider.gameObject.name);
                        DestinationPosition = hit.point;
                        CanMove = true;
                        IsMoving = true;
                        Item.transform.position = DestinationPosition;
                        //TODO SetActive(touch effect) = true

                    }
                }
            }
        }
        public void RotateCreature(Vector2 firstpos, Vector2 touchpos)
        {
            //Log.text = "first:" + firtstouchpos.ToString() + "newpos:" + touchpos.ToString();
            //creature.AnimationChange();
            float x = touchpos.x - firstpos.x;
            float y = touchpos.y - firstpos.y;
            int directionx;
            Vector2 rotateAngle = new Vector2(x, y);
            float controlAngle = Vector2.Angle(rotateAngle, Vector2.up);
            if (x > 0)
            {
                directionx = 1;
            }
            else
            {
                directionx = -1;
            }
            //		Debug.Log ("Rotating");

            float angleAB = Camera.main.transform.rotation.y + controlAngle * directionx;
            //Log.text = angleAB.ToString();
            transform.rotation = Quaternion.Euler(0, angleAB, 0);
            float newposx = StopDistance * Mathf.Sin(controlAngle * Mathf.Deg2Rad) * directionx;
            float newposy = StopDistance * Mathf.Cos(controlAngle * Mathf.Deg2Rad);
        }
    }

}

