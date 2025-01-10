using System.Collections;
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
        Restaurant,
        Inn,
        Tavern,
        Library,
        MagicShop
        // Add other store types as needed
    }

    [Header("Store Information")]
    [SerializeField] public string storeName;
    [SerializeField] public StoreType storeType;
    [SerializeField] public NPCProfile owner;
    [SerializeField] public List<ItemInstance> Inventory = new List<ItemInstance>();
    [SerializeField] public Dictionary<ItemSO, int> restockQuantities = new Dictionary<ItemSO, int>();
    [SerializeField] public Dictionary<ItemSO, (int minInterval, int maxInterval)> restockIntervals = new Dictionary<ItemSO, (int, int)>();



    private void Start()
    {
        InitializeInventory();
    }

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
                AddItemsToInventory("Items/General");
                break;
            case StoreType.Blacksmith:
                AddItemsToInventory("Items/Blacksmith");
                break;
            case StoreType.Alchemist:
                AddItemsToInventory("Items/Alchemist");
                break;
            case StoreType.Food:
                AddItemsToInventory("Items/Food");
                break;
            case StoreType.Clothing:
                AddItemsToInventory("Items/Clothing");
                break;
            // Add cases for other store types...
            default:
                AddItemsToInventory("Items/Miscellaneous");
                break;
        }
    }

    private void AddItemsToInventory(string folderPath)
    {
        ItemSO[] items = Resources.LoadAll<ItemSO>(folderPath);
        foreach (ItemSO item in items)
        {
            if (item.isSellable)
            {
                for (int i = 0; i < 5; i++) // Initial quantity of 5 for each item
                {
                    CreateAndAddItemInstance(item);
                }
                restockQuantities[item] = 5; // Set restock quantity
                restockIntervals[item] = (60, 120); // Set restock interval between 60 and 120 seconds
                StartCoroutine(RestockItem(item));
            }
        }
    }

    private void CreateAndAddItemInstance(ItemSO item)
    {
        int quality = Random.Range(1, 101); // Random quality between 1 and 100
        int durability = item.itemMaxDurability;
        ItemInstance instance = new ItemInstance(item, quality, durability);
        Inventory.Add(instance);
    }

    private IEnumerator RestockItem(ItemSO item)
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(restockIntervals[item].minInterval, restockIntervals[item].maxInterval));
            int currentCount = Inventory.FindAll(i => i.itemTemplate == item).Count;
            int restockAmount = restockQuantities[item] - currentCount;
            for (int i = 0; i < restockAmount; i++)
            {
                CreateAndAddItemInstance(item);
            }
        }
    }

    public float CalculatePrice(ItemInstance item)
    {
        // Example logic to calculate price based on owner's stats and item's quality
        float price = item.itemTemplate.itemValue * item.quality / 100f;
        price *= 1 + (owner.intelligence + owner.charisma) / 200f; // Adjust price based on owner stats
        return price;
    }
}