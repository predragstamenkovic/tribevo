using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EventsManager : MonoBehaviour
{
    [SerializeField]
    private List<Discovery> _discoveries;
    [SerializeField]
    private List<EraEvent> _eraEvents;

    private Dictionary<string, Discovery> _discoveriesPool;
    private Dictionary<string, EraEvent> _eraEventsPool;

    private Dictionary<string, GameEvent> _happenedEvents;

    private void Awake()
    {
        PreparePool();
    }

    private void PreparePool(List<Discovery> discoveries = null, List<EraEvent> eraEvents = null)
    {
        var dList = discoveries == null ? _discoveries : discoveries;
        for (int i = 0; i < dList.Count; i++)
            _discoveriesPool.Add(dList[i].DiscoveryEvent.id, dList[i]);
        var eList = eraEvents == null ? _eraEvents : eraEvents;
        for (int i = 0; i < eList.Count; i++)
            _eraEventsPool.Add(eList[i].id, eList[i]);
    }

    public Discovery PopDiscovery(int zone)
    {
        var discovery = _discoveriesPool.Where(disc => disc.Value.IsAvailable(GameManager.Instance)).PickRandom().Value;
        _discoveriesPool.Remove(discovery.DiscoveryEvent.id);
        return discovery;
    }

    public EraEvent PopEraEvent()
    {
        var eraEvent = _eraEventsPool.Where(e => e.Value.IsEventAvailable()).PickRandom().Value;
        _eraEventsPool.Remove(eraEvent.id);
        return eraEvent;
    }

    public bool HasEventHappened(string eventId)
    {
        return _happenedEvents.ContainsKey(eventId);
    }

    public void AddEvent(GameEvent gameEvent)
    {
        _happenedEvents.Add(gameEvent.id, gameEvent);
    }
}
