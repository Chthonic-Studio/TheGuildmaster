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


public static class ColliderExtensions
{
    public static Vector2 GetRandomPointInCollider(Collider2D collider)
    {
        Bounds bounds = collider.bounds;

        // Generate random points within the bounds of the collider
        float randomX = Random.Range(bounds.min.x, bounds.max.x);
        float randomY = Random.Range(bounds.min.y, bounds.max.y);

        Vector2 randomPoint = new Vector2(randomX, randomY);

        // Ensure the random point is inside the collider
        while (!collider.OverlapPoint(randomPoint))
        {
            randomX = Random.Range(bounds.min.x, bounds.max.x);
            randomY = Random.Range(bounds.min.y, bounds.max.y);
            randomPoint = new Vector2(randomX, randomY);
        }

        return randomPoint;
    }
}