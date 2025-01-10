using System.Collections;
using UnityEngine;
using Pathfinding;

public class WindowShopping
{
    private characterTownAI characterTownAI;
    private CharacterProfile characterProfile;
    private Store store;

    public WindowShopping(characterTownAI characterTownAI, CharacterProfile characterProfile)
    {
        this.characterTownAI = characterTownAI;
        this.characterProfile = characterProfile;

        Execute();
    }

    private void Execute()
    {
        // Placeholder logic to select a random store of any type
        store = StoreManager.Instance.GetRandomStore(Store.StoreType.General);
        if (store == null)
        {
            Debug.LogWarning("No available store found.");
            return;
        }

        // Set the destination to the store
        AIDestinationSetter destinationSetter = characterTownAI.GetComponent<AIDestinationSetter>();
        destinationSetter.target = store.transform;

        // Start coroutine to handle the window shopping process
        characterTownAI.StartCoroutine(WindowShoppingProcess());
    }

    private IEnumerator WindowShoppingProcess()
    {
        // Wait until the character reaches the store
        yield return new WaitUntil(() => Vector3.Distance(characterTownAI.transform.position, store.transform.position) < 1f);

        // Simulate entering the store
        characterTownAI.gameObject.SetActive(false);

        // Simulate time spent window shopping
        yield return new WaitForSeconds(Random.Range(15f, 30f));

        // Simulate exiting the store
        characterTownAI.gameObject.SetActive(true);
        characterTownAI.isActive = false;

        Debug.Log("Window shopping completed without buying anything.");
    }
}