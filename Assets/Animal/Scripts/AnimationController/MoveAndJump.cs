using System.Collections;
using System.Collections.Generic;
using Firecoals.Animal;
using UnityEngine;

public class MoveAndJump : MonoBehaviour
{
    public bool CanEat { get; set; }
    public float Jumpdistiance;
    public string movingAnimName;
    public string stopAnimName;
    public string IdleAnimName;
    public bool IsJump;
    public bool CanjumpWolf;
    public float timedelay=1;
    public float SpeedJump=2;
    protected Animator animator;
    public float MoveSpeed;
    public Vector3 DestinationPosition { get; set; }
    public bool IsMoving { get; set; }
    public bool CanMove { get; set; }
    protected bool IsRotating;
    public float StopDistance;
    public bool IsWolf;
    public bool IsEgale;
    protected RaycastHit hit;
    public GameObject Item { get; set; }

    protected  void Start()
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
                if (hit.collider.gameObject)
                {
                    Debug.LogWarning("hit " + hit.collider.gameObject.name);
                }
                if (hit.collider.gameObject.layer == 9)
                {
                    if (IsWolf)
                    {
                        if (CanjumpWolf)
                        onClick(hit);
                    }
                    else
                    {
                       onClick(hit);
                    }
                }
            }
        }
    }
    void onClick(RaycastHit hit)
    {
        Debug.LogWarning("hit " + hit.point + " " + hit.collider.gameObject.name);
        DestinationPosition = hit.point;
        CanMove = true;
        IsMoving = true;
        if (Item != null)
        {
            if (!IsEgale)
            {
                Item.transform.position = hit.point;
            }
            else
            {
                Item.transform.position =new Vector3(hit.point.x,hit.point.y,hit.point.z-Jumpdistiance);
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

        Debug.DrawLine(transform.position, target, Color.red, 30, false);
        if (Quaternion.Angle(angle, transform.rotation) < 5)
        {

           // transform.position = Vector3.MoveTowards(transform.position, target, MoveSpeed * Time.deltaTime);
           if (Vector3.Distance(transform.position, target) > StopDistance)
           {
           }

           if (Vector3.Distance(transform.position, target) > Jumpdistiance && IsJump==false) 
            {
                transform.position = Vector3.MoveTowards(transform.position, target, MoveSpeed * Time.deltaTime);
                // OnMoveEventStarted(moveAnimClipName);
                animator.SetBool("IsJump", false);
                animator.SetBool("IsIdle", false);
                animator.SetBool("IsWalk", true);
                CanjumpWolf = false;
                IsRotating = false;
            }
            else
           {
            
               StartCoroutine(delayjump(target));
              
            }
        }
        else
        {
            IsRotating = true;
        }
        if (Vector3.Distance(transform.position, target) < StopDistance)
        {
            if (IsMoving)
            {
                IsMoving = false;
                CanMove = false;
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
            SmoothMove(DestinationPosition, movingAnimName, stopAnimName, IdleAnimName);
        }
    }
    IEnumerator delayjump(Vector3 target)
    {

        animator.SetBool("IsWalk", false);
        animator.SetBool("IsJump", true);
        transform.position = Vector3.MoveTowards(transform.position, target, MoveSpeed * Time.deltaTime * SpeedJump);
        yield return  new WaitForSeconds(timedelay);
        animator.SetBool("IsJump", false);
        animator.SetBool("IsIdle", true);
        CanjumpWolf = true;
    
  
       
    }

}
