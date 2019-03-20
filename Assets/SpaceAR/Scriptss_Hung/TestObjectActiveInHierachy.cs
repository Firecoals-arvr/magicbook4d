using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FireCoals.Space
{
    public class TestObjectActiveInHierachy : MonoBehaviour
    {
        GameObject[] planet;
        public UILabel labelObj;

        // Start is called before the first frame update
        void Start()
        {
            planet = GameObject.FindGameObjectsWithTag("planettarget");
        }

        // Update is called once per frame
        void Update()
        {
            CheckActive();
        }

        void CheckActive()
        {
            for (int i = 0; i < planet.Length; i++)
            {
                if (planet[i].transform.GetChild(0).gameObject.activeInHierarchy)
                {
                    labelObj.text = planet[i].transform.GetChild(0).name;
                }
            }

        }
    }
}
