using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System;

public class TombRaiding
{
    private characterTownAI characterTownAI;
    private CharacterProfile characterProfile;
    public GameObject WestExit;
    public GameObject EastExit;
    public GameObject SouthExit;

    public TombRaiding(characterTownAI characterTownAI, CharacterProfile characterProfile)
    {
        this.characterTownAI = characterTownAI;
        this.characterProfile = characterProfile;

        TreasureHunting();
    }

    public void TreasureHunting()
    {
        // Get the AIDestinationSetter component of the character
        AIDestinationSetter destinationSetter = characterTownAI.GetComponent<AIDestinationSetter>();

        // Find the tavern doors
        GameObject WestExit = GameObject.Find("WestExit");
        GameObject EastExit = GameObject.Find("EastExit");
        GameObject SouthExit = GameObject.Find("SouthExit");

        // Create an array of the tavern doors
        GameObject[] TownExits = new GameObject[] { WestExit, EastExit, SouthExit };

        // Select a random tavern door
        GameObject randomTownExit = TownExits[UnityEngine.Random.Range(0, TownExits.Length)];

        if (randomTownExit != null)
        {
            // Set the target of the AIDestinationSetter to the door
            destinationSetter.target = randomTownExit.transform;

            // Start the coroutine to make the character wait at the door. Select a min and max waiting time
            characterTownAI.StartCoroutine(characterTownAI.WaitAtDoor(300, 500));
        }
        else
        {
            Debug.LogError("No town exit found. Unable to leave the town.");
        }
    }

}