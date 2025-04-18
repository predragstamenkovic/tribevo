using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Zone")]
public class Zone : ScriptableObject
{
    public int territorySize;
    public int territoryCount;
    public int minDiscoveries;
    public int maxDiscoveries;
    public int territoryCapIncrease;
    public int discoveryCapIncrease;
    public int hostility;
}
