using System.Collections.Generic;
using UnityEngine;

public class Store : MonoBehaviour
{
    public enum StoreType
    {
        General,
        Blacksmith,
        Alchemist,
        Food,
        Clothing,
        // Add other store types as needed
    }

    [Header("Store Information")]
    public string storeName;
    public StoreType storeType;
    public NPCProfile owner;
    public List<ItemSO> Inventory = new List<ItemSO>();

    public void InitializeStore(NPCProfile owner, StoreType storeType)
    {
        this.owner = owner;
        this.storeType = storeType;
        owner.Store = this; // Link the store to the NPC owner
        InitializeInventory();
    }

    private void InitializeInventory()
    {
        // Example logic to initialize store inventory based on store type
        switch (storeType)
        {
            case StoreType.General:
                // Add general store items
                Inventory.Add(Resources.Load<ItemSO>("Items/Miscellaneous"));
                Inventory.Add(Resources.Load<ItemSO>("Items/Food"));
                break;
            case StoreType.Blacksmith:
                // Add blacksmith store items
                Inventory.Add(Resources.Load<ItemSO>("Items/Weapons"));
                Inventory.Add(Resources.Load<ItemSO>("Items/Armor"));
                break;
            case StoreType.Alchemist:
                // Add alchemist store items
                Inventory.Add(Resources.Load<ItemSO>("Items/Potions"));
                Inventory.Add(Resources.Load<ItemSO>("Items/Reagents"));
                break;
            case StoreType.Food:
                // Add food store items
                Inventory.Add(Resources.Load<ItemSO>("Items/Food"));
                Inventory.Add(Resources.Load<ItemSO>("Items/Drinks"));
                break;
            case StoreType.Clothing:
                // Add clothing store items
                Inventory.Add(Resources.Load<ItemSO>("Items/Clothing"));
                Inventory.Add(Resources.Load<ItemSO>("Items/Accessories"));
                break;
            // Add cases for other store types...
            default:
                // Add general items
                Inventory.Add(Resources.Load<ItemSO>("Items/Miscellaneous"));
                break;
        }
    }
}