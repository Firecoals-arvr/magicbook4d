using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Showinfoshuttle : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }
    public GameObject infor1;
    public GameObject infor2;
    public GameObject infor3;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit) && (hit.collider.tag == "shuttle"))
            {
                switch (hit.transform.name)
                {
                    case "Body":
                        infor1.SetActive(true);
                        infor2.SetActive(false);
                        infor3.SetActive(false);
                        break;
                    case "Door":
                        infor1.SetActive(false);
                        infor2.SetActive(true);
                        infor3.SetActive(false);
                        break;
                    case "Engine":
                        infor1.SetActive(false);
                        infor2.SetActive(false);
                        infor3.SetActive(true);
                        break;
                    default:
                        break;
                }

            }
            else
            {
                infor1.SetActive(false);
                infor2.SetActive(false);
                infor3.SetActive(false);
            }
        }
    }
}
