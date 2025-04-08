using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZoneView : MonoBehaviour
{
    public Image zoneImage;
    public Image darkOverlay;
    public Image unlockOverlay;
    public int zoneIndex;

    public void FillZone()
    {
        darkOverlay.fillAmount = 0.0f;
        unlockOverlay.fillAmount = 0.0f;
    }
    public void HideZone()
    {
        darkOverlay.fillAmount = 1.0f;
        unlockOverlay.fillAmount = 1.0f;
    }

    public void UpdateZone()
    {
        float zonePercent = GameManager.Instance.TerritoryManager.CurrentZonePercent;
        float territoryPercent = GameManager.Instance.Tribe.TerritoryPoints / GameManager.Instance.Tribe.TerritoryCap;
        float discoveryZonePercent = GameManager.Instance.TerritoryManager.CurrentDiscoveryPercent;
        float discoveryPercent = GameManager.Instance.Tribe.DiscoveryPoints / GameManager.Instance.Tribe.DiscoveryCap;
        int currentZoneIndex = GameManager.Instance.TerritoryManager.CurrentZoneIndex;
        int currentDiscoveryIndex = GameManager.Instance.TerritoryManager.CurrentDiscoveryIndex;

        if (currentZoneIndex > zoneIndex)
            unlockOverlay.fillAmount = 0.0f;
        else if (currentZoneIndex == zoneIndex)
            unlockOverlay.fillAmount = 1.0f - territoryPercent / GameManager.Instance.TerritoryManager.CurrentZone.territoryCount - zonePercent;
        else
            unlockOverlay.fillAmount = 1.0f;

        if (currentDiscoveryIndex > zoneIndex)
            darkOverlay.fillAmount = 0.0f;
        else if (currentDiscoveryIndex == zoneIndex)
            darkOverlay.fillAmount = 1.0f - discoveryPercent / GameManager.Instance.TerritoryManager.CurrentZone.territoryCount - discoveryZonePercent;
        else
            darkOverlay.fillAmount = 1.0f;
    }
}
