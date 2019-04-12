using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Firecoals.Space
{
    public class TestObjectActiveInHierachy : MonoBehaviour
    {
        /// <summary>
        /// danh sách object
        /// </summary>
        GameObject[] planet;

        /// <summary>
        /// label hiển thị tên object
        /// </summary>
        public UILabel labelObj;

        // Start is called before the first frame update
        void Start()
        {
            planet = GameObject.FindGameObjectsWithTag("Planet");
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
                if (planet[i].gameObject.activeInHierarchy)
                {
                    labelObj.text = planet[i].name;
                }
            }

        }
    }
}
