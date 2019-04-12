using UnityEngine;

public class RobotJoystickController : MonoBehaviour {

    public float moveSpeed;
    public float rotateSpeed;
    public Joystick joystick;
    public GameObject robotContainer;
       private Transform _camTransform;
    void Start()
    {
        // _camTransform = Camera.main.transform;
    }
	void Update () 
	{
        Vector3 moveVector = (transform.right * joystick.Horizontal + transform.forward * joystick.Vertical).normalized;
        //Debug.Log(moveVector);
        Vector3 directory=moveVector-transform.position;
        float angle=Mathf.Atan2(directory.y,directory.x)*Mathf.Rad2Deg;
        transform.Translate(moveVector * moveSpeed * Time.deltaTime*transform.localScale.x);
        //Debug.Log(moveVector);

        if(moveVector!=Vector3.zero)
        {
            robotContainer.GetComponentInChildren<Animation>().Play("Run");
            robotContainer.transform.rotation=Quaternion.LookRotation(moveVector*rotateSpeed*Time.deltaTime);
        }
        else
        {
            robotContainer.GetComponentInChildren<Animation>().Stop();
        }

        // if(moveVector!=Vector3.zero)
        // {
        // Vector3 rotatedDir = _camTransform.TransformDirection(moveVector);
        // rotatedDir = new Vector3(rotatedDir.x, 0, rotatedDir.z);
        // //rotatedDir = new Vector3(rotatedDir.x, rotatedDir.y,0 );
        // rotatedDir = rotatedDir.normalized * moveVector.magnitude;
        // float step = rotateSpeed * Time.deltaTime;
        // Vector3 newDir = Vector3.RotateTowards(transform.forward, rotatedDir, step, 0.0F);
        // if (rotatedDir != Vector3.zero)
        // {
        //     transform.rotation = Quaternion.LookRotation(newDir);
        // }
        // //     Quaternion rotation=Quaternion.AngleAxis(angle,Vector3.forward);
        // //    transform.rotation=Quaternion.Slerp(robotContainer.transform.rotation,rotation,rotateSpeed*Time.deltaTime);
        // }
        // if(moveVector!=Vector3.zero)
        // {
        // transform.rotation = Quaternion.LookRotation(moveVector*moveSpeed*Time.deltaTime);
        // // anima.Play("Walking");
        // // }
        // // else
        // // {
        // // if(anima.IsPlaying("Walking"))
        // // anima.Stop();
        // // if(anima.isPlaying==false)
        // // anima.Play("Idle");
        // // }
        // // if(stt==true)
        // // {
        // //     AlienIntro();
        // //     stt=false;
        // }
	}
    
}