using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    private List<ItemSO> items;

    public Inventory(List<ItemSO> items)
    {
        this.items = items;
    }

    // Add an item to the inventory
    public bool AddItem(ItemSO item)
    {
        if (items.Count >= 10)
        {
            Debug.LogWarning("Inventory is full");
            return false;
        }

        items.Add(item);
        Debug.Log($"Item {item.itemName} added to inventory");
        return true;
    }

    // Remove an item from the inventory
    public bool RemoveItem(ItemSO item)
    {
        if (items.Remove(item))
        {
            Debug.Log($"Item {item.itemName} removed from inventory");
            return true;
        }

        Debug.LogWarning("Item not found in inventory");
        return false;
    }

    // List all items in the inventory
    public List<ItemSO> GetItems()
    {
        return items;
    }

    // Check if inventory contains a specific item
    public bool ContainsItem(ItemSO item)
    {
        return items.Contains(item);
    }
}