using System.Collections;
using System.Collections.Generic;
using System;

[AttributeUsage(AttributeTargets.Property)] // This attribute will be used on properties
public class StatAttribute : Attribute
{
    public string StatName { get; private set; } // This will hold the name of the stat

    public StatAttribute(string statName)
    {
        StatName = statName;
    }
}