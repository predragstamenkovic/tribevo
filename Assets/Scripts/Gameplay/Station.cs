using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Station")]
public class Station : ScriptableObject
{
    public bool isUnique;
    public string id;
    public string stationName;
    public int size;
    public Sprite sprite;
    public float buildTime;
    public List<StatEffect> effects;
}
