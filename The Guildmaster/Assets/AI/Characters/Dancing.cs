using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System;

public class Dancing
{
    private characterTownAI characterTownAI;
    private CharacterProfile characterProfile;
    public GameObject Tavern1Door;
    public GameObject Tavern2Door;
    public GameObject Tavern3Door;

    public Dancing(characterTownAI characterTownAI, CharacterProfile characterProfile)
    {
        this.characterTownAI = characterTownAI;
        this.characterProfile = characterProfile;

        StartDancing();
    }

    public void StartDancing()
    {
        // Get the AIDestinationSetter component of the character
        AIDestinationSetter destinationSetter = characterTownAI.GetComponent<AIDestinationSetter>();

        // Find the tavern doors
        GameObject Tavern1Door = GameObject.Find("Tavern1Door");
        GameObject Tavern2Door = GameObject.Find("Tavern2Door");
        GameObject Tavern3Door = GameObject.Find("Tavern3Door");

        // Create an array of the tavern doors
        GameObject[] tavernDoors = new GameObject[] { Tavern1Door, Tavern2Door, Tavern3Door };

        // Select a random tavern door
        GameObject randomTavernDoor = tavernDoors[UnityEngine.Random.Range(0, tavernDoors.Length)];

        // Get the TownDoor component of the random tavern door
        TownDoor door = randomTavernDoor.GetComponent<TownDoor>();

        if (door != null)
        {
            // Set the target of the AIDestinationSetter to the door
            destinationSetter.target = door.transform;

            // Start the coroutine to make the character wait at the door. Select a min and max waiting time
            characterTownAI.StartCoroutine(characterTownAI.WaitAtDoor(30, 50));
        }
        else
        {
            Debug.LogError("No TownDoor found in the selected tavern door");
        }
    }

}