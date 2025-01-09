using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System;

public class Bathhouse
{
    private characterTownAI characterTownAI;
    private CharacterProfile characterProfile;
    private GameObject bathhouse;

    public Bathhouse(characterTownAI characterTownAI, CharacterProfile characterProfile)
    {
        this.characterTownAI = characterTownAI;
        this.characterProfile = characterProfile;

        GoToBathhouse();
    }

    public void GoToBathhouse()
    {
        // Get the AIDestinationSetter component of the character
        AIDestinationSetter destinationSetter = characterTownAI.GetComponent<AIDestinationSetter>();

        // Find the landmark in the scene
        GameObject bathhouse = GameObject.Find("Bathhouse");

        if (bathhouse != null)
        {
            // Set the target of the AIDestinationSetter to the MoveTarget
            destinationSetter.target = bathhouse.transform;

            // Start the coroutine to make the character wait at the landmark. Select a min and max waiting time
            characterTownAI.StartCoroutine(characterTownAI.WaitAtDoor(120, 180));
        }            
            
        else
        {
            Debug.LogError("Bathhouse not found for character " + characterTownAI.name);
        }

    }

}


