using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HostilityManager: MonoBehaviour
{
    [SerializeField]
    private List<StatEffect> hostilityEffects;

    private List<int> hostilityGauge = new List<int>() { -10, -5, 0, 5, 10, 20 };
    private float hostilityPoints;
    private DifficultySetting difficulty;
    private int phase = 0;

    public void Setup(DifficultySetting difficulty)
    {
        this.difficulty = difficulty;
        hostilityPoints = 0;
    }

    public void UpdateHostility()
    {
        var gm = GameManager.Instance;
        if (phase > difficulty.hostilityIncreasePeriod)
        {
            hostilityPoints += difficulty.hostilityIncreasePerEra[gm.EraManager.CurrentEra];
            phase = 0;
        }

        phase++;
    }

    public StatEffect GetCurrentHostilityEffect()
    {
        var diff = GameManager.Instance.Tribe.Stats.DefenceStrength - GameManager.Instance.TerritoryManager.CurrentTerritoryHostility - hostilityPoints;
        for (int i = 0; i < hostilityGauge.Count; i++)
            if (hostilityGauge[i] >= diff)
                return hostilityEffects[i];

        return hostilityEffects[hostilityGauge.Count];
    }

    public IReadOnlyList<int> HostilityGauge => hostilityGauge;

    public float HostilityPoints => hostilityPoints;

    public float HostilityDiff => hostilityPoints + GameManager.Instance.TerritoryManager.CurrentTerritoryHostility - GameManager.Instance.Tribe.Stats.DefenceStrength;
}
