using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EraManager : MonoBehaviour
{
    public List<EraEvent> eraEvents;

    public const int ERA_COUNT = 7;
    private List<int> eraChangeTimes = new List<int>() { 120, 300, 780, 600, 900, 1080, 1200 };
    private List<List<EraEvent>> eventsPerEra;
    private List<EraEvent> preparedEvents;
    private int currentEra = 0;

    public int CurrentEra => currentEra;

    public void Setup()
    {
        eventsPerEra = new List<List<EraEvent>>();
        for (int i = 0; i < ERA_COUNT; i++)
            eventsPerEra.Add(new List<EraEvent>());

        for (int i = 0; i < eraEvents.Count; i++)
            eventsPerEra[eraEvents[i].era].Add(eraEvents[i]);
    }

    public void PrepareEvents()
    {
        currentEra = 0;
        preparedEvents = new List<EraEvent>();
        List<EraEvent> leadingEvents = null;
        for (int i = 0; i < ERA_COUNT; i++)
        {
            if (leadingEvents != null)
                preparedEvents.Add(leadingEvents[Random.Range(0, leadingEvents.Count)]);
            else
                preparedEvents.Add(eventsPerEra[i][Random.Range(0, eventsPerEra[i].Count)]);
            leadingEvents = preparedEvents[i].leadingEvents;
        }
    }

    public void UpdateEra()
    {
        if (GameManager.Instance.GetSessionSeconds > eraChangeTimes[currentEra])
        {
            preparedEvents[currentEra].EventAction();
            currentEra++;
        }
    }
}
