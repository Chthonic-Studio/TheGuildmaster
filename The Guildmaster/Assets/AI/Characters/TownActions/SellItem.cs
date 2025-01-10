using System.Collections;
using UnityEngine;
using Pathfinding;

public class SellItem
{
    private characterTownAI characterTownAI;
    private CharacterProfile characterProfile;
    private Store store;

    public SellItem(characterTownAI characterTownAI, CharacterProfile characterProfile)
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

        // Start coroutine to handle the selling process
        characterTownAI.StartCoroutine(SellingProcess());
    }

    private IEnumerator SellingProcess()
    {
        // Wait until the character reaches the store
        yield return new WaitUntil(() => Vector3.Distance(characterTownAI.transform.position, store.transform.position) < 1f);

        // Simulate entering the store
        characterTownAI.gameObject.SetActive(false);

        // Execute selling logic
        yield return SellRandomItem();

        // Simulate time spent in the store
        yield return new WaitForSeconds(Random.Range(2f, 5f));

        // Simulate exiting the store
        characterTownAI.gameObject.SetActive(true);
        characterTownAI.isActive = false;
    }

    private IEnumerator SellRandomItem()
    {
        if (characterProfile.CharacterInventory.Count == 0)
        {
            Debug.LogWarning("Character inventory is empty.");
            yield break;
        }

        // Select a random item from the character's inventory
        ItemSO itemToSell = characterProfile.CharacterInventory[Random.Range(0, characterProfile.CharacterInventory.Count)];
        float price = store.CalculatePrice(new ItemInstance(itemToSell, 100, itemToSell.itemMaxDurability)); // Assuming max quality and durability for simplicity

        // Add gold to the character and store
        characterProfile.CharacterInventory.Remove(itemToSell);
        characterProfile.gold += (int)price;
        store.owner.Gold -= (int)price; // Deduct gold from store owner
        store.Inventory.Add(new ItemInstance(itemToSell, 100, itemToSell.itemMaxDurability));

        Debug.Log($"Sold {itemToSell.itemName} for {price} gold");

        yield return null;
    }
}