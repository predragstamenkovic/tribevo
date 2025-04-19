using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Event/EraEvent")]
public class EraEvent : GameEvent
{
    public int era;
    public List<EraEvent> leadingEvents;
}
