using System.Collections;
using UnityEngine;
using Pathfinding;

public class PracticeSkills
{
    private characterTownAI characterTownAI;
    private CharacterProfile characterProfile;
    private GameObject guildDoor;
    private float minStayDuration = 20f;
    private float maxStayDuration = 40f;

    public PracticeSkills(characterTownAI characterTownAI, CharacterProfile characterProfile)
    {
        this.characterTownAI = characterTownAI;
        this.characterProfile = characterProfile;
        this.guildDoor = GuildManager.Instance.GuildDoor;

        StartPracticeSkills();
    }

    private void StartPracticeSkills()
    {
        characterTownAI.StartCoroutine(PracticeSkillsRoutine());
    }

    private IEnumerator PracticeSkillsRoutine()
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

        Debug.Log("Skill practice completed.");
        characterTownAI.isActive = false;
    }
}