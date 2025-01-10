using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInstance
{
    public ItemSO itemTemplate; // Reference to the ItemSO template
    public int currentDurability;
    public int quality;

    public ItemInstance(ItemSO template, int quality, int durability)
    {
        this.itemTemplate = template;
        this.quality = quality;
        this.currentDurability = durability;
    }

    // Function to copy an ItemInstance
    public ItemInstance Copy()
    {
        return new ItemInstance(itemTemplate, quality, currentDurability);
    }
}