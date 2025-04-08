using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZonesViewManager : MonoBehaviour
{
    public List<ZoneView> zoneViews;

    public void UpdateZones()
    {
        for (int i = 0; i < zoneViews.Count; i++)
        {
            zoneViews[i].UpdateZone();
        }
    }
}
