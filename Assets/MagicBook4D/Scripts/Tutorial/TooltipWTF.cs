using UnityEngine;

/// <summary>
/// Show tooltip UI
/// </summary>
public class TooltipWTF : MonoBehaviour
{
    public string text;
    public void PhoneTextField()
    {
        UITooltip.Show(text);
    }
}
