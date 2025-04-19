using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Event/GameEvent")]
public class GameEvent : ScriptableObject
{
    public string id;
    public string eventName;
    public Sprite sprite;
    public List<StatEffect> effects;
    public List<Evolution> requiredEvolutions;

    public bool IsEventAvailable()
    {
        return true;
    }

    public virtual void EventAction()
    {

    }
}
