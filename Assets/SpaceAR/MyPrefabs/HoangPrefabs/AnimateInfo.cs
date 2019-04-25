using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Firecoals.Space
{
    public class AnimateInfo : MonoBehaviour
    {
        public InforObject[] InfoLeft;
        public InforObject[] InfoRight;
        List<GameObject> lst = new List<GameObject>();
        private Animator animator;
        
        private void Start()
        {

        }
        public void InstantiatePanel()
        {
            for (int i = 0; i < InfoLeft.Length; i++)
            {
                Debug.Log("size of InfoLeft: "+InfoLeft.Length);
                GameObject go = Instantiate(InfoLeft[i].m_Panel, InfoLeft[i].m_Transform.position, Quaternion.identity, InfoLeft[i].m_Parents.transform);
                animator = go.GetComponent<Animator>();
                go.transform.localRotation = Quaternion.AngleAxis(0, Vector3.zero);
                //go.transform.localScale = GetComponentInParent<Transform>().localScale;
                go.GetComponentInChildren<UILocalize>().key = InfoLeft[i].m_Label;
                go.GetComponentInChildren<UILabel>().text = Localization.Get(InfoLeft[i].m_Label);
                go.name= InfoLeft[i].m_Label;
                lst.Add(go);
            }
        }

        public void InstantiatePanelRight()
        {
            for (int i = 0; i < InfoRight.Length; i++)
            {
                Debug.Log("size of InfoRight: " + InfoLeft.Length);
                GameObject go = Instantiate(InfoRight[i].m_Panel, InfoRight[i].m_Transform.position, Quaternion.identity, InfoRight[i].m_Parents.transform);
                animator = go.GetComponent<Animator>();
                go.transform.localRotation = Quaternion.AngleAxis(0, Vector3.zero);
                go.GetComponentInChildren<UILocalize>().key = InfoRight[i].m_Label;
                //go.transform.localScale = GetComponentInParent<Transform>().localScale;
                go.GetComponentInChildren<UILabel>().text = Localization.Get(InfoRight[i].m_Label);
                go.name = InfoLeft[i].m_Label;
                lst.Add(go);
            }
        }

        public void DestroyAll()
        {
            foreach(GameObject go in lst)
            {
                Destroy(go);
            }
            //Destroy(GameObject.Find("Lop1(Clone)"));
            //Destroy(GameObject.Find("Lop2(Clone)"));
        }

    }
}
