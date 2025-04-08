using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StationView : MonoBehaviour
{
    public Image buildingImage;
    public Text nameText;
    public Text builtCountText;

    public void UpdateView(Station station, int built)
    {
        buildingImage.sprite = station.sprite;
        nameText.text = station.stationName + "(" + built + ")";
        builtCountText.text = built.ToString();
        if (built == 0)
            buildingImage.color = new Color(1.0f, 1.0f, 1.0f, 0.33f);
        else
            buildingImage.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
    }
}
