using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

namespace Firecoals.Animal
{
    public class AnimalAnimation : MonoBehaviour
    {
        public delegate void OnMoveStarted(string movingAnimationClip);

        public delegate void OnMoveStopped(string stopAnimationClip);


        public event OnMoveStarted MoveEventStarted;
        public event OnMoveStopped MoveEventStopped;

        public float movingAnimationSpeed;
        /// <summary>
        /// The animal walking speed or running speed
        /// </summary>
        public float MoveSpeed;

        /// <summary>
        /// The distance where the animal stop near by the item
        /// </summary>
        public float StopDistance;
        /// <summary>
        /// The animation attached to the object
        /// </summary>
        protected Animation anim;

        public bool HaveEat;
        public float TimeEat;
        /// <summary>
        /// The position on floor that touched
        /// </summary>
        public Vector3 DestinationPosition { get; set; }

        /// <summary>
        /// If the animal is moving
        /// </summary>
        public bool IsMoving { get; set; }
        /// <summary>
        /// Return true if the animal can move
        /// </summary>
        public bool CanMove { get; set; }
        protected bool IsRotating;
        public GameObject Item { get; set; }
        protected void Start()
        {
            SetUpAnimation();
            Item = transform.parent.GetComponentInChildren<Item>().gameObject;
        }
        /// <summary>
        /// Setting up the properties of the animation attached to the game object
        /// </summary>
        private void SetUpAnimation()
        {
            anim = GetComponent<Animation>();
            anim.wrapMode = WrapMode.Loop;
        }
        protected void PlayNow(string animationClip)
        {
            anim.PlayQueued(animationClip, QueueMode.PlayNow);
        }

        protected void PlayAfterCompleteOthers(string animationClip)
        {
            anim.PlayQueued(animationClip, QueueMode.CompleteOthers);
        }
        /// <summary>
        /// Play an animation clip
        /// for Run or Walk
        /// anim.speed= MoveSpeed
        /// CrossFade applied
        /// </summary>
        /// <param name="animationClip"></param>
        protected void PlayWalkOrRun(string animationClip)
        {
            anim[animationClip].speed = movingAnimationSpeed;
            anim.CrossFade(animationClip);
            anim.Play(animationClip);

        }

        protected virtual void OnMoveEventStarted(string movingAnimationClip)
        {
            //Debug.Log("on moving");
            PlayWalkOrRun(movingAnimationClip);
            MoveEventStarted?.Invoke(movingAnimationClip);
        }

        protected virtual void OnMoveEventStopped(string stopAnimationClip,string idleAnimClipName)
        {

           //PlayNow(stopAnimationClip);
           if (HaveEat)
           {
               StartCoroutine(StopIdle(stopAnimationClip, idleAnimClipName));
            }
           else
           {
               PlayNow(stopAnimationClip);
            }
           // Debug.Log("on stop");
            MoveEventStopped?.Invoke(stopAnimationClip);
        }

        IEnumerator StopIdle(string stopAnimationClip,string idleAnimClipName)
        {
            PlayNow(stopAnimationClip);
            yield return new WaitForSeconds(TimeEat);
            anim.Stop(stopAnimationClip);
            PlayWalkOrRun(idleAnimClipName);
        }
        /// <summary>
        /// Move The Animal
        /// </summary>
        /// <param name="target"></param>
        /// <param name="moveAnimClipName">name of the animation clip which is needed when move the animal</param>
        /// <param name="stopAnimClipName">name of the animation clip when the animal has stopped</param>
        protected void SmoothMove(Vector3 target, string moveAnimClipName, string stopAnimClipName,string idleAnimClipName)
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
                    OnMoveEventStopped(stopAnimClipName,idleAnimClipName);
                }
            }
            else
            {
                IsMoving = true;
                CanMove = true;
            }

        }
        //public void DoMoveTowards(string moveAnimClipName, string stopAnimClipName)
        //{
        //    if (IsMoving)
        //    {
        //        var destinationDistance = Vector3.Distance(DestinationPosition, transform.position);

        //        if (destinationDistance > .5f)
        //        {
        //            IsMoving = true;

        //        }
        //        else
        //        {
        //            IsMoving = false;
        //            return;
        //        }

        //        var vec = new Vector3(DestinationPosition.x, 0, DestinationPosition.z);
        //        PlayWalkOrRun(moveAnimClipName);

        //        transform.position = Vector3.MoveTowards(transform.position,
        //            DestinationPosition, MoveSpeed * Time.deltaTime);

        //        var rot = DestinationPosition - transform.position;
        //        rot.y = 0;
        //        Debug.Log("Looking at: " + rot);
        //        DoRotation(rot);

        //    }
        //}

        //public void DoRotation(Vector3 directionVector3)
        //{
        //    if (directionVector3 != Vector3.zero)
        //    {
        //        var desiredRotation = Quaternion.LookRotation(directionVector3);
        //        transform.localRotation =
        //            Quaternion.Slerp(transform.localRotation, desiredRotation, Time.deltaTime * 100f);

        //    }
        //}
    }

}
