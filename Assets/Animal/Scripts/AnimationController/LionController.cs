using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Firecoals.Animal
{
    [RequireComponent(typeof(Animation))]
    public class LionController : AnimalController
    {
        public string movingAnimName;
        public string stopAnimName;
        private bool isJumping;
        public string idle;
        protected void Update()
        {
            //if (Vector3.Distance(transform.position, DestinationPosition) < jumpDistance && Vector3.Distance(transform.position, DestinationPosition) > jumpDistance - 0.03)
            //{
            //    if (!isJumping)
            //    {
            //        isJumping = true;
            //    }
            //}

            if (CanMove)
            {
                SmoothMove(DestinationPosition, movingAnimName, stopAnimName);
            }
            if (DemJump != 0)
            {
                Debug.LogWarning(DemJump.ToString());
            
            }
        }
        //public void JumpAction()
        //{
        //    if (jumpActionName != null)
        //    {
        //        Debug.Log("Jump");
        //        if (!delayStart)
        //        {
        //            Debug.Log("coroutine");
        //            delayStart = true;
        //            StartCoroutine(DelayJump());
        //        }
        //        anim[jumpActionName].speed = 1;
        //        anim.CrossFade(jumpActionName);
        //    }
        //}
        private IEnumerator wait()
        {
            yield return new WaitForSeconds(2);
            DemJump++;
            OnMoveEventStopped(idle,idle);
        }
        protected virtual void SmoothMove(Vector3 target, string moveAnimClipName, string stopAnimClipName)
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

            Debug.DrawLine(transform.position, target, Color.red, 30, false);
            if (Quaternion.Angle(angle, transform.rotation) < 5)
            {
                if (Vector3.Distance(transform.position, target) > StopDistance)
                {
                    transform.position = Vector3.MoveTowards(transform.position, target, MoveSpeed * Time.deltaTime);
                    OnMoveEventStarted(moveAnimClipName);
                    IsRotating = false;
                }

            }
            else
            {
                IsRotating = true;
            }
            if (Vector3.Distance(transform.position, target) < StopDistance)
            {
                if (IsMoving)
                {
                    IsMoving = false;
                    CanMove = false;
                    anim.Stop(moveAnimClipName);
                    if (Jump)
                    {
                        if (DemJump == 0)
                        {
                          // OnMoveEventStopped(stopAnimClipName,);
                          this.GetComponent<Animation>().PlayQueued(stopAnimClipName, QueueMode.PlayNow);
                           StartCoroutine(wait());
                        }                          
                    }
                  
                }
            }
            else
            {
                IsMoving = true;
                CanMove = true;
            }

        }
    }
}