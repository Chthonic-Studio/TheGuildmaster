using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System;

public class Sleep
{
    private characterTownAI characterTownAI;
    private CharacterProfile characterProfile;
    private characterTownAI fatigue;

    public Sleep(characterTownAI characterTownAI, CharacterProfile characterProfile, characterTownAI fatigue)
    {
        this.characterTownAI = characterTownAI;
        this.characterProfile = characterProfile;
        this.characterTownAI = fatigue;

        StartSleeping();
    }

    public void StartSleeping()
    {
        // Get the AIDestinationSetter component of the character
        AIDestinationSetter destinationSetter = characterTownAI.GetComponent<AIDestinationSetter>();

        // Get the door of the character's house
        TownDoor door = characterTownAI.character.house.GetComponentInChildren<TownDoor>();

        if (door != null)
        {
            // Set the target of the AIDestinationSetter to the door
            destinationSetter.target = door.transform;

            // Start the coroutine to sleep
            characterTownAI.StartCoroutine(characterTownAI.SleepCoroutine(fatigue));
        }
        else
        {
            Debug.LogError("No TownDoor found in the character's house");
        }
    }
}