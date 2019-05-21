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
        public bool CanEat { get; set; }
        public float ScaleTemp;
        private GameObject effectTouch;

        protected void Start()
        {
            effectTouch = GameObject.Find("EffectTouch");
            base.Start();
        }
        protected void FixedUpdate()
        {
            if (Input.GetMouseButtonDown(0))//TODO && not hover UI
            {
                CanEat = false;
                DemJump = 0;
                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out var hit) && !UICamera.isOverUI)
                {

                    if (hit.collider.gameObject)
                    {
                        //Debug.LogError("hit " + hit.collider.gameObject.name);
                    }
                    if (hit.collider.gameObject.layer == 9)
                    {
                        //Debug.LogError("hit " + hit.point + " " + hit.collider.gameObject.name);
                        DestinationPosition = hit.point;
                        CanMove = true;
                        IsMoving = true;
                        Jump = true;
                        if (Item != null)
                        {
                            Item.transform.position = hit.point;
                            effectTouch.transform.position = Item.transform.position;
                            effectTouch.transform.GetChild(0).gameObject.SetActive(true);
                            StartCoroutine(ResetEffect());
                        }

                        //TODO SetActive(touch effect) = true
                    }
                }
                

            }

        }
        IEnumerator ResetEffect()
        {
            yield return new WaitForSeconds(.6f);
            effectTouch.transform.GetChild(0).gameObject.SetActive(false);
        }

    }

}

