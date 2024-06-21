using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System;

public class VisitLandmark
{
    private characterTownAI characterTownAI;
    private CharacterProfile characterProfile;
    private GameObject landmark;

    public VisitLandmark(characterTownAI characterTownAI, CharacterProfile characterProfile)
    {
        this.characterTownAI = characterTownAI;
        this.characterProfile = characterProfile;

        VisitingLandmark();
    }

    public void VisitingLandmark()
    {
        // Get the AIDestinationSetter component of the character
        AIDestinationSetter destinationSetter = characterTownAI.GetComponent<AIDestinationSetter>();

        // Create a list of the landmarks
        List<string> landmarks = new List<string> { "MainPlaza", "Castle", "Docks", "Forest" };

        // Select a random landmark
        string randomLandmark = landmarks[UnityEngine.Random.Range(0, landmarks.Count)];

        // Find the landmark in the scene
        GameObject landmark = GameObject.Find(randomLandmark);

        if (landmark != null)
        {
            // Get the MoveTarget child object
            Transform moveTarget = characterTownAI.transform.Find("MoveTarget");

            if (moveTarget != null)
            {
                // Generate a random point around the landmark within a radius of 5 units
                Vector3 randomPoint = landmark.transform.position + UnityEngine.Random.insideUnitSphere * 5;

                // Check if the random point is inside a collider
                while (Physics2D.OverlapCircle(randomPoint, 0.5f))
                {
                    // If the point is inside a collider, generate a new point
                    randomPoint = landmark.transform.position + UnityEngine.Random.insideUnitSphere * 5;
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
            characterTownAI.StartCoroutine(characterTownAI.WaitAtDoor(30, 50));
        }
        else
        {
            Debug.LogError("Landmark " + randomLandmark + " not found");
        }
    }


}