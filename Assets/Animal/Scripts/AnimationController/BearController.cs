using System.Collections;
using System.Collections.Generic;
using Firecoals.Animal;
using UnityEngine;

namespace Firecoals.Animal
{
    [RequireComponent(typeof(Animation))]
    public class BearController : AnimalController
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
