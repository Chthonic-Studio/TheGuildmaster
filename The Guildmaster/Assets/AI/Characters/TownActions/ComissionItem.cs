using UnityEngine;

public class BuyItemAction
{
    private characterTownAI characterTownAI;
    private CharacterProfile characterProfile;
    private ItemSO itemToBuy;

    public BuyItemAction(characterTownAI characterTownAI, CharacterProfile characterProfile, ItemSO itemToBuy)
    {
        this.characterTownAI = characterTownAI;
        this.characterProfile = characterProfile;
        this.itemToBuy = itemToBuy;

        Execute();
    }

    public void Execute()
    {
        if (characterProfile.gold >= itemToBuy.itemValue && characterProfile.CharacterInventory.Count < 10)
        {
            characterProfile.CharacterInventory.Add(itemToBuy);
            characterProfile.gold -= itemToBuy.itemValue;
            Debug.Log($"Bought {itemToBuy.itemName} for {itemToBuy.itemValue} gold");
        }
        else if (characterProfile.gold < itemToBuy.itemValue)
        {
            Debug.LogWarning("Not enough gold to buy the item");
        }
        else if (characterProfile.CharacterInventory.Count >= 10)
        {
            Debug.LogWarning("Inventory is full");
        }
    }
}