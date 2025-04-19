using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StatEffect
{
    public bool isTemporary;
    public float activePeriod;
    public int priority;

    public int flatEvolution;
    public float mulEvolution;
    public int flatFood;
    public float mulFood;
    public int flatPopGrowth;
    public float mulPopGrowth;
    public int flatStationSlots;
    public float mulStationSlots;
    public int flatDiscoverySpeed;
    public float mulDiscoverySpeed;
    public int flatTerritoryGrowth;
    public float mulTerritoryGrowth;
    public int flatDefenceStrength;
    public float mulDefenceStrength;

    private float timer;

    public virtual bool CanApplyEffect()
    {
        return true;
    }

    public void StartEffect()
    {
        timer = activePeriod;
    }

    public void UpdateTimer()
    {
        timer--;
    }

    public float TimeLeft => timer;
    public bool IsTimerDone => timer <= 0;
    public float TimePercent => timer / activePeriod;
}
