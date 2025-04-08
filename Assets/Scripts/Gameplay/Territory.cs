using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Territory
{
    private int size;
    private Discovery discovery;

    public Territory(int size, Discovery discovery = null)
    {
        this.size = size;
        this.discovery = discovery;
    }

    public GameEvent DisoveryEvent => discovery.discoveryEvent;
    public int Size => size;
    public IReadOnlyList<StatEffect> Effects => discovery != null ? discovery.territoryEffects : new List<StatEffect>();
}
