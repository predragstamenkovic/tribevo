using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Discovery")]
public class Discovery : ScriptableObject
{
    public GameEvent discoveryEvent;
    public List<StatEffect> territoryEffects;
    public int zone;
}
