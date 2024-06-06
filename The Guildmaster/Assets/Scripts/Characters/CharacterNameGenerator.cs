using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class NameGenerator 
{
    private static NamesDatabase namesDatabase;

    public static void Initialize(NamesDatabase db)
    {
        namesDatabase = db;
    }

    public static string GenerateFirstName(string gender, RaceSO race)
    {
        List<string> firstNames = GetFirstNamesForGenderAndRace(gender, race);
        int randomIndex = UnityEngine.Random.Range(0, firstNames.Count);
        return firstNames[randomIndex];
    }

    public static string GenerateLastName(RaceSO race)
    {
        List<string> lastNames = GetLastNamesForRace(race);
        int randomIndex = UnityEngine.Random.Range(0, lastNames.Count);
        return lastNames[randomIndex];
    }

    private static List<string> GetFirstNamesForGenderAndRace(string gender, RaceSO race)
    {
        // Remove spaces from the gender string before parsing
        string sanitizedGender = gender.Replace(" ", "");
        
        if (!Enum.TryParse(sanitizedGender, out NamesDatabase.Gender genderEnum))
        {
            throw new ArgumentException($"Invalid gender value: {gender}");
        }

        return namesDatabase.firstNames
            .Where(entry => entry.gender == genderEnum && entry.races.Contains(race))
            .Select(entry => entry.name)
            .ToList();
    }
    
    private static List<string> GetLastNamesForRace(RaceSO race)
    {
        return namesDatabase.lastNames
            .Where(entry => entry.races.Contains(race))
            .Select(entry => entry.name)
            .ToList();
    }
}

