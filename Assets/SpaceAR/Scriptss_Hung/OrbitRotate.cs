using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitRotate : MonoBehaviour
{
    public GameObject parentPlanet;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //transform.Rotate(new Vector3(0f, -5f, 0f) * speed * Time.deltaTime);
        MoveAroundPlanet();
    }

    void MoveAroundPlanet()
    {
        transform.RotateAround(parentPlanet.transform.position, Vector3.up, speed * Time.deltaTime);
    }
}
