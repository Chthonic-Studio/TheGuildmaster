using System.Collections;
using UnityEngine;
using Pathfinding;

public class BuyItem
{
    private characterTownAI characterTownAI;
    private CharacterProfile characterProfile;
    private Store store;

    public BuyItem(characterTownAI characterTownAI, CharacterProfile characterProfile)
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

        // Start coroutine to handle the buying process
        characterTownAI.StartCoroutine(BuyingProcess());
    }

    private IEnumerator BuyingProcess()
    {
        // Wait until the character reaches the store
        yield return new WaitUntil(() => Vector3.Distance(characterTownAI.transform.position, store.transform.position) < 1f);

        // Simulate entering the store
        characterTownAI.gameObject.SetActive(false);

        // Execute buying logic
        yield return BuyRandomItem();

        // Simulate time spent in the store
        yield return new WaitForSeconds(Random.Range(10f, 30f));

        // Simulate exiting the store
        characterTownAI.gameObject.SetActive(true);
        characterTownAI.isActive = false;
    }

    private IEnumerator BuyRandomItem()
    {
        if (store.Inventory.Count == 0)
        {
            Debug.LogWarning("Store inventory is empty.");
            yield break;
        }

        ItemInstance itemToBuy = store.Inventory[Random.Range(0, store.Inventory.Count)];
        float price = store.CalculatePrice(itemToBuy);

        if (characterProfile.gold >= price)
        {
            if (characterProfile.CharacterInventory.Count < 10) // Assuming a max inventory size of 10
            {
                characterProfile.CharacterInventory.Add(itemToBuy.itemTemplate);
                characterProfile.gold -= (int)price;
                store.Inventory.Remove(itemToBuy);
                store.owner.Gold += (int)price;
                Debug.Log($"Bought {itemToBuy.itemTemplate.itemName} for {price} gold");
            }
            else
            {
                Debug.LogWarning("Inventory is full");
            }
        }
        else
        {
            Debug.LogWarning("Not enough gold to buy the item");
        }

        yield return null;
    }
}