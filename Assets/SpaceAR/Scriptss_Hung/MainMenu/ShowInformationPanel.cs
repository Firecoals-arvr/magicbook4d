using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Firecoals.Space
{
    public class ShowInformationPanel : MonoBehaviour
    {
        public GameObject mainPanel;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            HideObjectInfo();
        }

        public void ShowObjectInfo()
        {
            mainPanel.GetComponent<Transform>().DOLocalMove(new Vector3(0f, 344f, 0f), 1f);
        }

        private void HideObjectInfo()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    if (hit.transform.tag == "panelifnorNGUI")
                    {
                        Debug.LogError("Some information...");
                    }
                }
                else
                {
                    mainPanel.GetComponent<Transform>().DOLocalMove(new Vector3(0f, 1445f, 0f), 1f);
                }
            }
        }
    }
}
