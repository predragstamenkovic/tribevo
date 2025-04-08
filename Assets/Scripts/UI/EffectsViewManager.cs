using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectsViewManager : MonoBehaviour
{
    public EffectView tempView;
    public RectTransform content;

    private List<EffectView> effectViews;

    public void AddEffect(StatEffect effect, Sprite sprite)
    {
        float size = content.rect.height;
        var ev = Instantiate(tempView, content);
        var rect = (ev.transform as RectTransform);
        ev.SetEffect(this, effect, sprite);
        rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, size);
        rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, size);
        effectViews.Add(ev);
        UpdateViews();
        ev.gameObject.SetActive(true);
    }

    public void UpdateViews()
    {
        float size = content.rect.height;
        for (int i = 0; i < effectViews.Count; i++)
            effectViews[i].transform.localPosition = i * size * 1.15f * Vector3.right;
    }

    public void RemoveEffect(EffectView ev)
    {
        effectViews.Remove(ev);
        UpdateViews();
    }
}
