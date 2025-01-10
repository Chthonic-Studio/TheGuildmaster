using System.Collections;
using UnityEngine;
using Pathfinding;

public class Spar
{
    private characterTownAI characterTownAI;
    private CharacterProfile characterProfile;
    private characterTownAI sparringPartner;
    private GameObject guildDoor;
    private GameObject backdoor;
    private GameObject arena;
    private float searchTimeout = 5f;
    private float conversationDuration = 2f;
    private float sparDurationMin = 10f;
    private float sparDurationMax = 30f;

    public Spar(characterTownAI characterTownAI, CharacterProfile characterProfile)
    {
        this.characterTownAI = characterTownAI;
        this.characterProfile = characterProfile;
        this.guildDoor = GuildManager.Instance.GuildDoor;
        this.backdoor = GuildManager.Instance.Backdoor;
        this.arena = GuildManager.Instance.Arena;

        StartSpar();
    }

    private void StartSpar()
    {
        characterTownAI.StartCoroutine(FindSparringPartner());
    }

    private IEnumerator FindSparringPartner()
    {
        float startTime = Time.time;
        while (Time.time - startTime < searchTimeout)
        {
            // Find an idle character to spar with
            characterTownAI[] allCharacters = GameObject.FindObjectsOfType<characterTownAI>();
            foreach (var character in allCharacters)
            {
                if (character != characterTownAI && character.IsIdle())
                {
                    sparringPartner = character;
                    break;
                }
            }

            if (sparringPartner != null)
            {
                yield return StartCoroutine(InitiateSparring());
                yield break;
            }

            yield return null;
        }

        Debug.Log("No idle character found for sparring.");
    }

    private IEnumerator InitiateSparring()
    {
        // Walk up to the sparring partner
        AIDestinationSetter destinationSetter = characterTownAI.GetComponent<AIDestinationSetter>();
        destinationSetter.target = sparringPartner.transform;

        // Wait until the character reaches the sparring partner
        yield return new WaitUntil(() => Vector3.Distance(characterTownAI.transform.position, sparringPartner.transform.position) < 1f);

        // Simulate conversation
        yield return new WaitForSeconds(conversationDuration);

        // Both characters move to the guild door
        destinationSetter.target = guildDoor.transform;
        AIDestinationSetter partnerDestinationSetter = sparringPartner.GetComponent<AIDestinationSetter>();
        partnerDestinationSetter.target = guildDoor.transform;

        // Wait until both characters reach the guild door
        yield return new WaitUntil(() => Vector3.Distance(characterTownAI.transform.position, guildDoor.transform.position) < 1f &&
                                         Vector3.Distance(sparringPartner.transform.position, guildDoor.transform.position) < 1f);

        // Simulate entering the guild
        characterTownAI.gameObject.SetActive(false);
        sparringPartner.gameObject.SetActive(false);
        yield return new WaitForSeconds(2f);

        // Reappear at the backdoor
        characterTownAI.transform.position = backdoor.transform.position;
        sparringPartner.transform.position = backdoor.transform.position;
        characterTownAI.gameObject.SetActive(true);
        sparringPartner.gameObject.SetActive(true);

        // Both characters move to the arena
        destinationSetter.target = arena.transform;
        partnerDestinationSetter.target = arena.transform;

        // Wait until both characters reach the arena
        yield return new WaitUntil(() => Vector3.Distance(characterTownAI.transform.position, arena.transform.position) < 1f &&
                                         Vector3.Distance(sparringPartner.transform.position, arena.transform.position) < 1f);

        // Start sparring
        yield return StartCoroutine(SparringSession());

        // Both characters move to the backdoor after sparring
        destinationSetter.target = backdoor.transform;
        partnerDestinationSetter.target = backdoor.transform;

        // Wait until both characters reach the backdoor
        yield return new WaitUntil(() => Vector3.Distance(characterTownAI.transform.position, backdoor.transform.position) < 1f &&
                                         Vector3.Distance(sparringPartner.transform.position, backdoor.transform.position) < 1f);

        // Simulate exiting the guild
        characterTownAI.gameObject.SetActive(false);
        sparringPartner.gameObject.SetActive(false);
        yield return new WaitForSeconds(2f);

        // Reappear at the guild door
        characterTownAI.transform.position = guildDoor.transform.position;
        sparringPartner.transform.position = guildDoor.transform.position;
        characterTownAI.gameObject.SetActive(true);
        sparringPartner.gameObject.SetActive(true);

        Debug.Log("Sparring session ended.");
        characterTownAI.isActive = false;
        sparringPartner.isActive = false;
    }

    private IEnumerator SparringSession()
    {
        float sparDuration = Random.Range(sparDurationMin, sparDurationMax);
        float endTime = Time.time + sparDuration;

        while (Time.time < endTime)
        {
            // Move characters close to each other to simulate fighting
            Vector3 randomOffset = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);
            characterTownAI.transform.position = arena.transform.position + randomOffset;
            sparringPartner.transform.position = arena.transform.position - randomOffset;

            // Simulate collision every 2 seconds
            yield return new WaitForSeconds(2f);
        }
    }
}