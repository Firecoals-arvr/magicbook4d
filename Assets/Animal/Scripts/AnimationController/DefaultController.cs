using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Firecoals.Animal
{
    [RequireComponent(typeof(Animation))]
    public class DefaultController : AnimalController
    {
        public string movingAnimName;
        public string stopAnimName;
        public string IdleAnimName;

        protected void Update()
        {
            if (CanMove)
            {
                ScaleAnimal = (float)this.transform.parent.transform.localScale.x;
                Debug.Log("Stop:"+StopDistance);
                SmoothMove(DestinationPosition, movingAnimName, stopAnimName, IdleAnimName);
            }
        }
    }
}
