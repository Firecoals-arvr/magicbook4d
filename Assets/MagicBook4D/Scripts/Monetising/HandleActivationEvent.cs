using UnityEngine;
namespace Firecoals.Purchasing
{
    public class HandleActivationEvent : MonoBehaviour
    {

        public UIButton activateCodeButton;
        public UIPanel spinning;
        public UILabel notifyPhonePage, notifyCodePage;

        private void Start()
        {

            Initial();
            //PlayerPrefs.DeleteKey("Project_A");
            //PlayerPrefs.DeleteKey("Project_B");
            //PlayerPrefs.DeleteKey("Project_C");
        }

        private void Initial()
        {

            ActiveManager.OnRegisterLicenseSuccess += OnRegisterLicenseSuccess;
            ActiveManager.OnRegisterLicenseFailure += OnRegisterLicenseFailure;

            ActiveManager.OnPlayerServerActived += OnPlayerServerActived;
            ActiveManager.OnPlayerServerNotActived += OnPlayerServerNotActived;

            ActiveManager.OnConnectionError += OnConnectionError;
        }


        //private void StartCheckingLicense()
        //{
        //    for (int i = 0; i < 3; ++i)
        //    {
        //        if (ActiveManager.IsActiveOfflineOk(i + 1) == true)
        //        {
        //            if (InAppPurchaseInGame.IsInitialized() == false)
        //            {
        //                InAppPurchaseInGame.InitIAP();
        //            }
        //            //if (ActivateChanged.instance != null)
        //            //    ActivateChanged.instance.ActivatedChange();// ActivatedChange();
        //        }
        //        else
        //        {
        //            if (ActivateChanged.instance != null)
        //                ActivateChanged.instance.NotActivatedChange();
        //            ActivateChanged.instance.NotActivatedChange();
        //        }
        //    }
        //}

        #region EVENT_HANDLE

        private void OnPlayerServerActived(string projectId)
        {
            notifyPhonePage.text = "Khôi phục thành công " + ActiveManager.ProjectIdToName(projectId);
            NGUITools.SetActive(spinning.gameObject, false);
        }

        private void OnPlayerServerNotActived()
        {
            NGUITools.SetActive(spinning.gameObject, false);
            notifyCodePage.text = "Khôi phục thất bại vui lòng kiểm tra lại số điện thoại";
            //DialogManager.instance.HideLoadingBar();
            //DialogManager.PopUpDialog(Localization.Get("FailDialogTitle"), Localization.Get("RestoreFail"), DialogManager.DialogType.OkDialog, null, null);
        }

        private void OnRegisterLicenseFailure(string msg)
        {
            //GetDialog("Loading").SetActive(false);
            //DialogManager.instance.HideLoadingBar();
            NGUITools.SetActive(spinning.gameObject, false);
            if (msg == "1")
            {
                //DialogManager.PopUpDialog(Localization.Get("FailDialogTitle"), Localization.Get("IncorrectCode"), DialogManager.DialogType.OkDialog);
                notifyCodePage.text = "Mã bạn vừa nhập không đúng, vui lòng thử lại";
            }
            else if (msg == "2")
            {
                //DialogManager.instance.HideLoadingBar();
                NGUITools.SetActive(spinning.gameObject, false);
                //Ma da duoc su dung
                PopupManager.PopUpDialog(string.Empty,
                    "Mã này  đã được kích hoạt, nếu bạn đã kích hoạt vui lòng khôi phục lại");
            }
            else
            {
                // Loi khong xac dinh
                notifyCodePage.text = "Lỗi không xác định";
                //DialogManager.PopUpDialog(Localization.Get("FailDialogTitle"), Localization.Get("Error"), DialogManager.DialogType.OkDialog);
            }
        }

        private void OnRegisterLicenseSuccess(string projectId)
        {
            //DialogManager.instance.HideLoadingBar();
            //DialogManager.PopUpDialog(Localization.Get("SuccessDialogTitle"), Localization.Get("activesucceed"), DialogManager.DialogType.OkDialog, null, null);
            //ActiveManager.SaveActiveState();
            NGUITools.SetActive(spinning.gameObject, false);
            notifyCodePage.text = "";
            PopupManager.PopUpDialog("Thành công", "Kích hoạt thành công " + ActiveManager.ProjectIdToName(projectId));
        }



        private void OnConnectionError()
        {
            //DialogManager.instance.HideLoadingBar();
            NGUITools.SetActive(spinning.gameObject, false);
            PopupManager.PopUpDialog("Lỗi", "Không có kết nối mạng, vui lòng thử lại");
            //DialogManager.PopUpDialog(Localization.Get("FailDialogTitle"), Localization.Get("NetworkError"), DialogManager.DialogType.OkDialog, null, null);

        }

        #endregion




        #region CODE_ACTIVATION 

        #region RESTORE_PHONE_NUMBER

        private void RestoreCodeUsingPhonenumber()
        {
            if (!ActivationManager.instance.IsValidPhone())
            {
                PopupManager.PopUpDialog("Lỗi", "Số điện thoại bạn nhập không đúng, vui lòng thử lại");
            }
            else
            {
                //DialogManager.PopUpDialog(string.Empty, string.Empty, DialogManager.DialogType.LoadingBar);
                NGUITools.SetActive(spinning.gameObject, true);
                CM_JobQueue.Make().Enqueue(ActiveManager.CheckRestore(ActivationManager.instance.GetPhoneNumber(), "A"))
                    .Enqueue(ActiveManager.CheckRestore(ActivationManager.instance.GetPhoneNumber(), "B"))
                    .Enqueue(ActiveManager.CheckRestore(ActivationManager.instance.GetPhoneNumber(), "C")).Start()
                    .NotifyOnQueueStarted(
                        (object sender, CM_QueueEventArgs args) => { NGUITools.SetActive(spinning.gameObject, true); });

            }

        }

        public void RestoreCode()
        {
            if (Application.internetReachability != NetworkReachability.NotReachable)
            {
                RestoreCodeUsingPhonenumber();
            }
            else
            {
                //DialogManager.PopUpDialog(Localization.Get("FailDialogTitle"), Localization.Get("NetworkError"), DialogManager.DialogType.OkDialog, null, null);
                PopupManager.PopUpDialog("Lỗi", "Không có kết nối mạng, vui lòng thử lại");
            }
        }

        #endregion

        #region NEW_REGISTER 

        public void NewUser()
        {

            if (Application.internetReachability != NetworkReachability.NotReachable)
            {
                if (ActivationManager.instance.IsValidCodeAndPhoneNumber())
                {
                    //DialogManager.PopUpDialog(string.Empty, string.Empty, DialogManager.DialogType.LoadingBar);
                    NGUITools.SetActive(spinning.gameObject, true);
                    StartCoroutine(ActiveManager.RegisterUser(ActivationManager.instance.GetPhoneNumber(),
                        ActivationManager.instance.GetCode()[0].ToString(), ActivationManager.instance.GetCode()));
                }
                else
                {
                    //DialogManager.PopUpDialog(Localization.Get("FailDialogTitle"), Localization.Get("Invalid"), DialogManager.DialogType.OkDialog, null, null);
                    PopupManager.PopUpDialog("Lỗi", "Mã code bạn vừa nhập không hợp lệ");
                }
            }
            else
            {
                //DialogManager.PopUpDialog(Localization.Get("FailDialogTitle"), Localization.Get("NetworkError"), DialogManager.DialogType.OkDialog, null, null);
                PopupManager.PopUpDialog("Lỗi", "Không có kết nối mạng, vui lòng thử lại");
            }
        }

        #endregion

        #endregion

    }

}