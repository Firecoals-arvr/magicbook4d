using UnityEngine;

namespace Firecoals.Purchasing
{
    public class PassApple : MonoBehaviour
    {
        public UIButton activateCodeButton;
        private void Start()
        {
#if UNITY_IOS
        ActiveManager.OnShowActivationButton += OnShowActivationButton;
        ActiveManager.OnHideActivationButton += OnHideActivationButton;
#endif
        }

        private void OnHideActivationButton()
        {
            NGUITools.SetActive(activateCodeButton.gameObject, false);
        }
        private void OnShowActivationButton()
        {
            NGUITools.SetActive(activateCodeButton.gameObject, true);
        }
    }

}

