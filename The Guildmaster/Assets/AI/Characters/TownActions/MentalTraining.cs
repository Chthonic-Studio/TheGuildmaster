using System.Collections;
using UnityEngine;
using Pathfinding;

public class MentalTraining
{
    private characterTownAI characterTownAI;
    private CharacterProfile characterProfile;
    private TownDoor homeDoor;
    private float minStayDuration = 20f;
    private float maxStayDuration = 40f;

    public MentalTraining(characterTownAI characterTownAI, CharacterProfile characterProfile)
    {
        this.characterTownAI = characterTownAI;
        this.characterProfile = characterProfile;
        this.homeDoor = characterTownAI.door;

        StartMentalTraining();
    }

    private void StartMentalTraining()
    {
        characterTownAI.StartCoroutine(MentalTrainingRoutine());
    }

    private IEnumerator MentalTrainingRoutine()
    {
        // Move to the character's home door
        AIDestinationSetter destinationSetter = characterTownAI.GetComponent<AIDestinationSetter>();
        destinationSetter.target = homeDoor.transform;

        // Wait until the character reaches their home door
        yield return new WaitUntil(() => Vector3.Distance(characterTownAI.transform.position, homeDoor.transform.position) < 1f);

        // Simulate entering the home
        characterTownAI.gameObject.SetActive(false);
        yield return new WaitForSeconds(2f);

        // Stay at home for a random duration
        float stayDuration = Random.Range(minStayDuration, maxStayDuration);
        yield return new WaitForSeconds(stayDuration);

        // Simulate leaving the home
        characterTownAI.gameObject.SetActive(true);

        Debug.Log("Mental training completed.");
        characterTownAI.isActive = false;
    }
}