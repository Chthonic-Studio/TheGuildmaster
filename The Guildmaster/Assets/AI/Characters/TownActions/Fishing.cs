using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System;

public class Fishing
{
    private characterTownAI characterTownAI;
    private CharacterProfile characterProfile;
    public GameObject FishingSpot1;
    public GameObject FishingSpot2;
    public GameObject FishingSpot3;
    public GameObject FishingSpot4;
    public GameObject FishingSpot5;

    public Fishing(characterTownAI characterTownAI, CharacterProfile characterProfile)
    {
        this.characterTownAI = characterTownAI;
        this.characterProfile = characterProfile;

        GoFishing();
    }

    public void GoFishing()
    {
        // Get the AIDestinationSetter component of the character
        AIDestinationSetter destinationSetter = characterTownAI.GetComponent<AIDestinationSetter>();

        // Find the fishing spots
        GameObject FishingSpot1 = GameObject.Find("FishingSpot1");
        GameObject FishingSpot2 = GameObject.Find("FishingSpot2");
        GameObject FishingSpot3 = GameObject.Find("FishingSpot3");
        GameObject FishingSpot4 = GameObject.Find("FishingSpot4");
        GameObject FishingSpot5 = GameObject.Find("FishingSpot5");

        // Create an array of the fishing spots
        GameObject[] fishingSpots = new GameObject[] { FishingSpot1, FishingSpot2, FishingSpot3, FishingSpot4, FishingSpot5 };

        // Select a random spot
        GameObject randomFishingSpot = fishingSpots[UnityEngine.Random.Range(0, fishingSpots.Length)];

        if (randomFishingSpot != null)
        {
            // Set the target of the AIDestinationSetter to the door
            destinationSetter.target = randomFishingSpot.transform;

            // Start the coroutine to make the character wait at the door. Select a min and max waiting time
            characterTownAI.StartCoroutine(characterTownAI.WaitAtDoor(90, 120));
        }
        else
        {
            Debug.LogError("No Fishing Spot found");
        }
    }



}