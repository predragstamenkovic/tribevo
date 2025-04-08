using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationsViewManager : MonoBehaviour
{
    public RectTransform content;
    public StationView stationTemp;

    private Dictionary<string, StationView> stationViews;

    public void CheckStations()
    {
        var stations = GameManager.Instance.Tribe.AvailableStations;
        foreach (var station in stations)
        {
            var id = station.Value.id;
            if (!stationViews.ContainsKey(id))
            {
                var stationView = Instantiate(stationTemp, content);
                stationView.UpdateView(station.Value, 0);
                stationView.gameObject.SetActive(true);
                stationViews.Add(station.Key, stationView);
            }
            else
            {
                stationViews[id].UpdateView(station.Value, GameManager.Instance.Tribe.BuiltStations[id]);
            }
        }

        var removalList = new List<string>();
        foreach (var view in stationViews)
        {
            if (!stations.ContainsKey(view.Key))
                removalList.Add(view.Key);
        }

        for (int i = 0; i < removalList.Count; i++)
            stationViews.Remove(removalList[i]);
    }

    public void UpdateStation(string id)
    {
        stationViews[id].UpdateView(
            GameManager.Instance.Tribe.AvailableStations[id], 
            GameManager.Instance.Tribe.BuiltStations[id]);
    }
}
