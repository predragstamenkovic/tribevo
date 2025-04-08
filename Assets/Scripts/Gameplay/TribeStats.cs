using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TribeStats
{
    private float evolution = 1.0f;
    private float food = 1.0f;
    private float popGrowth = 1.0f;
    private int stationSlots = 2;
    private float discoverySpeed = 1.0f;
    private float territoryGrowth = 1.0f;
    private float defenceStrength = 1.0f;

    private float evolutionPointsMultiplier = 1.0f;
    private float foodMultiplier = 1.0f;
    private float popGrowthMultiplier = 1.0f;
    private float stationSlotsMultiplier = 1.0f;
    private float discoverySpeedMultiplier = 1.0f;
    private float territoryGrowthMultiplier = 1.0f;
    private float defenceStrengthMultiplier = 1.0f;

    public TribeStats(TribeStats stats)
    {
        evolution = stats.evolution;
        food = stats.food;
        popGrowth = stats.popGrowth;
        stationSlots = stats.stationSlots;
        discoverySpeed = stats.discoverySpeed;
        territoryGrowth = stats.territoryGrowth;
        defenceStrength = stats.defenceStrength;
        evolutionPointsMultiplier = stats.evolutionPointsMultiplier;
        foodMultiplier = stats.foodMultiplier;
        popGrowthMultiplier = stats.popGrowthMultiplier;
        stationSlotsMultiplier = stats.stationSlotsMultiplier;
        discoverySpeedMultiplier = stats.discoverySpeedMultiplier;
        territoryGrowthMultiplier = stats.territoryGrowthMultiplier;
        defenceStrengthMultiplier = stats.defenceStrengthMultiplier;
    }

    public float Evolution => evolution * evolutionPointsMultiplier;
    public float Food => food * foodMultiplier;
    public int PopGrowth => Mathf.FloorToInt(popGrowth * popGrowthMultiplier);
    public int StationSlots => Mathf.FloorToInt(stationSlots * stationSlotsMultiplier);
    public float DiscoverySpeed => discoverySpeed * discoverySpeedMultiplier;
    public float TerritoryGrowth => territoryGrowth * territoryGrowthMultiplier;
    public float DefenceStrength => defenceStrength * defenceStrengthMultiplier;

    public void AddStationSlots(int count)
    {
        stationSlots += count;
    }

    public void ApplyEffect(StatEffect effect)
    {
        if (!effect.CanApplyEffect())
            return;

        evolution += effect.flatEvolution;
        evolutionPointsMultiplier += effect.mulEvolution;
        food += effect.flatFood;
        foodMultiplier += effect.mulFood;
        popGrowth += effect.flatPopGrowth;
        popGrowthMultiplier += effect.mulPopGrowth;
        stationSlots += effect.flatStationSlots;
        stationSlotsMultiplier += effect.mulStationSlots;
        discoverySpeed += effect.flatDiscoverySpeed;
        discoverySpeedMultiplier += effect.mulDiscoverySpeed;
        territoryGrowth += effect.flatTerritoryGrowth;
        territoryGrowthMultiplier += effect.mulTerritoryGrowth;
        defenceStrength += effect.flatDefenceStrength;
        defenceStrengthMultiplier += effect.mulDefenceStrength;
    }

    public void RemoveEffect(StatEffect effect)
    {
        evolution -= effect.flatEvolution;
        evolutionPointsMultiplier -= effect.mulEvolution;
        food -= effect.flatFood;
        foodMultiplier -= effect.mulFood;
        popGrowth -= effect.flatPopGrowth;
        popGrowthMultiplier -= effect.mulPopGrowth;
        stationSlots -= effect.flatStationSlots;
        stationSlotsMultiplier -= effect.mulStationSlots;
        discoverySpeed -= effect.flatDiscoverySpeed;
        discoverySpeedMultiplier -= effect.mulDiscoverySpeed;
        territoryGrowth -= effect.flatTerritoryGrowth;
        territoryGrowthMultiplier -= effect.mulTerritoryGrowth;
        defenceStrength -= effect.flatDefenceStrength;
        defenceStrengthMultiplier -= effect.mulDefenceStrength;
    }
}
