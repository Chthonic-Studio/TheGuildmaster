using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System;

public class GoExploring
{
    private characterTownAI characterTownAI;
    private CharacterProfile characterProfile;

    public GoExploring(characterTownAI characterTownAI, CharacterProfile characterProfile)
    {
        this.characterTownAI = characterTownAI;
        this.characterProfile = characterProfile;

        GoForExploration();
    }

    public void GoForExploration()
    {
        // Get the AIDestinationSetter component of the character
        AIDestinationSetter destinationSetter = characterTownAI.GetComponent<AIDestinationSetter>();

        // Define the bounds of the town
        Vector3 townMinBounds = new Vector3(-116, -60, 0);
        Vector3 townMaxBounds = new Vector3(122, 118, 0);

        // Generate a random point within the bounds of the town
        Vector3 randomPoint = new Vector3(
            UnityEngine.Random.Range(townMinBounds.x, townMaxBounds.x),
            0,
            UnityEngine.Random.Range(townMinBounds.y, townMaxBounds.y)
        );

        // Check if the random point is inside a collider
        while (Physics2D.OverlapCircle(randomPoint, 0.5f))
        {
            // If the point is inside a collider, generate a new point
            randomPoint = new Vector3(
                UnityEngine.Random.Range(townMinBounds.x, townMaxBounds.x),
                0,
                UnityEngine.Random.Range(townMinBounds.y, townMaxBounds.y)
            );
        }

        // Get the MoveTarget child object
        Transform moveTarget = characterTownAI.transform.Find("MoveTarget");

        if (moveTarget != null)
        {
            // Set the position of the MoveTarget to the random point
            moveTarget.position = randomPoint;

            // Set the target of the AIDestinationSetter to the MoveTarget
            destinationSetter.target = moveTarget;
        }
        else
        {
            Debug.LogError("MoveTarget not found for character " + characterTownAI.name);
        }
    }

}