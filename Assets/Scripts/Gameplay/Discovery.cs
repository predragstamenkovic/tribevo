using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Discovery")]
public class Discovery : ScriptableObject
{
    [SerializeField]
    private GameEvent _discoveryEvent;
    [SerializeField]
    private List<StatEffect> _territoryEffects;
    [SerializeField]
    private int _zone;

    public GameEvent DiscoveryEvent => _discoveryEvent;
    public IReadOnlyList<StatEffect> TerritoryEffects => _territoryEffects;
    public int Zone => _zone;

    public bool IsAvailable(GameManager gameManager)
    {
        return _discoveryEvent.IsEventAvailable() && gameManager.TerritoryManager.CurrentZoneIndex == _zone;
    }

    public bool IsAvailable(int zone)
    {
        return _discoveryEvent.IsEventAvailable() && zone == _zone;
    }
}
