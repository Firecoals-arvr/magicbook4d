using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SelectLanguage : MonoBehaviour
{
    public GameObject buttonSelect;

    public GameObject _vnflag;
    public GameObject _engflag;
    // Start is called before the first frame update

    public void ClickToChangeLanguage()
    {
        //NGUITools.SetActive(_vnflag, true);
        //NGUITools.SetActive(_engflag, true);

        _vnflag.transform.DOLocalMove(new Vector3(165f, 188f, 0f), 1f);
        _engflag.transform.DOLocalMove(new Vector3(260f, 0f, 0f), 1f);
    }

    public void ClickVNFlag()
    {
        buttonSelect.GetComponent<UI2DSprite>().sprite2D = _vnflag.GetComponent<UI2DSprite>().sprite2D;
        buttonSelect.GetComponent<UIButton>().normalSprite2D = _vnflag.GetComponent<UIButton>().normalSprite2D;
        buttonSelect.GetComponent<UIButton>().hoverSprite2D = _vnflag.GetComponent<UIButton>().hoverSprite2D;
        buttonSelect.GetComponent<UIButton>().pressedSprite2D = _vnflag.GetComponent<UIButton>().pressedSprite2D;
        buttonSelect.GetComponent<UIButton>().disabledSprite2D = _vnflag.GetComponent<UIButton>().disabledSprite2D;
        LanguageHasSelected();
    }

    public void ClickEngFlag()
    {
        buttonSelect.GetComponent<UI2DSprite>().sprite2D = _engflag.GetComponent<UI2DSprite>().sprite2D;
        buttonSelect.GetComponent<UIButton>().normalSprite2D = _engflag.GetComponent<UIButton>().normalSprite2D;
        buttonSelect.GetComponent<UIButton>().hoverSprite2D = _engflag.GetComponent<UIButton>().hoverSprite2D;
        buttonSelect.GetComponent<UIButton>().pressedSprite2D = _engflag.GetComponent<UIButton>().pressedSprite2D;
        buttonSelect.GetComponent<UIButton>().disabledSprite2D = _engflag.GetComponent<UIButton>().disabledSprite2D;
        LanguageHasSelected();
    }

    void LanguageHasSelected()
    {
        _vnflag.transform.DOLocalMove(Vector3.zero, 1f);
        _engflag.transform.DOLocalMove(Vector3.zero, 1f);

        //NGUITools.SetActive(_vnflag, false);
        //NGUITools.SetActive(_engflag, false);
    }
}
