using System.Collections;
using UnityEngine;
using Pathfinding;

public class StudyTactics
{
    private characterTownAI characterTownAI;
    private CharacterProfile characterProfile;
    private GameObject guildDoor;
    private float minStayDuration = 20f;
    private float maxStayDuration = 40f;

    public StudyTactics(characterTownAI characterTownAI, CharacterProfile characterProfile)
    {
        this.characterTownAI = characterTownAI;
        this.characterProfile = characterProfile;
        this.guildDoor = GuildManager.Instance.GuildDoor;

        StartStudyTactics();
    }

    private void StartStudyTactics()
    {
        characterTownAI.StartCoroutine(StudyTacticsRoutine());
    }

    private IEnumerator StudyTacticsRoutine()
    {
        // Move to the guild door
        AIDestinationSetter destinationSetter = characterTownAI.GetComponent<AIDestinationSetter>();
        destinationSetter.target = guildDoor.transform;

        // Wait until the character reaches the guild door
        yield return new WaitUntil(() => Vector3.Distance(characterTownAI.transform.position, guildDoor.transform.position) < 1f);

        // Simulate entering the guild
        characterTownAI.gameObject.SetActive(false);
        yield return new WaitForSeconds(2f);

        // Stay in the guild for a random duration
        float stayDuration = Random.Range(minStayDuration, maxStayDuration);
        yield return new WaitForSeconds(stayDuration);

        // Simulate leaving the guild
        characterTownAI.transform.position = guildDoor.transform.position;
        characterTownAI.gameObject.SetActive(true);

        Debug.Log("Tactics study completed.");
        characterTownAI.isActive = false;
    }
}