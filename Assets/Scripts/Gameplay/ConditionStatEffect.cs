using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ConditionStatEffect : StatEffect
{
    public bool perEffect;
    public List<string> requiredEvolutions;
    public float requiredHostility;
    public float requiredPeriod;
    public TribeStats requiredStats;

    public override bool CanApplyEffect()
    {
        return true;
    }
}
