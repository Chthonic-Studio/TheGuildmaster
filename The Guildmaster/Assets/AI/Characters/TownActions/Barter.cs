using System.Collections;
using UnityEngine;
using Pathfinding;

public class Barter
{
    private characterTownAI characterTownAI;
    private CharacterProfile characterProfile;
    private NPCProfile npcProfile;

    public Barter(characterTownAI characterTownAI, CharacterProfile characterProfile)
    {
        this.characterTownAI = characterTownAI;
        this.characterProfile = characterProfile;

        Execute();
    }

    private void Execute()
    {
        // Find a random NPC to barter with
        npcProfile = FindRandomNPC();
        if (npcProfile == null)
        {
            Debug.LogWarning("No available NPC found for bartering.");
            return;
        }

        // Set the destination for both the character and NPC to meet
        Vector3 meetingPoint = (characterTownAI.transform.position + npcProfile.transform.position) / 2;

        // Set the destination for the character
        AIDestinationSetter characterDestinationSetter = characterTownAI.GetComponent<AIDestinationSetter>();
        characterDestinationSetter.target = new GameObject("MeetingPoint").transform;
        characterDestinationSetter.target.position = meetingPoint;

        // Set the destination for the NPC
        AIDestinationSetter npcDestinationSetter = npcProfile.GetComponent<AIDestinationSetter>();
        npcDestinationSetter.target = characterDestinationSetter.target;

        // Start coroutine to handle the bartering process
        characterTownAI.StartCoroutine(BarteringProcess());
    }

    private NPCProfile FindRandomNPC()
    {
        NPCProfile[] npcs = FindObjectsOfType<NPCProfile>();
        if (npcs.Length == 0)
        {
            return null;
        }

        return npcs[Random.Range(0, npcs.Length)];
    }

    private IEnumerator BarteringProcess()
    {
        // Wait until both the character and NPC reach the meeting point
        yield return new WaitUntil(() => Vector3.Distance(characterTownAI.transform.position, npcProfile.transform.position) < 1f);

        // Execute bartering logic
        yield return BarterLogic();

        // Simulate time spent bartering
        yield return new WaitForSeconds(Random.Range(10f, 20f));

        // End the bartering process and make the NPC and character go their own ways
        characterTownAI.isActive = false;
        npcProfile.GetComponent<AIDestinationSetter>().target = null;
    }

    private IEnumerator BarterLogic()
    {
        int action = Random.Range(0, 3); // 0: NPC buys item, 1: Character buys item, 2: Nothing happens

        switch (action)
        {
            case 0:
                yield return NPCBuysItem();
                break;
            case 1:
                yield return CharacterBuysItem();
                break;
            case 2:
                Debug.Log("Barter reached nothing.");
                break;
        }

        yield return null;
    }

    private IEnumerator NPCBuysItem()
    {
        if (characterProfile.CharacterInventory.Count == 0)
        {
            Debug.Log("Character inventory is empty.");
            yield break;
        }

        // Select a random item from the character's inventory
        ItemSO itemToSell = characterProfile.CharacterInventory[Random.Range(0, characterProfile.CharacterInventory.Count)];
        float price = itemToSell.itemValue; // For simplicity, using itemValue as the price

        // Add gold to the character and NPC
        characterProfile.CharacterInventory.Remove(itemToSell);
        characterProfile.gold += (int)price;
        npcProfile.Gold -= (int)price; // Deduct gold from NPC

        Debug.Log($"NPC bought {itemToSell.itemName} for {price} gold");

        yield return null;
    }

    private IEnumerator CharacterBuysItem()
    {
        if (npcProfile.BarterInventory.Count == 0)
        {
            Debug.Log("NPC's barter inventory is empty.");
            yield break;
        }

        // Select a random item from the NPC's barter inventory
        ItemSO itemToBuy = npcProfile.BarterInventory[Random.Range(0, npcProfile.BarterInventory.Count)];
        float price = itemToBuy.itemValue; // For simplicity, using itemValue as the price

        if (characterProfile.gold >= price)
        {
            if (characterProfile.CharacterInventory.Count < 10) // Assuming a max inventory size of 10
            {
                characterProfile.CharacterInventory.Add(itemToBuy);
                characterProfile.gold -= (int)price;
                npcProfile.Gold += (int)price; // Add gold to NPC

                Debug.Log($"Character bought {itemToBuy.itemName} for {price} gold");
            }
            else
            {
                Debug.Log("Character's inventory is full");
            }
        }
        else
        {
            Debug.Log("Not enough gold to buy the item");
        }

        yield return null;
    }
}