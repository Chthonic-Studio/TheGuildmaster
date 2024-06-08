using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UtilityAI
{
    public float Desire { get; set; }
    public float Need { get; set; }
    public float UtilityValue { get; set; }

    public UtilityAI(float desire, float need)
    {
        Desire = desire;
        Need = need;
        CalculateUtilityValue();
    }

    public void CalculateUtilityValue()
    {
        // Implement your custom formula here
        // For example:
        UtilityValue = ((Desire + Need) * 0.7f) + UnityEngine.Random.Range(0f, 15f);
    }
}