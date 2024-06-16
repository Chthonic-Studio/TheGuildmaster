using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class NPCSpawner
{
    ///////// GENDER SELECTION /////////
    public static string GetNPCGender()
    {
        return CharacterGenderSelection.GetRandomGender();
    }

    /////// OCCUPATION SELECTION ///////

    public static NPCProfile.occupationType GetCharacterOccupation(NPCManager npcManager)
    {
        // Check if the total number of active NPCs has reached the limit.
        if (!npcManager.CanSpawnNPC())
        {
            throw new InvalidOperationException("Limit of active NPCs reached");
        }

        // Get the list of available occupations.
        List<NPCProfile.occupationType> availableOccupations = npcManager.GetAvailableOccupations();

        // If there are no available occupations, throw an exception.
        if (availableOccupations.Count == 0)
        {
            throw new InvalidOperationException("No occupation could be selected");
        }

        // Select a random occupation from the list of available occupations.
        NPCProfile.occupationType selectedOccupation = availableOccupations[UnityEngine.Random.Range(0, availableOccupations.Count)];

        return selectedOccupation;
    }

    /////// RACE SELECTION ////////

    public static RaceSO GetNPCRace(NPCManager npcManager)
    {
        float totalWeight = npcManager.allRaces.Sum(race => race.weight);
        float randomWeightPoint = UnityEngine.Random.Range(0, totalWeight);

        foreach (RaceSO race in npcManager.allRaces)
        {
            if (randomWeightPoint < race.weight)
                return race;
            randomWeightPoint -= race.weight;
        }

        throw new InvalidOperationException("No race could be selected");
    }

    /////// NAME SELECTION ///////

   public static (string, string) GetNPCName(string gender, RaceSO race)
   {
        string firstName = NameGenerator.GenerateFirstName(gender, race);
        string lastName = NameGenerator.GenerateLastName(race);        
       return (firstName, lastName);
    }

    //////// NPC SPAWNING /////////

    // Create a unique ID for the character
    private static int nextId = 0;

    public static void SpawnNPC(NPCManager npcManager, NamesDatabase namesDatabase)
    {
        // Get the NPC's gender, race, and name.
        string gender = GetNPCGender();
        RaceSO race = GetNPCRace(npcManager);
        (string firstName, string lastName) = GetNPCName(gender, race);

        // Get the NPC's occupation.
        NPCProfile.occupationType occupation = GetCharacterOccupation(npcManager);

        // Instantiate a new GameObject from the prefab.
        GameObject newNPC = GameObject.Instantiate(npcManager.npcPrefab);

        // Get the NPCProfile script from the new GameObject.
        NPCProfile npcProfile = newNPC.GetComponent<NPCProfile>();

        // Set the values in the NPCProfile script.
        npcProfile.npcGender = gender;
        npcProfile.npcRace = race;
        npcProfile.firstName = firstName;
        npcProfile.lastName = lastName;
        npcProfile.Occupation = occupation;
        npcProfile.age = UnityEngine.Random.Range(18, 30);
        npcProfile.Gold = UnityEngine.Random.Range(0, 1000);

        // Generate a unique alphanumeric ID for the NPC.
        npcProfile.npcID = $"{nextId++}-{Guid.NewGuid()}";

        // Initialize the NPCProfile.
        npcProfile.Initialize();

        // Register the NPC with the NPCManager.
        if (!npcManager.RegisterNPC(npcProfile))
        {
            // If the NPC could not be registered, destroy the GameObject and return.
            GameObject.Destroy(newNPC);
            return;
        }

        // You can add more code here to spawn the NPC based on the gender, race, name, and occupation.
    }



}
