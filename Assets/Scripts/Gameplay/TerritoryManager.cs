using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TerritoryManager : MonoBehaviour
{
    [SerializeField]
    private List<Discovery> discoveries;
    [SerializeField]
    private List<Zone> zones;

    public const int ZONE_COUNT = 6;
    public const int BASE_T_ZONE_CAP = 20;
    public const int BASE_D_ZONE_CAP = 10;
    private List<List<Territory>> territories;
    private List<List<Discovery>> discoveriesPerZone;

    private int nextTerritory = 0;
    private int nextDiscovery = 0;
    private int currentTerritoryZone = 0;
    private int currentDiscoveryZone = 0;

    public void Setup()
    {
        discoveriesPerZone = new List<List<Discovery>>();
        for (int i = 0; i < ZONE_COUNT; i++)
            discoveriesPerZone.Add(new List<Discovery>());

        for (int i = 0; i < discoveries.Count; i++)
            discoveriesPerZone[discoveries[i].Zone].Add(discoveries[i]);
    }

    public void GenerateMap()
    {
        nextTerritory = 0;
        nextDiscovery = 0;
        currentTerritoryZone = 0;
        currentDiscoveryZone = 0;
        territories = new List<List<Territory>>();
        for (int i = 0; i < ZONE_COUNT; i++)
        {
            var zone = zones[i];
            int discoveryCount = Random.Range(zone.minDiscoveries, zone.maxDiscoveries + 1);
            List<Discovery> discoveries = new List<Discovery>();
            for (int d = 0; d < discoveryCount; d++)
                discoveries.Add(GameManager.Instance.EventsManager.PopDiscovery(i));
            var territoryIndexes = new List<int>();
            for (int t = 0; t < zone.territoryCount; t++)
                territoryIndexes.Add(t);
            territoryIndexes = territoryIndexes.Shuffle().ToList();

            territories.Add(new List<Territory>());
            for (int t = 0; t < zone.territoryCount; t++)
            {
                Discovery discovery = null;
                var index = territoryIndexes.IndexOf(t);
                if (index > -1)
                    discovery = discoveries[index];
                territories[i].Add(new Territory(zone.territorySize, discovery));
            }
        }
    }

    public Territory GetNextDiscovery()
    {
        var territory = territories[currentDiscoveryZone][nextDiscovery];
        nextDiscovery++;
        if (territories[currentDiscoveryZone].Count == nextDiscovery)
        {
            nextDiscovery = 0;
            currentDiscoveryZone++;
        }
        return territory;
    }

    public Territory AcquireNextTerritory()
    {
        if (!HasFreeDiscoveredTerritory)
            return null;

        var territory = territories[currentTerritoryZone][nextTerritory];
        nextTerritory++;
        if (territories[currentTerritoryZone].Count == nextTerritory)
        {
            nextTerritory = 0;
            currentTerritoryZone++;
        }
        return territory;
    }

    public int GetDiscoveryCap
    {
        get
        {
            int cap = BASE_D_ZONE_CAP;
            for (int i = 0; i < currentDiscoveryZone; i++)
                cap += zones[i].discoveryCapIncrease * zones[i].territoryCount;

            cap += zones[currentDiscoveryZone].discoveryCapIncrease * nextDiscovery;

            return cap;
        }
    }

    public int GetTerritoryCap
    {
        get
        {
            int cap = BASE_T_ZONE_CAP;
            for (int i = 0; i < currentTerritoryZone; i++)
                cap += zones[i].territoryCapIncrease * zones[i].territoryCount;

            cap += zones[currentTerritoryZone].territoryCapIncrease * nextTerritory;

            return cap;
        }
    }

    public int NextTerritory => nextTerritory;
    public int CurrentZoneIndex => currentTerritoryZone;
    public float CurrentZonePercent => nextTerritory / zones[currentTerritoryZone].territoryCount;
    public int NextDiscovery => nextDiscovery;
    public int CurrentDiscoveryIndex => currentDiscoveryZone;
    public float CurrentDiscoveryPercent => nextDiscovery / zones[currentDiscoveryZone].territoryCount;
    public bool HasFreeDiscoveredTerritory => currentDiscoveryZone >= currentTerritoryZone && nextDiscovery > nextTerritory;
    public Zone CurrentZone => zones[currentTerritoryZone];
    public Zone CurrentDiscoveryZone => zones[currentDiscoveryZone];
    public float CurrentTerritoryHostility
    {
        get
        {
            float hostility = 0;
            for (int i = 0; i < currentTerritoryZone; i++)
                hostility += zones[i].hostility * zones[currentTerritoryZone].territoryCount;

            hostility += zones[currentTerritoryZone].hostility * nextTerritory;
            return hostility;
        }
    }
}
