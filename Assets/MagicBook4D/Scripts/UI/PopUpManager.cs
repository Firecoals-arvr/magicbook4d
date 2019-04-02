using UnityEngine;


public class PopupManager : MonoBehaviour
{
    // Singleton reference
    private static PopupManager _instance;
    #region PUBLIC_VARIABLE
    public static bool isShuttingDown;
    //I made 2 types of dialogues, one that is only one OK Button
    //and another that has both an Yes and a No button which lead
    //to different callbacks.
    public enum DialogType
    {
        OkDialog,
        YesNoDialog,
    }

    //Game object for the OK only dialog box
    public GameObject okDialogObject;
    //Game Object for the YesNO dialog box
    public GameObject yesNoDialogObject;

    //Here go the dialog texts for both dialogs.
    public UILabel[] dialogText;
    //Here go the tialog titles for both dialogs.
    public UILabel[] dialogTitle;

    //We're going to use a void delegate for the callbacks
    public delegate void dialogAnswer();
    //We have one for ok/yes
    public dialogAnswer okAnswer;
    //And one for no
    public dialogAnswer noAnswer;

    //Bool to check if there is already a dialog currently showing.
    public static bool showingDialog;

    #endregion

    // Public getter for the singleton reference
    public static PopupManager Instance
    {
        get
        {
            // If there is no instance for DialogManager yet and we're not shutting down at the moment
            if (_instance == null && !isShuttingDown)
            {
                //Try finding and instance in the scene
                _instance = GameObject.FindObjectOfType<PopupManager>();
                //If no instance was found, let's create one
                if (!_instance)
                {
                    GameObject singleton = (GameObject)Instantiate(Resources.Load("PopupManager"), GameObject.FindObjectOfType<UIRoot>().transform);
                    singleton.name = "PopupManager";
                    _instance = singleton.GetComponent<PopupManager>();
                }
                //Set the instance to persist between levels.
                //DontDestroyOnLoad(_instance.gameObject);
            }
            //Return an instance, either that we found or that we created.
            return _instance;
        }
    }



    //Unity calls this function when quitting, I'm using that info to avoid creating
    //something when the game is quitting as unity doesn't like that.
    private void OnApplicationQuit()
    {
        isShuttingDown = true;
    }

    private void Awake()
    {
        //If there is no instance of this currently in the scene
        if (_instance == null)
        {
            //Set ourselves as the instance and mark us to persist between scenes
            _instance = this;
            //DontDestroyOnLoad(this);
        }
        else
        {
            //If there is already an instance of this and It's not me, then destroy me as there should only be one.
            if (this != _instance)
                Destroy(gameObject);
        }
    }

    /// <summary>
    /// This is the method we'll call to show the dialog. It takes a string for title, one for text,
    /// a dialog type (Ok only or Yes/No) and 2 callbacks, one for OK/Yes and the other one for NO
    /// </summary>
    /// <param name="_title">Title of the dialog box</param>
    /// <param name="_text">Text contained in the dialog box</param>
    /// <param name="_desiredDialog">Type of the Dialog Box, either DialogType.OkDialog or DialogType.YesNoDialog</param>
    /// <param name="_dialogAnswer">Callback to call if user pressed [Ok] or [Yes] buttons</param>
    /// <param name="_dialogNegativeAnswer">Callback to call if the user presses the [No] button</param>
    public static void PopUpDialog(string _title, string _text, DialogType _desiredDialog = DialogType.OkDialog, dialogAnswer _dialogAnswer = null, dialogAnswer _dialogNegativeAnswer = null)
    {
        //instance.Caculate (_text);
        if (showingDialog)
        {
            Instance.okDialogObject.SetActive(false);
            Instance.yesNoDialogObject.SetActive(false);
        }
        //Set the showing dialog bool to true to prevent another dialog over this.
        showingDialog = true;

        //Set our dialog boxes to show or not show based on it's desired type.
        switch (_desiredDialog)
        {
            case DialogType.OkDialog:
                //Active and tween scale
                Instance.okDialogObject.SetActive(true);
                var x = TweenScale.Begin(Instance.okDialogObject, 0.3f, Vector3.one);
                x.method = UITweener.Method.EaseInOut;
                x.Play(true);
                Instance.yesNoDialogObject.SetActive(false);

                break;
            case DialogType.YesNoDialog:
                Instance.okDialogObject.SetActive(false);
                Instance.yesNoDialogObject.SetActive(true);
                //Tween scale animation
                var y = TweenScale.Begin(Instance.yesNoDialogObject, 0.3f, Vector3.one);
                y.method = UITweener.Method.EaseOut;
                y.Play(true);
                break;
        }

        //Fill all the texts with the desired text.
        foreach (var text in Instance.dialogText)
        {
            text.text = _text;
        }

        //Fill all the titles with the desired title.
        foreach (var title in Instance.dialogTitle)
        {
            title.text = _title;
        }

        //Set our callbacks to the ones we received.
        Instance.okAnswer = _dialogAnswer;
        Instance.noAnswer = _dialogNegativeAnswer;
    }

    public bool AnswerYes => true;

    public bool AnswerNo => false;

    public void DismissDialog(bool _answer)
    {
        //If answer is true call YES/OK delegate if one exists
        if (_answer)
        {
            okAnswer?.Invoke();
        }
        else
        {
            //If answer is false call NO delegate if one exists.
            noAnswer?.Invoke();
        }
        //Restart local scale to zero for Tween Scale
        Instance.okDialogObject.transform.localScale = Vector3.zero;
        Instance.yesNoDialogObject.transform.localScale = Vector3.zero;


        //Hide the gameobjects and set the showingDialog back to false to allow for new dialog calls.
        okDialogObject.SetActive(false);
        yesNoDialogObject.SetActive(false);
        showingDialog = false;
    }
    public void HideAll()
    {
        okDialogObject.SetActive(false);
        yesNoDialogObject.SetActive(false);
        showingDialog = false;
    }


}
