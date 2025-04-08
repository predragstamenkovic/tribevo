using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsViewManager : MonoBehaviour
{
    [SerializeField]
    private StatView evoStat;
    [SerializeField]
    private StatView foodStat;
    [SerializeField]
    private StatView territoryStat;
    [SerializeField]
    private StatView discoveryStat;
    [SerializeField]
    private StatView hostilityStat;

    public void UpdateStats()
    {
        var tribe = GameManager.Instance.Tribe;
        evoStat.UpdateStat(tribe.Stats.Evolution, tribe.EvolutionPoints, tribe.EvolutionCap);
        foodStat.UpdateStat(tribe.Stats.PopGrowth, tribe.PopGrowthPoints, tribe.PopCap);
        territoryStat.UpdateStat(tribe.Stats.TerritoryGrowth, tribe.TerritoryPoints, tribe.TerritoryCap);
        discoveryStat.UpdateStat(tribe.Stats.DiscoverySpeed, tribe.DiscoveryPoints, tribe.DiscoveryCap);
        var hm = GameManager.Instance.HostilityManager;
        var diff = hm.HostilityDiff;
        hostilityStat.UpdateStat(diff, diff - hm.HostilityGauge[0], hm.HostilityGauge[hm.HostilityGauge.Count - 1]);
    }
}
