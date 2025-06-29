using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tribe
{
    private TribeStats stats;
    private Dictionary<string, Evolution> evolutions;
    private Dictionary<string, Station> availableStations;
    private Dictionary<string, int> builtStations;
    private float evolutionPoints;
    private int popCount;
    private float popGrowthPoints;
    private int evoCount;
    private int evoPickCount = 2;
    private int popCap;
    private float discoveryPoints;
    private int discoveryCap;
    private float territoryPoints;
    private int territoryCap;
    private List<Territory> territories;

    public TribeStats Stats => stats;
    public float EvolutionPoints => evolutionPoints;
    public float EvolutionCap => GameManager.Instance.EvolutionsManager.GetEvolutionCap(evoCount);
    public float PopGrowthPoints => popGrowthPoints;
    public float PopCap => popCap;
    public float TerritoryPoints => territoryPoints;
    public float TerritoryCap => territoryCap;
    public float DiscoveryPoints => discoveryPoints;
    public float DiscoveryCap => discoveryCap;
    public IReadOnlyDictionary<string, Station> AvailableStations => availableStations;
    public IReadOnlyDictionary<string, int> BuiltStations => builtStations;

    public bool HasEvolution(string evoId)
    {
        return evolutions.ContainsKey(evoId);
    }

    public Tribe(DifficultySetting setting)
    {
        stats = new TribeStats(setting.defaultStats);
        evolutions = new Dictionary<string, Evolution>();
        availableStations = new Dictionary<string, Station>();
        builtStations = new Dictionary<string, int>();
        evoCount = 0;
        evolutionPoints = 0;
        popCount = 1;
        popGrowthPoints = 0;
        popCap = PopUtil.GetPopCap(popCount);
        discoveryPoints = 0;
        territoryPoints = 0;
        discoveryCap = GameManager.Instance.TerritoryManager.GetDiscoveryCap;
        territoryCap = GameManager.Instance.TerritoryManager.GetTerritoryCap;
        GameManager.Instance.EffectsManager.OnEffectEnd += OnEffectEnd;
    }

    public void BuildStation(Station station)
    {
        if (builtStations.ContainsKey(station.id))
            builtStations[station.id]++;
        else
            builtStations.Add(station.id, 1);

        for (int i = 0; i < station.effects.Count; i++)
        {
            if (station.effects[i].isTemporary)
            {
                GameManager.Instance.EffectsManager.RegisterGenericEffect(station.effects[i]);
                station.effects[i].StartEffect();
            }
            stats.ApplyEffect(station.effects[i]);
        }
    }

    private void OnEffectEnd(StatEffect effect)
    {
        stats.RemoveEffect(effect);
    }

    public void UpdateTribe()
    {
        evolutionPoints += stats.Evolution;
        popGrowthPoints += stats.Food - PopUtil.GetFoodRequirements(popCount);
        discoveryPoints += stats.DiscoverySpeed;
        territoryPoints += stats.TerritoryGrowth;

        int evoCap = GameManager.Instance.EvolutionsManager.GetEvolutionCap(evoCount);
        if (evolutionPoints >= evoCap)
        {
            evolutionPoints -= evoCap;
            var picks = GameManager.Instance.EvolutionsManager.GetEvolutionPicks(evoPickCount);
        }

        if (popGrowthPoints >= popCap)
        {
            popCount += stats.PopGrowth;
            popCap = PopUtil.GetPopCap(popCount);
        }
        else if (popGrowthPoints <= -popCap / 10)
        {
            popCount -= 1;
            popCap = PopUtil.GetPopCap(popCount);
            popGrowthPoints += popCap;
            if (popCount <= 0)
                GameManager.Instance.GameOver();
        }

        if (discoveryPoints >= discoveryCap)
        {
            var territory = GameManager.Instance.TerritoryManager.GetNextDiscovery();
            if (territory.DisoveryEvent != null)
            {
                var effects = territory.DisoveryEvent.effects;
                territory.DisoveryEvent.EventAction();
                for (int i = 0; i < effects.Count; i++)
                {
                    if (effects[i].isTemporary)
                    {
                        GameManager.Instance.EffectsManager.RegisterEventEffect(effects[i]);
                        effects[i].StartEffect();
                    }
                    stats.ApplyEffect(effects[i]);
                }
            }
            discoveryCap = GameManager.Instance.TerritoryManager.GetDiscoveryCap;
        }

        if (territoryPoints >= territoryCap)
        {
            var territory = GameManager.Instance.TerritoryManager.AcquireNextTerritory();
            if (territory != null)
            {
                var effects = territory.Effects;
                for (int i = 0; i < effects.Count; i++)
                {
                    if (effects[i].isTemporary)
                    {
                        GameManager.Instance.EffectsManager.RegisterGenericEffect(effects[i]);
                        effects[i].StartEffect();
                    }
                    stats.ApplyEffect(effects[i]);
                }
            }
            stats.AddStationSlots(territory.Size);
            territoryCap = GameManager.Instance.TerritoryManager.GetTerritoryCap;
            territories.Add(territory);
        }
        else if (territoryPoints < 0)
        {
            var territory = territories[territories.Count - 1];
            var effects = territory.Effects;
            for (int i = 0; i < effects.Count; i++)
            {
                if (!effects[i].isTemporary)
                    stats.RemoveEffect(effects[i]);
            }
            stats.AddStationSlots(-territory.Size);
            territories.RemoveAt(territories.Count - 1);
            territoryCap = GameManager.Instance.TerritoryManager.GetTerritoryCap;
            territoryPoints += territoryCap;
        }
    }

    public void PickedEvolution(Evolution evolution)
    {
        if (evolutions.ContainsKey(evolution.id))
            return;

        evolutions.Add(evolution.id, evolution);
        for (int i = 0; i < evolution.effects.Count; i++)
            stats.ApplyEffect(evolution.effects[i]);

        for (int i = 0; i < evolution.stations.Count; i++)
            if (!availableStations.ContainsKey(evolution.stations[i].id))
                availableStations.Add(evolution.stations[i].id, evolution.stations[i]);
    }
}
