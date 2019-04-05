using UnityEngine;

namespace Firecoals.Purchasing
{
    public class ActivationManager : MonoBehaviour
    {
        //	[SerializeField] private GameObject greyImage;
        public UILabel textCode;
        public UILabel textPhoneNumber;
        public const int LOL = 8;
        //[SerializeField] private InputField inputField;
        //	[SerializeField] private Button activateBtn;
        public static ActivationManager instance;
        private void Start()
        {
            MakeInstance();
        }

        private void MakeInstance()
        {
            if (instance == null)
            {
                instance = this;
                //Debug.LogError("Make ActivationManager");
            }
        }

        private bool IsRightProjectName()
        {
            //switch (projectId)
            //{
            //    case "A":
            //        return GetCode().ToCharArray()[0].Equals('A');
            //    case "B":
            //        return GetCode().ToCharArray()[0].Equals('B');
            //    case "C":
            //        return GetCode().ToCharArray()[0].Equals('C');
            //    default:
            //        return true;
            //}

            return GetCode().ToCharArray()[0].Equals('A') || GetCode().ToCharArray()[0].Equals('B') || GetCode().ToCharArray()[0].Equals('C');
        }

        // check activation code
        private bool IsValidCode()
        {
            if ((GetCode().Length != LOL) || !IsRightProjectName())
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
            return GetPhoneNumber().Length >= 8 && GetPhoneNumber().Length <= 11;
        }

        ///<summary>
        ///Project_Id: Animal = 1, Sapce = 2, Color = 3
        ///</summary>
        public bool IsValidCodeAndPhoneNumber()
        {
            return IsValidCode() && IsValidPhoneNumber();
        }

        public string GetCode()
        {
            return textCode.text.ToUpper();
        }

        public string GetPhoneNumber()
        {
            return textPhoneNumber.text;
        }

        public bool IsValidPhone()
        {
            return IsValidPhoneNumber();
        }
    }


}
