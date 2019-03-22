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

        protected void Update()
        {
            if (CanMove)
            {
                SmoothMove(TouchPosition, movingAnimName, stopAnimName);
            }
        }

    }
}
