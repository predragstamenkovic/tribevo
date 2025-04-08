using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultySetting : ScriptableObject
{
    public string diffName;
    public List<float> hostilityIncreasePerEra;
    public float hostilityIncreasePeriod;
    public TribeStats defaultStats;
}
