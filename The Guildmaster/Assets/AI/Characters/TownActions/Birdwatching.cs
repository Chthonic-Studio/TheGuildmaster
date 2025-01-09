using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System;

public class Birdwatching
{
    private characterTownAI characterTownAI;
    private CharacterProfile characterProfile;
    private GameObject birdwatchingSpot;

    public Birdwatching(characterTownAI characterTownAI, CharacterProfile characterProfile)
    {
        this.characterTownAI = characterTownAI;
        this.characterProfile = characterProfile;

        GoBirdwatching();
    }

    public void GoBirdwatching()
    {
        // Get the AIDestinationSetter component of the character
        AIDestinationSetter destinationSetter = characterTownAI.GetComponent<AIDestinationSetter>();

        // Create a list of birdwatching spots
        List<string> birdwatchingSpots = new List<string> { "Birdwatching1", "Birdwatching2" };

        // Select a random landmark
        string selectedSpot = birdwatchingSpots[UnityEngine.Random.Range(0, birdwatchingSpots.Count)];

        // Find the landmark in the scene
        GameObject birdwatchingSpotObject = GameObject.Find(selectedSpot);

        if (birdwatchingSpotObject != null)
        {
            // Get the MoveTarget child object
            Transform moveTarget = characterTownAI.transform.Find("MoveTarget");

            if (moveTarget != null)
            {
                // Generate a random point around the landmark within a radius of 15 units
                Vector3 randomPoint = birdwatchingSpotObject.transform.position + UnityEngine.Random.insideUnitSphere * 15;

                // Check if the random point is inside a collider
                while (Physics2D.OverlapCircle(randomPoint, 0.5f))
                {
                    // If the point is inside a collider, generate a new point
                    randomPoint = birdwatchingSpotObject.transform.position + UnityEngine.Random.insideUnitSphere * 15;
                }

                // Set the position of the MoveTarget to the random point
                moveTarget.position = randomPoint;

                // Set the target of the AIDestinationSetter to the MoveTarget
                destinationSetter.target = moveTarget;

                // Start the coroutine to make the character wait at the landmark. Select a min and max waiting time
                characterTownAI.StartCoroutine(characterTownAI.WaitAtDoor(90, 150));
            }
        }            
            else
            {
                Debug.LogError("MoveTarget not found for character " + characterTownAI.name);
            }
        }

}


