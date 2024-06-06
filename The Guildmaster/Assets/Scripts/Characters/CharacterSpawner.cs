using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class CharacterSpawner 
{
    ///////// GENDER SELECTION /////////
    public static string GetCharacterGender()
    {
        return CharacterGenderSelection.GetRandomGender();
    }

    /////// RACE SELECTION ////////

    public static RaceSO GetCharacterRace(CharacterManager characterManager)
    {
        float totalWeight = characterManager.allRaces.Sum(race => race.weight);
        float randomWeightPoint = UnityEngine.Random.Range(0, totalWeight);

        foreach (RaceSO race in characterManager.allRaces)
        {
            if (randomWeightPoint < race.weight)
                return race;
            randomWeightPoint -= race.weight;
        }

        throw new InvalidOperationException("No race could be selected");
    }

    /////// CLASS SELECTION ///////

    public static ClassSO GetCharacterClass(CharacterManager characterManager)
    {
        float totalWeight = characterManager.allClasses.Sum(classSO => classSO.weight);
        float randomWeightPoint = UnityEngine.Random.Range(0, totalWeight);

        foreach (ClassSO classSO in characterManager.allClasses)
        {
            if (randomWeightPoint < classSO.weight)
                return classSO;
            randomWeightPoint -= classSO.weight;
        }

        throw new InvalidOperationException("No class could be selected");
    }

    /////// TRAIT SELECTION ///////

    public static List<TraitSO> GetCharacterTraits(CharacterManager characterManager, int numTraits)
    {
        List<TraitSO> selectedTraits = new List<TraitSO>();
        List<TraitSO> possibleTraits = new List<TraitSO>(characterManager.allTraits);

        for (int i = 0; i < numTraits; i++)
        {
            float totalWeight = 0;
            foreach (TraitSO trait in possibleTraits)
            {
                totalWeight += trait.weight;
            }

            float randomValue = UnityEngine.Random.Range(0, totalWeight);

            float cumulativeWeight = 0;
            TraitSO selectedTrait = null;
            foreach (TraitSO trait in possibleTraits)
            {
                cumulativeWeight += trait.weight;
                if (randomValue < cumulativeWeight)
                {
                    selectedTrait = trait;
                    break;
                }
            }

            if (selectedTrait!= null)
            {
                selectedTraits.Add(selectedTrait);
                possibleTraits.Remove(selectedTrait); // Remove the selected trait from the possible traits
            }
        }

        return selectedTraits;
    }
    /////// PERSONALITY SELECTION ///////

    public static PersonalitySO GetCharacterPersonality(CharacterManager characterManager)
    {
        var randomIndex = UnityEngine.Random.Range(0, characterManager.allPersonalities.Count);
        return characterManager.allPersonalities[randomIndex];
    }

    /////// BACKSTORY SELECTION ///////

    public static BackstorySO GetCharacterBackstory(CharacterManager characterManager)
    {
        var randomIndex = UnityEngine.Random.Range(0, characterManager.allBackstories.Count);
        return characterManager.allBackstories[randomIndex];
    }

    /////// NAME SELECTION ///////

   public static (string, string) GetCharacterName(string gender, RaceSO race)
   {
        string firstName = NameGenerator.GenerateFirstName(gender, race);
        string lastName = NameGenerator.GenerateLastName(race);        
       return (firstName, lastName);
    }

    //////// CHARACTER SPAWNING /////////

    // Create a unique ID for the character
    private static int nextId = 0;

    public static void SpawnCharacter(CharacterManager characterManager, NamesDatabase namesDatabase)
    {
        
        string gender = GetCharacterGender();
        RaceSO race = GetCharacterRace(characterManager);
        (string firstName, string lastName) = GetCharacterName(gender, race);
        ClassSO selectedClass = GetCharacterClass(characterManager);
        List<TraitSO> selectedTraits = GetCharacterTraits(characterManager, 3);
        PersonalitySO selectedPersonality = GetCharacterPersonality(characterManager);
        BackstorySO selectedBackstory = GetCharacterBackstory(characterManager);

        // Instantiate a new GameObject from the prefab
        GameObject newCharacter = GameObject.Instantiate(characterManager.characterPrefab);

        // Get the CharacterProfile script from the new GameObject
        CharacterProfile characterProfile = newCharacter.GetComponent<CharacterProfile>();

        // Set the values in the CharacterProfile script
        characterProfile.characterGender = gender;
        characterProfile.selectedRace = race;
        characterProfile.characterFirstName = firstName;
        characterProfile.characterLastName = lastName;
        characterProfile.selectedClass = selectedClass;
        characterProfile.selectedTraits = selectedTraits;
        characterProfile.selectedPersonality = selectedPersonality;
        characterProfile.selectedBackstory = selectedBackstory;

        // Generate a unique alphanumeric ID for the character
        characterProfile.characterID = $"{nextId++}-{Guid.NewGuid()}";

        // You can add more code here to spawn the character based on the gender, race, and name
    }

}