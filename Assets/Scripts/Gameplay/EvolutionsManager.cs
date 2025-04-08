using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EvolutionsManager : MonoBehaviour
{
    [SerializeField]
    private List<Evolution> evolutions;
    private Dictionary<string, Evolution> availableEvolutions;

    private List<int> EVOLUTION_CAPS = new List<int> { 20, 30, 40, 55, 75, 95, 120, 150, 185, 230, 280, 340, 415, 505, 610, 740, 895, 1080, 1300, 1570, 
        1890, 2270, 2725, 3270, 3920, 4700, 5640, 6765, 8110, 9725, 11680, 14020, 16825, 20195, 24235, 29080, 34895, 41875, 50250, 60300, 72360 };

    public IReadOnlyList<Evolution> GetEvolutionPicks(int count)
    {
        var picks = new List<Evolution>();
        var evoList = availableEvolutions.Select(evo => evo.Value).ToList();
        if (evoList.Count < count)
            return evoList;

        evoList.Shuffle();
        for (int i = 0; i < count; i++)
            picks.Add(evoList[i]);

        return picks;
    }

    public void EvolutionPicked(Evolution pickedEvo)
    {
        if (availableEvolutions.ContainsKey(pickedEvo.id))
            availableEvolutions.Remove(pickedEvo.id);
        for (int i = 0; i < pickedEvo.leadingEvolutions.Count; i++)
        {
            var evo = pickedEvo.leadingEvolutions[i];
            if (evo.IsEvolutionAvailable())
                availableEvolutions.Add(evo.id, evo);
        }
    }

    public int GetEvolutionCap(int evolutions)
    {
        return EVOLUTION_CAPS[evolutions];
    }
}
