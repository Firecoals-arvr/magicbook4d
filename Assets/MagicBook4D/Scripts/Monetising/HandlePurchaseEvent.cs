using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Firecoals.Purchasing
{
    public class HandlePurchaseEvent : MonoBehaviour
    {
        public UIPanel spinner;
        void Start()
        {
            InAppPurchaseInGame.InitIAP();
            Initialize();
            StartCheckingLicense();
        }

        private void Initialize()
        {
            InAppPurchaseInGame.IAPOnPurchaseSuccess += OnPurchaseSuccess;
            InAppPurchaseInGame.IAPOnPurchaseFailed += OnPurchaseFailed;
        }
        private void StartCheckingLicense()
        {
            foreach (var projectId in new[] { "A", "B", "C" })
            {
                if (ActiveManager.IsActiveOfflineOk(projectId))
                {
                    if (InAppPurchaseInGame.IsInitialized() == false)
                    {
                        InAppPurchaseInGame.InitIAP();
                    }
                }
            }

        }
        private void OnPurchaseSuccess()
        {
            NGUITools.SetActive(spinner.gameObject, false);
            PopupManager.PopUpDialog("Thành công", "Cảm ơn bạn đã tin tưởng MagicBook 4D");
        }

        private void OnPurchaseFailed()
        {
            NGUITools.SetActive(spinner.gameObject, false);
            PopupManager.PopUpDialog("", "Đã xảy ra lỗi trong quá trình mua");
        }
        #region IN_APP_PURCHASE
        public void BuyAnimal()
        {
            PopupManager.PopUpDialog("Thế giới động vật", "Bạn có chắc chắn muốn mua không?",default, "Mua","Hủy bỏ",
                PopupManager.DialogType.YesNoDialog,
                () =>
                {
                    InAppPurchaseInGame.SetUPIAP(1);
                    InAppPurchaseInGame.BuyLicense();
                });

        }
        public void BuySpace()
        {
            PopupManager.PopUpDialog("Khám phá vũ trụ", "Bạn có chắc chắn muốn mua không?", default, "Mua", "Hủy bỏ",
                PopupManager.DialogType.YesNoDialog,
                () =>
                {
                    InAppPurchaseInGame.SetUPIAP(2);
                    InAppPurchaseInGame.BuyLicense();
                });

        }
        public void BuyColor()
        {
            PopupManager.PopUpDialog("Tô màu", "Bạn có chắc chắn muốn mua không?", default, "Mua", "Hủy bỏ",
                PopupManager.DialogType.YesNoDialog,
                () =>
                {
                    InAppPurchaseInGame.SetUPIAP(3);
                    InAppPurchaseInGame.BuyLicense();
                });

        }
        #endregion
    }


}
