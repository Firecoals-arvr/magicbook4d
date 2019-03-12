using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonRotateAroundEarth : MonoBehaviour
{
    public float speed = 1f;
    public GameObject earth;

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(earth.transform.position, Vector3.left, speed * Time.deltaTime);
    }
}
