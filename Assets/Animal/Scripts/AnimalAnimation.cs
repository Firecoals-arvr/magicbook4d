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


        /// <summary>
        /// The position on floor that touched
        /// </summary>
        public Vector3 TouchPosition { get; set; }
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
            anim[animationClip].speed = MoveSpeed;
            anim.CrossFade(animationClip);
            anim.Play(animationClip);

        }

        protected virtual void OnMoveEventStarted(string movingAnimationClip)
        {
            Debug.Log("on moving");
            PlayWalkOrRun(movingAnimationClip);
            MoveEventStarted?.Invoke(movingAnimationClip);
        }

        protected virtual void OnMoveEventStopped(string stopAnimationClip)
        {
            PlayNow(stopAnimationClip);
            Debug.Log("on stop");
            MoveEventStopped?.Invoke(stopAnimationClip);
        }


        protected void SmoothMove(Vector3 target, string moveAnimClipName,string stopAnimClipName)
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

            //Debug.DrawLine(transform.position, target, Color.red, 30, false);


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
                    anim.Stop("Run");
                    OnMoveEventStopped(stopAnimClipName);

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
