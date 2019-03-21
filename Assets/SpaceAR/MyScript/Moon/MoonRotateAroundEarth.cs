using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonRotateAroundEarth : MonoBehaviour
{
    public float speed = 10f;
    public GameObject earth;

    // Update is called once per frame
    void LateUpdate()
    {
       
        transform.RotateAround(earth.transform.position, Vector3.up, speed *10f * Time.deltaTime );
    }
}
