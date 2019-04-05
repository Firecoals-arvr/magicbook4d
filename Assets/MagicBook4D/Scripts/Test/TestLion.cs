using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class TestLion : MonoBehaviour
{
    float maxJumpHeight = -2.0f;
    float groundHeight;
    Vector3 groundPos;
    float jumpSpeed = 7.0f;
    float fallSpeed = 6.0f;
    public bool inputJump = false;
    public bool grounded = true;
    public Animator anim;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        groundPos = transform.position;
        groundHeight = transform.position.z;
        maxJumpHeight = transform.position.z + maxJumpHeight;
        anim = GetComponent<Animator>();
    }
    public bool isGrounded;
    Rigidbody rb;
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == ("Ground") && isGrounded == false)
        {
            isGrounded = true;
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(new Vector3(0, 5, 0), ForceMode.Impulse);
            isGrounded = false;
            //if (grounded)
            //{
            //    groundPos = transform.position;
            //    inputJump = true;
            //    StartCoroutine("Jump");
            //}
        }
        if (transform.position == groundPos)
            grounded = true;
        else
            grounded = false;
    }

    IEnumerator Jump()
    {
        while (true)
        {
            if (transform.position.z <= maxJumpHeight)
                inputJump = false;
            if (inputJump)
            {
                anim.SetTrigger("IsJump");
                transform.Translate(Vector3.forward * jumpSpeed * Time.smoothDeltaTime);
            }
               
            else if (!inputJump)
            {
                if (transform.position == groundPos)
                    StopAllCoroutines();
            }

            yield return new WaitForEndOfFrame();
        }
    }
}
