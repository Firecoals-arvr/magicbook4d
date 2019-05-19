using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;

public class CheckPermionssionMenu : MonoBehaviour
{
    // Start is called before the first frame update
    private void Awake()
    {
        if (!Permission.HasUserAuthorizedPermission(Permission.Camera))
        {
            PopupManager.PopUpDialog("", "Bạn cần cấp quyền Camera để sử dụng tính năng này!", "OK", "Yes", "No", PopupManager.DialogType.YesNoDialog, () =>
            {
                Permission.RequestUserPermission(Permission.Camera);
                NativeGallery.RequestPermission();
            });
        }

    }
}
