using System.Collections;
using System.Collections.Generic;
using Firecoals.Animal;
using UnityEngine;

public class WolfMove : MonoBehaviour
{
    public bool CanEat { get; set; }
    public float Jumpdistiance;
    public string movingAnimName;
    public string stopAnimName;
    public string IdleAnimName;
    public bool IsJump;
    public bool CanjumpWolf;
    public float timedelay = 1;
    public float SpeedJump = 2;
    protected Animator animator;
    public float MoveSpeed;
    public Vector3 DestinationPosition { get; set; }
    public bool IsMoving { get; set; }
    public bool CanMove { get; set; }
    protected bool IsRotating;
    public float StopDistance;
    public bool IsWolf;
    protected RaycastHit hit;
    public float ScaleAnimal { get; set; }
    public GameObject Item { get; set; }
    private bool Jumping { get; set; }
    protected void Start()
    {
        Item = transform.parent.GetComponentInChildren<Item>().gameObject;
        animator = GetComponent<Animator>();

    }
    protected void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0))//TODO && not hover UI
        {
            CanEat = false;
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (Vector3.Distance(transform.position, hit.point) > Jumpdistiance)
                {
                    IsJump = true;
                }
                if (hit.collider.gameObject)
                {
                    Debug.LogWarning("hit " + hit.collider.gameObject.name);
                }
                if (hit.collider.gameObject.layer == 9)
                {
                    if (!Jumping)
                    {
                        DestinationPosition = hit.point;
                        CanMove = true;
                        IsMoving = true;
                        if (Item != null)
                            Item.transform.position = hit.point;
                        //TODO SetActive(touch effect) = true
                    }
                }
            }
        }
    }
    protected void SmoothMove(Vector3 target, string moveAnimClipName, string stopAnimClipName, string idleAnimClipName)
    {
        IsRotating = false;
        var targetDir = target - this.transform.position;
        var angle = Quaternion.LookRotation(targetDir);
        if (Item != null)
        {
            Item.transform.rotation = angle;
        }

        if (IsRotating == false)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, angle, Time.deltaTime * 100.0f);
        }
        Debug.Log("IsJump" + IsJump);
        Debug.DrawLine(transform.position, target, Color.red, 30, false);
        if (Quaternion.Angle(angle, transform.rotation) < 5)
        {
            if (Vector3.Distance(transform.position, target) > Jumpdistiance * ScaleAnimal && Jumping == false)
            {
                transform.position = Vector3.MoveTowards(transform.position, target, MoveSpeed * Time.deltaTime * ScaleAnimal);
                animator.SetBool("IsWalk", true);
                CanjumpWolf = false;
                IsRotating = false;
            }
            else
            {
                Jumping = true;
                StartCoroutine(DelayJump(target));

            }

        }
        else
        {
            IsRotating = true;
        }

        if (Vector3.Distance(transform.position, target) < StopDistance * ScaleAnimal)
        {
            if (IsMoving)
            {
                animator.SetBool("IsWalk", false);
                IsMoving = false;
                CanMove = false;
                Jumping = false;
            }
        }
        else
        {
            IsMoving = true;
            CanMove = true;
        }

    }

    protected void Update()
    {
        if (CanMove)
        {
            ScaleAnimal = (float)this.transform.parent.transform.localScale.x;
            SmoothMove(DestinationPosition, movingAnimName, stopAnimName, IdleAnimName);
            Debug.Log("Canmove"+CanMove);
        }
    }

    IEnumerator DelayJump(Vector3 target)
    {
        animator.SetBool("IsJump", true);
        float speed = 0;
        yield return new WaitForSeconds(timedelay);
        speed = SpeedJump;
        IsJump = false;
        transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * speed * ScaleAnimal);
        speed = 0;
        animator.SetBool("IsJump", false);
        animator.SetBool("IsWalk", false);
        CanjumpWolf = true;
        Jumping = false;
    }

}
