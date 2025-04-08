using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EffectView : MonoBehaviour
{
    public Image background;
    public Image effentImage;
    public Image timerImage;
    public Text effectText;

    private const float updatePeriod = 0.5f;
    private float phase = 0.0f;
    private StatEffect statEffect;
    private EffectsViewManager manager;

    public void SetEffect(EffectsViewManager manager, StatEffect statEffect, Sprite sprite)
    {
        this.manager = manager;
        this.statEffect = statEffect;
        effentImage.sprite = sprite;
        timerImage.fillAmount = 1;
    }

    public void UpdateEffect()
    {
        timerImage.fillAmount = statEffect.TimePercent;
        effectText.text = statEffect.TimeLeft.ToString("F0");
    }

    private void Update()
    {
        if (phase > updatePeriod)
        {
            phase = 0.0f;
            UpdateEffect();
            if (statEffect.IsTimerDone)
                manager.RemoveEffect(this);
        }
        phase += Time.deltaTime;
    }
}
