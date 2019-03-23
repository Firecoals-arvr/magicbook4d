using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDrag : MonoBehaviour
{
    void Start()
    {
        //defaultparent = gameObject.transform.parent.gameObject;
        defaultposition = gameObject.transform.localPosition;
        defaultlocalscale = gameObject.transform.localScale;
        defaultlocalrotation = gameObject.transform.localEulerAngles;
    }
    void Update()
    {

        // if(this.gameObject.transform.parent!=parentdefault.transform && this.gameObject.active==false)
        // {
        //  gameObject.transform.parent=parentdefault.transform;
        //  Debug.Log(defaulttranform.transform.position);
        //  if(defaulttranform!=null)
        //  {
        // 	 gameObject.transform.localPosition=defaulttranform.localPosition;
        // 	 gameObject.transform.localEulerAngles=defaulttranform.eulerAngles;
        // 	 gameObject.transform.localScale=defaulttranform.localScale;
        //  }
        // }



    }
    private GameObject defaultparent;
    Vector3 defaultposition;
    Vector3 defaultlocalscale;
    Vector3 defaultlocalrotation;
    private Vector3 screenPoint;
    private Vector3 offset;
    void OnMouseDown()
    {
        Debug.Log("ssss");
        gameObject.layer = 0; // ko hiểu

        // khi chạm vào planet thì ko cho nó xoay nữa
        //gameObject.transform.GetComponent<Autorotate>().enabled = false;
        //gameObject.transform.localEulerAngles=new Vector3(0,0,0);
        // set scale 
        gameObject.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        // nếu có con thì 
        if (gameObject.transform.childCount > 0)
        {
            //Debug.Log(gameObject.transform.childCount);

            gameObject.transform.GetChild(0).gameObject.layer = 0; // ko hiểu
                                                                   //nếu có cháu thì 
            if (gameObject.transform.GetChild(0).childCount > 0)
            {
                //set cho cháu có layer =0; (ko hiểu)
                for (int i = 0; i < gameObject.transform.GetChild(0).childCount; i++)
                {
                    gameObject.transform.GetChild(0).GetChild(i).gameObject.layer = 0;
                }
            }
        }
        //lấy vị trí của this.gameObject từ vị trí world ra vị trí screen
        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        //offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
        //set khoảng cách giữa this.gameObject và camera về vector 0
        offset = Vector3.zero;
        stt = false; // biến quản lý đối tượng j đó (chưa hiểu)
    }
    //khi kéo chuột
    void OnMouseDrag()
    {
        Debug.Log("dasdasd");
        //lấy vị trí của điểm đặt chuột trên màn hình từ 2D sang 3D. trục z dc set = vị trí của camera
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        //Debug.Log(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
        // đổi vị trí của điểm đặt chuột từ màn hình ra vị trí world space. offset đã nói ở trên
        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
        // set vị trí của this.gameObject về vị trí mới = new v3 (vị trí của điểm đặt chuột tại trục x, vị trí của this.gameObject tại trục y, vị trí của điểm đặt chuột tại trục z)
        transform.position = curPosition;
        //transform.position = new Vector3(curPosition.x, this.gameObject.transform.position.y, curPosition.z);

        //temp.position=transform.position;
    }
    //khi nhấc chuột lên
    void OnMouseUp()
    {
        Debug.Log("lol");
        // nếu đặt đúng vào thằng planet ?
        if (stt == true)
        {
            //set lại local position của gameObject về vector 0
            transform.localPosition = new Vector3(0, 0, 0);
            // set lại góc quay về vector 0
            transform.localEulerAngles = Vector3.zero;
            //  if(temp!=null)
            // 	 {
            //temp là thằng mẹ nào?
            temp.transform.GetComponent<CapsuleCollider>().radius = 0.01f;
            //  }
            //Debug.Log("set");
        }
        //nếu ko đặt đúng chỗ hoặc đang kéo trên đường rồi bỏ tay ra thì 
        else
        {
            //giống như start
            //this.gameObject.transform.SetParent(defaultparent.transform);
            this.gameObject.transform.localPosition = defaultposition;
            this.gameObject.transform.localScale = defaultlocalscale;
            this.gameObject.transform.localEulerAngles = defaultlocalrotation;
            this.gameObject.layer = 5;
            //this.gameObject.GetComponent<Autorotate>().enabled = true;
            if (gameObject.transform.childCount > 0)
            {
                if (gameObject.transform.GetChild(0).childCount > 0)
                {
                    for (int i = 0; i < gameObject.transform.GetChild(0).childCount; i++)
                    {
                        gameObject.transform.GetChild(0).GetChild(i).gameObject.layer = 5;
                    }
                }
            }


        }
        //  else
        //  {
        // 	 //Debug.Log(defaulttranform.localScale);
        // 	 //this.gameObject.transform.localScale=defaulttranform.localScale;
        //  }
    }
    //bien quan li scale cua collider khi xay ra va cham
    private GameObject temp;
    private Transform defaulttranform;
    //Bien quan li su kien dat object vao trung tam cua doi tuong cha(hanh tinh)
    private bool stt = false;
    /// <summary>
    /// OnTriggerEnter is called when the Collider other enters the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    /// <summary>
    /// OnTriggerStay is called once per frame for every Collider other
    /// that is touching the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Solar system")
        {

            //  if(this.gameObject.transform.localPosition!=Vector3.zero)
            //  {
            // 	 other.GetComponent<MeshRenderer>().material.SetFloat("_Outline",0.1f); // SetColor("OutlineColor",new Color(0,0,0,0)); //.color=new Color(1,1,1,1);
            // 	 //Debug.Log(other.GetComponent<MeshRenderer>().material.color);
            // 	 if(stt!=true)
            // 	stt=true;
            //  }


        }
    }
    // check va chạm
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter" + other.name);
        //nếu chạm vào thằng có tag là solar system
        if (other.tag == "Solar system")
        {
            Debug.Log("Solar systemr");
            //nếu thằng có tag ko có con
            if (other.gameObject.transform.childCount == 0)
            {
                //Debug.Log("other.gameObject.transform.childCount Triggerenter");
                // ???
                stt = true;
                //đặt lại thằng này vào làm con của thằng other 
                this.gameObject.transform.parent = other.gameObject.transform;
                //set local scale 
                this.gameObject.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
                // set thằng temp = thằng other 
                temp = other.gameObject;
            }
            //ngược lại nếu thằng này có con
            else
            {
                //nếu thằng other là con của thằng this.gameObject
                if (other.gameObject.transform.GetChild(0).name == gameObject.name)
                {
                    stt = true;
                    //set lại thằng bố của nó Đối lưu
                    this.gameObject.transform.parent = other.gameObject.transform;
                    this.gameObject.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
                    temp = other.gameObject;
                }
                //  else
                //  {
                // 	other.gameObject.transform.GetChild(0).SetParent(defaultparent.transform);
                // 	other.gameObject.transform.GetChild(0).localPosition=defaultposition;
                // 	other.gameObject.transform.GetChild(0).localScale=defaultlocalscale;
                // 	other.gameObject.transform.GetChild(0).localEulerAngles=defaultlocalrotation;
                // 	other.gameObject.transform.GetChild(0).gameObject.layer=5;
                // 	other.gameObject.transform.GetChild(0).GetComponent<Autorotate>().enabled=true;
                // 	if(other.transform.GetChild(0).childCount>0)
                // 	{
                // 		if(other.transform.GetChild(0).GetChild(0).childCount>0)
                // 		{
                // 			for(int i=0;i<other.transform.GetChild(0).GetChild(0).childCount;i++)
                // 			{
                // 			other.transform.GetChild(0).GetChild(0).GetChild(i).gameObject.layer=5;
                // 			}
                // 		}
                // 	}
                // 	stt=true;
                // 	this.gameObject.transform.parent=other.gameObject.transform;
                // 	this.gameObject.transform.localScale=new Vector3(1.1f,1.1f,1.1f);
                // 	temp=other.gameObject;
                //  }
            }

        }
    }
    /// <summary>
    /// OnTriggerExit is called when the Collider other has stopped touching the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Solar system")
        {
            if (temp != null)
            {
                //other.GetComponent<MeshRenderer>().material.SetColor("OutlineColor",new Color(0,0,0,1));
                //other.GetComponent<MeshRenderer>().material.SetFloat("_Outline",0);
                temp.transform.GetComponent<CapsuleCollider>().radius = 1;
                temp = null;
            }
            if (stt != false)
                stt = false;
        }
    }
}
