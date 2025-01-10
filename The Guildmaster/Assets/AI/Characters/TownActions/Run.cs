using System.Collections;
using UnityEngine;
using Pathfinding;

public class Run
{
    private characterTownAI characterTownAI;
    private CharacterProfile characterProfile;
    private float originalSpeed;
    private float runSpeedMultiplier = 1.5f;
    private Vector3 randomPoint;
    private Vector3 startPoint;

    public Run(characterTownAI characterTownAI, CharacterProfile characterProfile)
    {
        this.characterTownAI = characterTownAI;
        this.characterProfile = characterProfile;

        StartRun();
    }

    private void StartRun()
    {
        characterTownAI.StartCoroutine(RunRoutine());
    }

    private IEnumerator RunRoutine()
    {
        // Save the original speed and set the run speed
        originalSpeed = characterTownAI.GetComponent<AIPath>().maxSpeed;
        characterTownAI.GetComponent<AIPath>().maxSpeed *= runSpeedMultiplier;

        // Save the starting point
        startPoint = characterTownAI.transform.position;

        // Select a random point on the map
        randomPoint = GetRandomPointOnMap();

        // Move to the random point
        AIDestinationSetter destinationSetter = characterTownAI.GetComponent<AIDestinationSetter>();
        destinationSetter.target.position = randomPoint;

        // Wait until the character reaches the random point
        yield return new WaitUntil(() => Vector3.Distance(characterTownAI.transform.position, randomPoint) < 1f);

        // Move back to the starting point
        destinationSetter.target.position = startPoint;

        // Wait until the character reaches the starting point
        yield return new WaitUntil(() => Vector3.Distance(characterTownAI.transform.position, startPoint) < 1f);

        // Reset the character's speed to the original value
        characterTownAI.GetComponent<AIPath>().maxSpeed = originalSpeed;

        Debug.Log("Run action completed.");
        characterTownAI.isActive = false;
    }

    private Vector3 GetRandomPointOnMap()
    {
        // Assuming the map is a 2D plane with bounds from -50 to 50 on the x and z axes
        float x = Random.Range(-50f, 50f);
        float z = Random.Range(-50f, 50f);
        return new Vector3(x, 0, z);
    }
}