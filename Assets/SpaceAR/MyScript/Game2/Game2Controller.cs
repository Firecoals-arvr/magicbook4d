using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Firecoals.Space
{
    public class Game2Controller : MonoBehaviour
    {
        List<GameObject> prefabs = new List<GameObject>();
        List<string> infor = new List<string>();
        public GameObject panelTotal;
        public GameObject panelAnswer;
        public List<UIButton> bt = new List<UIButton>();
        public Animator anim;
        public GameObject score;
        public UILabel timeText, scoretext;
        int questionNumber;
        int numberRightAns;
        float timeToPlay = 0;
        GameObject go;
        public GameObject gameO;
        public GameObject playBT;
        bool rightAnswer;
        bool setTimePlay;

        void Start()
        {
            CheckPrefabs();
			// gameO là thằng chứa toàn bộ model
            
            score.transform.GetChild(0).GetComponent<UILabel>().text = "0";
            score.transform.GetChild(2).GetComponent<UILabel>().text = "0";
            numberRightAns = 0;

        }
        void Update()
        {
            if (setTimePlay == true)
            {
                // set time trả lời câu hỏi đếm ngược từ 10-0
                timeText.text = (int)System.Math.Round((10 - (Time.time - timeToPlay)), 1) + "s";
                // hết thời gian báo trả lời sai
                if (System.Math.Round((10 - (Time.time - timeToPlay)), 1) == 0)
                {
                    setTimePlay = false;
                    timeText.text = "10s";
                    AnswerFalse();
                }
            }
        }
        void CheckPrefabs()
        {
			// là thằng có tên game 2 mà ko fai là image target
            var a = GameObject.FindGameObjectWithTag("Game2").transform.GetChild(0);
            if (a.transform.GetChild(1).childCount > 0)
            {
                for (int i = 0; i < a.transform.GetChild(1).childCount; i++)
                {
                    prefabs.Add(a.transform.GetChild(1).transform.GetChild(i).gameObject);
                }

                for (int i = 0; i < prefabs.Count; i++)
                {
                    var obj = prefabs[i].name;
                    infor.Add(obj);
                }
            }
        }


        public void PlayGame()
        {
            NGUITools.SetActive(playBT, false);
            RandomPrefabs();
        }

        void RandomPrefabs()
        {
            if (questionNumber < 10)
            {
                Debug.LogError("gameO = " + gameO.name);
                if (gameO.transform.childCount == 0)
                {
                    questionNumber++;
                    score.transform.GetChild(2).GetComponent<UILabel>().text = questionNumber.ToString();
                    setTimePlay = true;
                    timeToPlay = Time.time;
                    go = Instantiate(prefabs[Random.Range(0, prefabs.Count)], gameO.transform);
                    go.SetActive(true);
                    RandomAnswer();
                    CheckAnswer();
                }
                else
                {
                    Destroy(gameO.transform.GetChild(0).gameObject);
                    questionNumber++;
                    score.transform.GetChild(2).GetComponent<UILabel>().text = questionNumber.ToString();
                    setTimePlay = true;
                    timeToPlay = Time.time;
                    go = Instantiate(prefabs[Random.Range(0, prefabs.Count)], gameO.transform);
                    go.SetActive(true);
                    RandomAnswer();
                    CheckAnswer();
                }
            }
            else
            {
                NGUITools.SetActive(panelTotal, true);
                scoretext.text = numberRightAns.ToString();

            }
        }
        void RandomAnswer()
        {
            NGUITools.SetActive(panelAnswer, true);
            rightAnswer = false;
            for (int i = 0; i < 4; i++)
            {
                panelAnswer.transform.GetChild(i).GetComponentInChildren<UILabel>().text = infor[Random.Range(0, infor.Count)];
                if (i == 0)
                {
                    while (panelAnswer.transform.GetChild(0).GetComponentInChildren<UILabel>().text == panelAnswer.transform.GetChild(1).GetComponentInChildren<UILabel>().text || panelAnswer.transform.GetChild(0).GetComponentInChildren<UILabel>().text == panelAnswer.transform.GetChild(2).GetComponentInChildren<UILabel>().text || panelAnswer.transform.GetChild(0).GetComponentInChildren<UILabel>().text == panelAnswer.transform.GetChild(3).GetComponentInChildren<UILabel>().text)
                    {
                        panelAnswer.transform.GetChild(0).GetComponentInChildren<UILabel>().text = infor[Random.Range(0, infor.Count)];

                    }
                    if (panelAnswer.transform.GetChild(i).GetComponentInChildren<UILabel>().text == go.name.Replace("(Clone)", ""))
                    {
                        rightAnswer = true;
                    }
                }
                if (i == 1)
                {
                    while (panelAnswer.transform.GetChild(1).GetComponentInChildren<UILabel>().text == panelAnswer.transform.GetChild(0).GetComponentInChildren<UILabel>().text || panelAnswer.transform.GetChild(1).GetComponentInChildren<UILabel>().text == panelAnswer.transform.GetChild(2).GetComponentInChildren<UILabel>().text || panelAnswer.transform.GetChild(1).GetComponentInChildren<UILabel>().text == panelAnswer.transform.GetChild(3).GetComponentInChildren<UILabel>().text)
                    {
                        panelAnswer.transform.GetChild(1).GetComponentInChildren<UILabel>().text = infor[Random.Range(0, infor.Count)];

                    }
                    if (panelAnswer.transform.GetChild(i).GetComponentInChildren<UILabel>().text == go.name.Replace("(Clone)", ""))
                    {
                        rightAnswer = true;
                    }
                }
                if (i == 2)
                {
                    while (panelAnswer.transform.GetChild(2).GetComponentInChildren<UILabel>().text == panelAnswer.transform.GetChild(1).GetComponentInChildren<UILabel>().text || panelAnswer.transform.GetChild(2).GetComponentInChildren<UILabel>().text == panelAnswer.transform.GetChild(0).GetComponentInChildren<UILabel>().text || panelAnswer.transform.GetChild(2).GetComponentInChildren<UILabel>().text == panelAnswer.transform.GetChild(3).GetComponentInChildren<UILabel>().text)
                    {
                        panelAnswer.transform.GetChild(2).GetComponentInChildren<UILabel>().text = infor[Random.Range(0, infor.Count)];

                    }
                    if (panelAnswer.transform.GetChild(i).GetComponentInChildren<UILabel>().text == go.name.Replace("(Clone)", ""))
                    {
                        rightAnswer = true;
                    }
                }
                if (i == 3)
                {
                    while (panelAnswer.transform.GetChild(3).GetComponentInChildren<UILabel>().text == panelAnswer.transform.GetChild(0).GetComponentInChildren<UILabel>().text || panelAnswer.transform.GetChild(3).GetComponentInChildren<UILabel>().text == panelAnswer.transform.GetChild(1).GetComponentInChildren<UILabel>().text || panelAnswer.transform.GetChild(3).GetComponentInChildren<UILabel>().text == panelAnswer.transform.GetChild(2).GetComponentInChildren<UILabel>().text)
                    {
                        panelAnswer.transform.GetChild(3).GetComponentInChildren<UILabel>().text = infor[Random.Range(0, infor.Count)];

                    }
                    if (panelAnswer.transform.GetChild(i).GetComponentInChildren<UILabel>().text == go.name.Replace("(Clone)", ""))
                    {
                        rightAnswer = true;
                    }
                }
            }
            if (rightAnswer == false)
            {
                panelAnswer.transform.GetChild(Random.Range(0, 4)).GetComponentInChildren<UILabel>().text = go.name.Replace("(Clone)", "");
            }
        }
        void CheckAnswer()
        {
            for (int i = 0; i < bt.Count; i++)
            {
                bt[i].onClick.Clear();
            }
            for (int i = 0; i < bt.Count; i++)
            {
                if (bt[i].GetComponentInChildren<UILabel>().text == go.name.Replace("(Clone)", ""))
                {

                    bt[i].onClick.Add(new EventDelegate(AnswerTrue));
                }
                else
                {
                    bt[i].onClick.Add(new EventDelegate(AnswerFalse));
                }
            }
        }
        void AnswerTrue()
        {
            Debug.Log("isthisin + ");
            anim.SetTrigger("True");
            numberRightAns++;
            score.transform.GetChild(0).GetComponent<UILabel>().text = numberRightAns.ToString();
            NextQuestion();
            Invoke("RandomPrefabs", 2);
        }
        void AnswerFalse()
        {
            anim.SetTrigger("False");

            NextQuestion();
            Invoke("RandomPrefabs", 2);
        }
        public void ResetGame()
        {
            NextQuestion();
            panelTotal.SetActive(false);
            questionNumber = 0;
            numberRightAns = 0;
            score.transform.GetChild(0).GetComponent<UILabel>().text = "0";
            score.transform.GetChild(2).GetComponent<UILabel>().text = "0";
            Invoke("RandomPrefabs", 0.5f);

        }
        void NextQuestion()
        {
            for (int i = 0; i < gameO.transform.childCount; i++)
            {
                Destroy(gameO.transform.GetChild(0).gameObject);
            }
            NGUITools.SetActive(panelAnswer, false);
        }
    }
}