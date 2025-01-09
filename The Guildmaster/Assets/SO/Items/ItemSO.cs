using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Item/Item")]

public class ItemSO : ScriptableObject
{
    [Header("Item Characteristics")]
    [SerializeField] public string itemName;
    [SerializeField] public string itemDescription;
    [SerializeField] public Sprite itemSprite;
    [SerializeField] public int itemValue;
    [SerializeField] public int itemQuality;
    [SerializeField] public bool isTradable;
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

    [Header("Item Main Stats")]
    [SerializeField] public int Health;
    [SerializeField] public int Mana;
    [SerializeField] public int Strength;
    [SerializeField] public int Agility;
    [SerializeField] public int Constitution;
    [SerializeField] public int Wisdom;
    [SerializeField] public int Intelligence;
    [SerializeField] public int Charisma;
    [SerializeField] public int Good;
    [SerializeField] public int Evil;
    [SerializeField] public int Leadership;
    [SerializeField] public int Willpower;
    [SerializeField] public int Luck;
    [SerializeField] public int Perception;

    //Item Affinities & Resistances
    [Header("Item Affinities & Resistances")]
    [SerializeField] public int FireAffinity;
    [SerializeField] public int WaterAffinity;
    [SerializeField] public int EarthAffinity;
    [SerializeField] public int AirAffinity;
    [SerializeField] public int LightAffinity;
    [SerializeField] public int DarkAffinity;
    [SerializeField] public int ArcaneAffinity;
    [SerializeField] public int NatureAffinity;
    [SerializeField] public int PsionicAffinity;
    [SerializeField] public int LightningAffinity;
    [SerializeField] public int FireResistance;
    [SerializeField] public int WaterResistance;
    [SerializeField] public int EarthResistance;
    [SerializeField] public int AirResistance;
    [SerializeField] public int LightResistance;
    [SerializeField] public int DarkResistance;
    [SerializeField] public int ArcaneResistance;
    [SerializeField] public int NatureResistance;
    [SerializeField] public int LightningResistance;
    [SerializeField] public int PsionicResistance;
    [SerializeField] public int Armor;


}