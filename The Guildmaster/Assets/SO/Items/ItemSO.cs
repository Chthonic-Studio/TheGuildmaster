using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Item/Item")]

public class ItemSO : ScriptableObject
{
    [SerializeField] public string itemName;
    [SerializeField] public string itemDescription;
    [SerializeField] public Sprite itemSprite;
    [SerializeField] public int itemValue;
    [SerializeField] public int itemWeight;
    [SerializeField] public int itemDurability;
    [SerializeField] public int itemMaxDurability;
    [SerializeField] public int itemLevel;
    [SerializeField] public int itemRarity;
    [SerializeField] public int itemStackLimit;
    [SerializeField] public bool isStackable;
    [SerializeField] public bool isEquippable;
    [SerializeField] public bool isConsumable;
    [SerializeField] public bool isDroppable;
    [SerializeField] public bool isSellable;
    [SerializeField] public bool isDestroyable;
    [SerializeField] public bool isQuestItem;
    [SerializeField] public bool isUnique;
    [SerializeField] public bool isIndestructible;
    [SerializeField] public bool isBroken;
    [SerializeField] public bool isEquipped;
    [SerializeField] public bool isTwoHanded;
    [SerializeField] public bool isMainHand;
    [SerializeField] public bool isOffHand;
    [SerializeField] public bool isHead;
    [SerializeField] public bool isNeck;
    [SerializeField] public bool isShoulders;
    [SerializeField] public bool isChest;
    [SerializeField] public bool isBack;
    [SerializeField] public bool isWrists;
    [SerializeField] public bool isHands;
    [SerializeField] public bool isWaist;
    [SerializeField] public bool isLegs;
    [SerializeField] public bool isFeet;
    [SerializeField] public bool isFinger;
    [SerializeField] public bool isTrinket;
    [SerializeField] public bool isRelic;
    [SerializeField] public bool isPotion;
    [SerializeField] public bool isFood;
    [SerializeField] public bool isDrink;
    [SerializeField] public bool isCraftingMaterial;
    [SerializeField] public bool isReagent;
    [SerializeField] public bool isRecipe;
    [SerializeField] public bool isQuest;
    [SerializeField] public bool isKey;
    [SerializeField] public bool isCurrency;
    [SerializeField] public bool isMiscellaneous;
    [SerializeField] public bool isJunk;
    [SerializeField] public bool isWeapon;
    [SerializeField] public bool isArmor;
    [SerializeField] public bool isShield;  
    [SerializeField] public bool isRanged;
    [SerializeField] public bool isMelee;
    [SerializeField] public bool isAmmo;

}