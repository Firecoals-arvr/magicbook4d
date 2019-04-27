using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Firecoals.Space
{
    [Serializable]
    public class InforObject
    {
        public string m_Label;
        public Transform m_Transform;
        public GameObject m_Panel;
        public GameObject m_Parents;
        public GameObject Panel
        {
            get
            {
                return m_Panel;
            }
            set
            {
                m_Panel = value;
            }

        }
        public string Label
        {
            get
            {
                return m_Label;
            }
            set
            {
                m_Label = value;
            }

        }
        public Transform transform
        {
            get
            {
                return m_Transform;
            }
            set
            {
                m_Transform = value;
            }

        }

        public GameObject Parent
        {
            get
            {
                return m_Parents;
            }
            set
            {
                m_Parents = value;
            }

        }
        public InforObject() { }
        public InforObject(GameObject _panel, string _label, Transform _transform)
        {
            this.m_Panel = _panel;
            this.m_Label = _label;
            this.m_Transform = _transform;
        }
    }
}