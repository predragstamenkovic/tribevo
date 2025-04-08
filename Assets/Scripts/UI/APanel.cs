using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class APanel : MonoBehaviour
{
    [SerializeField]
    private string panelName;

    public string PanelName => panelName;

    public virtual void Show()
    {
        gameObject.SetActive(true);
    }

    public virtual void Hide()
    {
        gameObject.SetActive(false);
    }

    public virtual void BeforeShow()
    {
    }

    public virtual void BeforeHide()
    {
    }

    public virtual void AfterShow()
    {
    }

    public virtual void AfterHide()
    {
    }
}
