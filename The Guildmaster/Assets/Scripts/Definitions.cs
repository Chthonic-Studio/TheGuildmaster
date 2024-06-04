using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Range
{
    public int minValue;
    public int maxValue;

    public int RandomValue()
    {
        return Random.Range(minValue, maxValue + 1);
    }
}

[System.Serializable]
public struct IntRange  // or ValueRange
{
    public int min; // Minimum value of the range
    public int max; // Maximum value of the range

    public IntRange(int min, int max)  // or ValueRange
    {
        this.min = min;
        this.max = max;
    }
}

[System.Serializable]
public class StatusProbability
{
    public StatusSO Status;
    [Range(0, 100)] public float Chance;
}

public class Definitions 
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}