using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Firecoals.Space
{
    public class Randomrotate : MonoBehaviour
    {
        // 3 coordinates of Vector 3.
        float a;
        float b;
        float c;
        void Start()
        {
            StartCoroutine(Delay());
        }

        // Update is called once per frame
        void Update()
        {
            transform.Rotate(new Vector3(a, b, c) * 20f * Time.deltaTime);
        }

        private IEnumerator Delay()
        {
            yield return new WaitForSeconds(1f);
            a = Random.Range(-0.25f, 0.25f);
            b = Random.Range(-0.25f, 0.25f);
            c = Random.Range(-0.25f, 0.25f);
        }
    }
}
