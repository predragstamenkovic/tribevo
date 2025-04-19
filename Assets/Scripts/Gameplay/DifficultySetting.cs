using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/DifficultySetting")]
public class DifficultySetting : ScriptableObject
{
    public string diffName;
    public List<float> hostilityIncreasePerEra;
    public float hostilityIncreasePeriod;
    public TribeStats defaultStats;
}
