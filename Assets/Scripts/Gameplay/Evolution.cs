using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Evolution")]
public class Evolution : ScriptableObject
{
    public string id;
    public List<StatEffect> effects;
    public List<Station> stations;
    public List<Evolution> requiredEvolutionsAnd;
    public List<Evolution> requiredEvolutionsOr;
    public List<GameEvent> requiredEventAnd;
    public List<GameEvent> requiredEventOr;
    public List<Evolution> leadingEvolutions;

    public bool IsEvolutionAvailable()
    {
        var tribe = GameManager.Instance.Tribe;
        for (int i = 0; i < requiredEvolutionsAnd.Count; i++)
            if (!tribe.HasEvolution(requiredEvolutionsAnd[i].id))
                return false;

        bool hasOr = false;
        for (int i = 0; i < requiredEvolutionsOr.Count; i++)
        {
            if (tribe.HasEvolution(requiredEvolutionsOr[i].id))
            {
                hasOr = true;
                break;
            }
        }
        if (!hasOr)
            return false;

        for (int i = 0; i < requiredEventAnd.Count; i++)
            if (!GameManager.Instance.HasEventHappened(requiredEventAnd[i].id))
                return false;

        hasOr = false;
        for (int i = 0; i < requiredEventOr.Count; i++)
        {
            if (GameManager.Instance.HasEventHappened(requiredEventOr[i].id))
            {
                hasOr = true;
                break;
            }
        }

        if (!hasOr)
            return false;


        return true;
    }
}
