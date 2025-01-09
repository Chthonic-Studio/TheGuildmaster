using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System;

public class Whoring
{
    private characterTownAI characterTownAI;
    private CharacterProfile characterProfile;
    private GameObject whorehouse;

    public Whoring(characterTownAI characterTownAI, CharacterProfile characterProfile)
    {
        this.characterTownAI = characterTownAI;
        this.characterProfile = characterProfile;

        Whorehouse();
    }

    public void Whorehouse()
    {
        // Get the AIDestinationSetter component of the character
        AIDestinationSetter destinationSetter = characterTownAI.GetComponent<AIDestinationSetter>();

        // Find the landmark in the scene
        GameObject whorehouse = GameObject.Find("Whorehouse");

        if (whorehouse != null)
        {
            // Set the target of the AIDestinationSetter to the MoveTarget
            destinationSetter.target = whorehouse.transform;

            // Start the coroutine to make the character wait at the landmark. Select a min and max waiting time
            characterTownAI.StartCoroutine(characterTownAI.WaitAtDoor(120, 180));
        }            
            
        else
        {
            Debug.LogError("Whorehouse not found for character " + characterTownAI.name);
        }
    }
}


