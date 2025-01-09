using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System;

public class Stargazing
{
    private characterTownAI characterTownAI;
    private CharacterProfile characterProfile;
    private GameObject stargazingSpot;

    public Stargazing(characterTownAI characterTownAI, CharacterProfile characterProfile)
    {
        this.characterTownAI = characterTownAI;
        this.characterProfile = characterProfile;

        GoStargazing();
    }

    public void GoStargazing()
    {
        // Get the AIDestinationSetter component of the character
        AIDestinationSetter destinationSetter = characterTownAI.GetComponent<AIDestinationSetter>();

        // Create a list of the landmarks of the festivals
        List<string> stargazingSpots = new List<string> { "stargazingSpot1", "stargazingSpot2" };

        // Select a random landmark
        string randomSpot = stargazingSpots[UnityEngine.Random.Range(0, stargazingSpot.Count)];

        // Find the landmark in the scene
        GameObject stargazingSpot = GameObject.Find(randomSpot);

        if (stargazingSpot != null)
        {
            // Get the MoveTarget child object
            Transform moveTarget = characterTownAI.transform.Find("MoveTarget");

            if (moveTarget != null)
            {
                // Generate a random point around the landmark within a radius of 15 units
                Vector3 randomPoint = stargazingSpot.transform.position + UnityEngine.Random.insideUnitSphere * 15;

                // Check if the random point is inside a collider
                while (Physics2D.OverlapCircle(randomPoint, 0.5f))
                {
                    // If the point is inside a collider, generate a new point
                    randomPoint = stargazingSpot.transform.position + UnityEngine.Random.insideUnitSphere * 15;
                }

                // Set the position of the MoveTarget to the random point
                moveTarget.position = randomPoint;

                // Set the target of the AIDestinationSetter to the MoveTarget
                destinationSetter.target = moveTarget;
            }            
            
            else
            {
                Debug.LogError("MoveTarget not found for character " + characterTownAI.name);
            }

            // Start the coroutine to make the character wait at the landmark. Select a min and max waiting time
            characterTownAI.StartCoroutine(characterTownAI.WaitAtDoor(90, 120));
        }
        else
        {
            Debug.LogError("Stargazing spot " + randomSpot + " not found");
        }
    }


}