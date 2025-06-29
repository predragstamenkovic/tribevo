using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectsManager
{
    private List<StatEffect> _activeEventEffects;
    private List<StatEffect> _activeGenericEffects;

    public Action<StatEffect> OnEffectEnd;

    public void RegisterEventEffect(StatEffect effect)
    {
        _activeEventEffects.Add(effect);
    }

    public void RegisterGenericEffect(StatEffect effect)
    {
        _activeEventEffects.Add(effect);
    }

    public void UpdateEffects()
    {
        for (int i = 0; i < _activeEventEffects.Count; i++)
        {
            var effect = _activeEventEffects[i];
            effect.UpdateTimer();
            if (effect.IsEffectDone)
                OnEffectEnd.Invoke(effect);
        }

        for (int i = 0; i < _activeGenericEffects.Count; i++)
        {
            var effect = _activeGenericEffects[i];
            effect.UpdateTimer();
            if (effect.IsEffectDone)
                OnEffectEnd.Invoke(effect);
        }
    }
}
