using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class ArrayGenerator : MonoBehaviour
{
    public int count;
    public int baseNum;
    public bool shouldFloor;

    private void Start()
    {
        GenerateArray();
    }

    public void GenerateArray()
    {
        StringBuilder sb = new StringBuilder(baseNum.ToString());
        float x = baseNum;
        for (int i = 0; i < count; i++)
        {
            x = (x + 1) * 1.1f;
            sb.Append(", " + (shouldFloor ? Mathf.Floor(x) : x));
        }

        Debug.Log(sb);
    }
}
