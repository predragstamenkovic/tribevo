using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatView : MonoBehaviour
{
    public Slider slider;
    public Text value;

    public void UpdateStat(float increase, float total, float max)
    {
        slider.maxValue = max;
        slider.value = total;
        value.text = increase.ToString("F1");
    }
}
