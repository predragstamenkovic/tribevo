using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuPanel : APanel
{
    public void OnPlay()
    {
        PanelTransitioner.Instance.Transition(PanelName, "gameplay");
    }
}
