using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayPanel : APanel
{
    public StatsViewManager statsViewManager;
    public ZonesViewManager zonesViewManager;
    public StationsViewManager stationsViewManager;
    public EffectsViewManager effectsViewManager;

    public override void BeforeShow()
    {
        base.BeforeShow();
        GameManager.Instance.StartGame();
    }

    public void UpdateViews()
    {
        statsViewManager.UpdateStats();
        zonesViewManager.UpdateZones();
        effectsViewManager.UpdateViews();
    }

    public void UpdateEvents()
    {

    }

    public void QuitGame()
    {
        PanelTransitioner.Instance.Transition(PanelName, "main");
    }
}
