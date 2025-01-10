using System.Collections;
using UnityEngine;
using Pathfinding;

public class LiftWeights
{
    private characterTownAI characterTownAI;
    private CharacterProfile characterProfile;
    private GameObject guildDoor;
    private GameObject backdoor;
    private GameObject weightsArea;
    private float minStayDuration = 20f;
    private float maxStayDuration = 40f;

    public LiftWeights(characterTownAI characterTownAI, CharacterProfile characterProfile)
    {
        this.characterTownAI = characterTownAI;
        this.characterProfile = characterProfile;
        this.guildDoor = GuildManager.Instance.GuildDoor;
        this.backdoor = GuildManager.Instance.Backdoor;
        this.weightsArea = GuildManager.Instance.Weights;

        StartLiftWeights();
    }

    private void StartLiftWeights()
    {
        characterTownAI.StartCoroutine(LiftWeightsRoutine());
    }

    private IEnumerator LiftWeightsRoutine()
    {
        // Move to the guild door
        AIDestinationSetter destinationSetter = characterTownAI.GetComponent<AIDestinationSetter>();
        destinationSetter.target = guildDoor.transform;

        // Wait until the character reaches the guild door
        yield return new WaitUntil(() => Vector3.Distance(characterTownAI.transform.position, guildDoor.transform.position) < 1f);

        // Simulate entering the guild
        characterTownAI.gameObject.SetActive(false);
        yield return new WaitForSeconds(2f);

        // Reappear at the backdoor
        characterTownAI.transform.position = backdoor.transform.position;
        characterTownAI.gameObject.SetActive(true);

        // Move to the weights area
        destinationSetter.target = weightsArea.transform;

        // Wait until the character reaches the weights area
        yield return new WaitUntil(() => Vector3.Distance(characterTownAI.transform.position, weightsArea.transform.position) < 1f);

        // Stay in the weights area for a random duration
        float stayDuration = Random.Range(minStayDuration, maxStayDuration);
        yield return new WaitForSeconds(stayDuration);

        // Move back to the backdoor
        destinationSetter.target = backdoor.transform;

        // Wait until the character reaches the backdoor
        yield return new WaitUntil(() => Vector3.Distance(characterTownAI.transform.position, backdoor.transform.position) < 1f);

        // Simulate entering the guild
        characterTownAI.gameObject.SetActive(false);
        yield return new WaitForSeconds(2f);

        // Reappear at the guild door
        characterTownAI.transform.position = guildDoor.transform.position;
        characterTownAI.gameObject.SetActive(true);

        Debug.Log("Weight lifting session completed.");
        characterTownAI.isActive = false;
    }
}