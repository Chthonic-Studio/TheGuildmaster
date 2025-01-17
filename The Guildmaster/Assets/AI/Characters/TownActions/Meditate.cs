using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System;

public class Meditate
{
    private characterTownAI characterTownAI;
    private CharacterProfile characterProfile;

    public Meditate(characterTownAI characterTownAI, CharacterProfile characterProfile)
    {
        this.characterTownAI = characterTownAI;
        this.characterProfile = characterProfile;

        StartMeditating();
    }

    public void StartMeditating()
    {
        // Get the AIDestinationSetter component of the character
        AIDestinationSetter destinationSetter = characterTownAI.GetComponent<AIDestinationSetter>();

        // Get the door of the character's house
        TownDoor door = characterTownAI.character.house.GetComponentInChildren<TownDoor>();

        if (door != null)
        {
            // Set the target of the AIDestinationSetter to the door
            destinationSetter.target = door.transform;

            // Start the coroutine to make the character wait at the door. Select a min and max waiting time
            characterTownAI.StartCoroutine(characterTownAI.WaitAtDoor(40, 60));
        }
        else
        {
            Debug.LogError("No TownDoor found in the character's house");
        }

    }


}