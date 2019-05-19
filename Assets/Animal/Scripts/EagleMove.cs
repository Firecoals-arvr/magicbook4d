using System.Collections;
using System.Collections.Generic;
using Firecoals.Animal;
using UnityEngine;
namespace Firecoals.Animal
{
    public class EagleMove : MonoBehaviour
    {
        public bool CanEat { get; set; }
        public float Jumpdistiance;
        public string movingAnimName;
        public string stopAnimName;
        public bool Eat { get; set; }
        public string IdleAnimName;
        public bool IsJump;
        public bool CanjumpWolf;
        public float timedelay = 1;
        public float SpeedJump = 2;
        protected Animator animator;
        public float MoveSpeed;
        public Vector3 DestinationPosition { get; set; }
        public bool IsMoving { get; set; }
        public bool CanMove { get; set; }
        protected bool IsRotating;
        public float StopDistance;
        public bool IsWolf;
        public bool IsEgale;
        protected RaycastHit hit;
        public float ScaleAnimal { get; set; }
        public GameObject Item { get; set; }
        private GameObject effectTouch;
        protected virtual void Start()
        {
            Item = transform.parent.GetComponentInChildren<Item>().gameObject;
            animator = GetComponent<Animator>();
            effectTouch = GameObject.Find("EffectTouch");
        }
        protected void FixedUpdate()
        {
            if (Input.GetMouseButtonDown(0))//TODO && not hover UI
            {
                CanEat = false;
                Eat = false;
                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit))
                {
                    if (Vector3.Distance(transform.position, hit.point) > Jumpdistiance)
                    {
                        IsJump = true;
                    }
                    else
                    {
                        IsJump = false;
                    }
                    if (hit.collider.gameObject)
                    {
                        Debug.LogWarning("hit " + hit.collider.gameObject.name);
                    }
                    if (hit.collider.gameObject.layer == 9)
                    {
                        DestinationPosition = hit.point;
                        CanMove = true;
                        IsMoving = true;
                        Item.transform.position = DestinationPosition;
                        effectTouch.transform.position = Item.transform.position;
                        effectTouch.transform.GetChild(0).gameObject.SetActive(true);
                        StartCoroutine(ResetEffect());
                    }
                }
            }
        }
        protected void SmoothMove(Vector3 target, string moveAnimClipName, string stopAnimClipName, string idleAnimClipName)
        {
            IsRotating = false;
            var targetDir = target - this.transform.position;
            var angle = Quaternion.LookRotation(targetDir);
            if (Item != null)
            {
                Item.transform.rotation = angle;
            }

            if (IsRotating == false)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, angle, Time.deltaTime * 100.0f);
            }
            if (Quaternion.Angle(angle, transform.rotation) < 5)
            {
                if (Vector3.Distance(transform.position, target) > Jumpdistiance * ScaleAnimal && IsJump == true)
                {
                    transform.position = Vector3.MoveTowards(transform.position, target, MoveSpeed * Time.deltaTime * ScaleAnimal);
                    animator.SetBool("IsWalk", true);
                    CanjumpWolf = false;
                    IsRotating = false;
                }
                else
                {
                    if (IsJump)
                    {

                        StartCoroutine(delayjump(target));
                    }
                    else
                    {
                        if (Vector3.Distance(transform.position, target) > StopDistance * ScaleAnimal && IsJump == false)
                        {
                            transform.position = Vector3.MoveTowards(transform.position, target, MoveSpeed * Time.deltaTime * ScaleAnimal);
                            animator.SetBool("IsWalk", true);
                            CanjumpWolf = false;
                            IsRotating = false;
                        }
                    }


                }


            }
            else
            {
                IsRotating = true;
            }

            if (Vector3.Distance(transform.position, target) < StopDistance * ScaleAnimal)
            {
                if (IsMoving)
                {

                    animator.SetBool("IsWalk", false);
                    IsMoving = false;
                    CanMove = false;
                }
            }
            else
            {
                IsMoving = true;
                CanMove = true;
            }

        }

        protected void Update()
        {
            if (CanMove)
            {
                ScaleAnimal = (float)this.transform.parent.transform.localScale.x;
                SmoothMove(DestinationPosition, movingAnimName, stopAnimName, IdleAnimName);
            }
        }

        IEnumerator delayjump(Vector3 target)
        {


            animator.SetBool("IsJump", true);
            float speed = 0;
            yield return new WaitForSeconds(timedelay);
            speed = SpeedJump;
            IsJump = false;
            transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * speed * ScaleAnimal);
            speed = 0;
            animator.SetBool("IsJump", false);
            animator.SetBool("IsWalk", false);
            CanjumpWolf = true;
        }
        IEnumerator ResetEffect()
        {
            yield return new WaitForSeconds(.6f);
            effectTouch.transform.GetChild(0).gameObject.SetActive(false);
        }
    }
}
