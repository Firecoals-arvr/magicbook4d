using UnityEngine;

namespace Firecoals.UI
{
    /// <summary>
    /// FishEye scroll effect apply to Scroll Elements
    /// </summary>
    public class FishEyeItem : MonoBehaviour
    {
        #region PRIVATE_VARIABLE

        private Transform _itemTransform;
        private UIPanel _scrollPanel;
        private UIWidget _itemWidget;
        private float _pos, _dis;

        #endregion
        #region PUBLIC_VARIABLE

        public float cellWidth = 300;
        public float downScale = 0.35f;

        #endregion

        #region MONOBEHAVIOUR_METHOD

        private UIModel3D model;
        private void Start()
        {
            _itemTransform = transform;
            _scrollPanel = _itemTransform.parent.parent.GetComponent<UIPanel>();
            _itemWidget = GetComponent<UIWidget>();
            model = GetComponentInChildren<UIModel3D>();
        }

        private void FixedUpdate()
        {
            _pos = _itemTransform.localPosition.x - _scrollPanel.clipOffset.x;
            _dis = Mathf.Clamp(Mathf.Abs(_pos), 0, cellWidth);
            _itemWidget.width = System.Convert.ToInt32(((cellWidth - _dis * downScale) / cellWidth) * cellWidth);
            var x = System.Convert.ToInt32(((cellWidth - _dis * downScale) / cellWidth) * cellWidth * 0.75f);
            model.transform.localScale = new Vector3(x, x, x);
        }
        #endregion

    }
}

