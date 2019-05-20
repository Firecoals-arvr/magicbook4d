using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Show tooltip UI
/// </summary>
public class TooltipWTF : MonoBehaviour
{
    public string text;
    public void PhoneTextField()
    {
        UITooltip.Show(text);
        StartCoroutine(HideTooltip());
    }
    IEnumerator HideTooltip()
    {
        yield return new WaitForSeconds(3f);
        UITooltip.Hide();
    }
}
