using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System;

public class Sunbathing
{
    private characterTownAI characterTownAI;
    private CharacterProfile characterProfile;
    private GameObject sunbathingSpot;

    public Sunbathing(characterTownAI characterTownAI, CharacterProfile characterProfile)
    {
        this.characterTownAI = characterTownAI;
        this.characterProfile = characterProfile;

        GoSunbathing();
    }

    public void GoSunbathing()
    {
        // Get the AIDestinationSetter component of the character
        AIDestinationSetter destinationSetter = characterTownAI.GetComponent<AIDestinationSetter>();

        // Create a list of the landmarks of the festivals
        List<string> sunbathingSpots = new List<string> { "SunbathingSpot1", "SunbathingSpot2" };

        // Select a random landmark
        string randomSpot = sunbathingSpots[UnityEngine.Random.Range(0, sunbathingSpot.Count)];

        // Find the landmark in the scene
        GameObject sunbathingSpot = GameObject.Find(randomSpot);

        if (sunbathingSpot != null)
        {
            // Get the MoveTarget child object
            Transform moveTarget = characterTownAI.transform.Find("MoveTarget");

            if (moveTarget != null)
            {
                // Generate a random point around the landmark within a radius of 15 units
                Vector3 randomPoint = sunbathingSpot.transform.position + UnityEngine.Random.insideUnitSphere * 15;

                // Check if the random point is inside a collider
                while (Physics2D.OverlapCircle(randomPoint, 0.5f))
                {
                    // If the point is inside a collider, generate a new point
                    randomPoint = sunbathingSpot.transform.position + UnityEngine.Random.insideUnitSphere * 15;
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
            Debug.LogError("Sunbathing spot " + randomSpot + " not found");
        }
    }


}