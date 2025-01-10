using System.Collections;
using UnityEngine;
using Pathfinding;

public class ComissionItem
{
    private characterTownAI characterTownAI;
    private CharacterProfile characterProfile;
    private Store store;

    public ComissionItem(characterTownAI characterTownAI, CharacterProfile characterProfile)
    {
        this.characterTownAI = characterTownAI;
        this.characterProfile = characterProfile;

        StartComissionItem();
    }

    public void StartComissionItem()
    {
        // Get the AIDestinationSetter component of the character
        AIDestinationSetter destinationSetter = characterTownAI.GetComponent<AIDestinationSetter>();

        // Placeholder logic to select a random store of any type
        store = StoreManager.Instance.GetRandomStore(Store.StoreType.General);
        if (store == null)
        {
            Debug.LogWarning("No available store found.");
            return;
        }

        // Set the destination to the store
        destinationSetter.target = store.transform;

        // Start coroutine to handle the commissioning process
        characterTownAI.StartCoroutine(ComissioningProcess());
    }

    private IEnumerator ComissioningProcess()
    {
        // Wait until the character reaches the store
        yield return new WaitUntil(() => Vector3.Distance(characterTownAI.transform.position, store.transform.position) < 1f);

        // Simulate entering the store
        characterTownAI.gameObject.SetActive(false);

        // Simulate time spent commissioning an item
        yield return new WaitForSeconds(Random.Range(30f, 60f));

        // Simulate exiting the store
        characterTownAI.gameObject.SetActive(true);
        characterTownAI.isActive = false;

        Debug.Log("ComissionItem completed.");
    }
}