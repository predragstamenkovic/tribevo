using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EraEvent : GameEvent
{
    public int era;
    public List<EraEvent> leadingEvents;
}
