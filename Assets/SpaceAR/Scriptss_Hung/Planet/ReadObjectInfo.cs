using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Firecoals.Space
{
    /// <summary>
    /// hiển thị thông tin của object
    /// </summary>
    public class ReadObjectInfo : DefaultTrackableEventHandler
    {
        /// <summary>
        /// thông tin chung về object
        /// </summary>
        [SerializeField] UILocalize objectInfo;

        /// <summary>
        /// thông tin về các thành phần của object (nếu có)
        /// </summary>
        [SerializeField] UILocalize componentInfo;

        /// <summary>
        /// tên của object
        /// </summary>
        [SerializeField] UILocalize objectName;

        private string st1;
        private string st2;
        // Start is called before the first frame update
        protected override void Start()
        {

        }

        protected override void OnTrackingFound()
        {
            base.OnTrackingFound();

        }

        protected override void OnTrackingLost()
        {
            base.OnTrackingLost();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
        }
    }
}