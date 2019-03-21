using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MagicBook_FireCoals
{
    public class LookAtSolar : MonoBehaviour
    {
        
        public float rotationSpeed = 20;
        
        void Update()
        {
            
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(-Camera.main.transform.position + transform.position, Vector3.up), rotationSpeed * Time.deltaTime);
        }
    }
}
