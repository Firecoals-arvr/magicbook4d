using UnityEngine;

namespace Firecoals.SceneTransition
{
    public class LoadingSpinner : MonoBehaviour
    {
        public float speed = 5f;
        private float gondelarm_sin;

        private void Update()
        {

            //Quaternion newRotation = Quaternion.AngleAxis(180f, Vector3.forward);
            //transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, speed);
            //if (Quaternion.Angle(transform.rotation, newRotation) < 1)
            //{
            //    transform.rotation = Quaternion.AngleAxis(0, Vector3.back);
            //}
            float arm_rot = Mathf.Sin(gondelarm_sin) * 80.0f * 0.5f;
            gondelarm_sin += Time.deltaTime * speed;
            transform.localEulerAngles = new Vector3(0.0f, 0.0f, arm_rot);

        }

    }
}
