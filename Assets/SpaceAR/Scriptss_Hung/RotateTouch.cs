using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTouch : MonoBehaviour
{
    public float speed;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnMouseDrag()
    {
        float rotX = Input.GetAxis("Mouse X") * Mathf.Deg2Rad * speed;
        transform.Rotate(new Vector3(0f, 5f, 0f), -rotX);
    }
}
