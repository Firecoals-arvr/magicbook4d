using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ActivationManager : MonoBehaviour
{
    //	[SerializeField] private GameObject greyImage;
    public UILabel textCode;
    public UILabel textPhoneNumber;
    public UILabel textPhoneNumberRestore;
    public const int LOL = 8;
    //[SerializeField] private InputField inputField;
    //	[SerializeField] private Button activateBtn;
    public static ActivationManager instance;

    private const int ID_ANIMAL = 1;
    private const int ID_SPACE = 2;
    private const int ID_COLOR = 3;

    private const char NAME_ANIMAL = 'A';
    private const char NAME_SPACE = 'B';
    private const char NAME_COLOR = 'C';
    private void Start()
    {
        MakeInstance();
    }
    void MakeInstance()
    {
        if (instance == null)
        {
            instance = this;
            //Debug.LogError("Make ActivationManager");
        }
    }

    bool IsRightProjectName(int Project_Id)
    {
        switch (Project_Id)
        {
            case ID_ANIMAL:
                return GetCode().ToCharArray()[0] == NAME_ANIMAL;
            case ID_SPACE:
                return GetCode().ToCharArray()[0] == NAME_SPACE;
            case ID_COLOR:
                return GetCode().ToCharArray()[0] == NAME_COLOR;
        }

        return true;
    }

    // check activation code
    private bool IsValidCode(int Project_Id)
    {
        if ((GetCode().Length != LOL) || !IsRightProjectName(Project_Id))
        {
            return false;
        }
        return true;
    }

    //public void MakeUpperCase()
    //{
    //    inputField.text = inputField.text.ToUpper();
    //}

    //check phone number
    private bool IsValidPhoneNumber()
    {
        if (GetPhoneNumber().Length < 8)
        {
            return false;
        }
        return true;
    }
    public bool IsValidPhoneNumberRestore()
    {
        if (GetPhoneNumberRestore().Length < 8)
        {
            return false;
        }
        return true;
    }

    ///<summary>
    ///Project_Id: Animal = 1, Sapce = 2, Color = 3
    ///</summary>
    public bool IsValidCodeAndPhoneNumber(int Project_Id)
    {
        
        return IsValidCode(Project_Id) && IsValidPhoneNumber();
    }

    public string GetCode()
    {
        return textCode.text.ToUpper();
    }

    public string GetPhoneNumber()
    {
        return textPhoneNumber.text;
    }
    public string GetPhoneNumberRestore()
    {
        return textPhoneNumberRestore.text;
    }

    public bool IsInvalidPhone()
    {
        return !IsValidPhoneNumber();
    }
    public bool IsInvalidPhoneRestore()
    {
        return !IsValidPhoneNumberRestore();
    }
}
