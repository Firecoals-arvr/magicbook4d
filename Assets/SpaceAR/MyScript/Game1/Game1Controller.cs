using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Firecoals.Space
{
    /// <summary>
    /// class này để check điểm
    /// </summary>
    public class Game1Controller : MonoBehaviour
    {
        public GameObject game1, playBT, panelFail, panelTotal;
        float time = 60;
        public UILabel timeTxt, scoreTxt, panelTotalTimetxt, panelFailScoreTxt;
        bool isStart;
        int rightAnswer;
        public GameObject listPlanet;

        //DragObject _drag;

        // Start is called before the first frame update
        void Start()
        {
            //_drag = GameObject.FindObjectOfType<DragObject>();
            // set cho thời gian = 60
            timeTxt.text = time.ToString();
        }

        // Update is called once per frame
        void Update()
        {
            // nếu ấn nút start thì bắt đầu tính thời gian
            if (isStart == true)
            {
                if (time > 0)
                {
                    time -= Time.deltaTime;
                    timeTxt.text = Mathf.Round(time).ToString();
                }
                else
                {
                    CheckResult();
                    NGUITools.SetActive(panelFail, true);
                }
            }
        }
        public void PlayButton()
        {
            isStart = true;
            //game1 là thằng solarsystem con của thằng game1 to
            game1.transform.GetChild(0).gameObject.SetActive(true);
            NGUITools.SetActive(playBT, false);
        }
        // check để tính điểm
        public void CheckResult()
        {
            rightAnswer = 0;
            // list này là list các planet màu xanh 
            //listPlaet = GameObject.FindGameObjectWithTag("planettả");
            //listPlanet = GameObject.FindGameObjectWithTag("planetcontainer");
            for (int i = 0; i < listPlanet.transform.childCount; i++)
            {
                Debug.Log("child = " + listPlanet.transform.GetChild(i).childCount);
                if (listPlanet.transform.GetChild(i).childCount > 0)
                {
                    if (listPlanet.transform.GetChild(i).GetChild(0).name == listPlanet.transform.GetChild(i).name)
                    {
                        rightAnswer++;
                    }
                }
            }

            if (rightAnswer == 8)
            {
                isStart = false;
                scoreTxt.text = rightAnswer.ToString();
                panelTotalTimetxt.text = (60.0f - Mathf.Round(time) + "s").ToString();
                NGUITools.SetActive(panelTotal, true);
            }
            else
            {
                isStart = false;
                scoreTxt.text = rightAnswer.ToString();
                panelFailScoreTxt.text = rightAnswer.ToString();
                NGUITools.SetActive(panelFail, true);
            }

        }
        // nút restart thực hiện = cách xóa prefabs đi load lại trên bundle
        public void Restart()
        {
            isStart = false;
            time = 60;
            rightAnswer = 0;
            if (panelFail.activeInHierarchy)
            {
                NGUITools.SetActive(panelFail, false);
            }
            if (panelTotal.activeInHierarchy)
            {
                NGUITools.SetActive(panelTotal, false);
            }

            for (int i = 0; i < listPlanet.transform.childCount; i++)
            {
                Debug.Log("child = " + listPlanet.transform.GetChild(i).childCount);
                if (listPlanet.transform.GetChild(i).childCount > 0)
                {
                    listPlanet.transform.GetChild(i).GetChild(0).GetComponent<DragObject>().RestartGame();

                }
            }


            PlayButton();
        }
    }
}