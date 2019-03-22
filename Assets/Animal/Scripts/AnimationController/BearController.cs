using System.Collections;
using System.Collections.Generic;
using Firecoals.Animal;
using UnityEngine;

namespace Firecoals.Animal
{
    [RequireComponent(typeof(Animation))]
    public class BearController : AnimalController
    {
      /// <summary>
      /// 
      /// </summary>
        protected void Update()
        {
            if (CanMove)
            {
                SmoothMove(TouchPosition);
            }
        }

    }
}
