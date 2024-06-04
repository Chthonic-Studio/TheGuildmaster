using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Skill", menuName = "Skills/Skill")]

public class SkillSO : BaseSO

{
    //Listing all Enums
    public enum SType
    {
        NotSet = 0,
        Passive = 1,
        Active = 101,
    }

    public enum DType
    {
        NotSet = 0,
        Physical = 1,
        Magical = 101,
        Healing = 201,
    }

    public enum STarget
    {
        NotSet = 0,
        Self = 1,
        Ally = 101,
        Enemy = 201,
        Area = 301,
    }

    public enum EType
    {
    None = 0, // in case an attack doesn't have an element
    Fire = 1,
    Water = 2,
    Earth = 3,
    Air = 4,
    Lightning = 5,
    Psionic = 6,
    Nature = 7,
    Light = 8,
    Dark = 9,
    Arcane = 10
    }

    //Skill info
    [Header("Skill Info")]
    [SerializeField] public string SkillName;
    [SerializeField] public string SkillDescription;

    [Header("Skill Type")]
    [SerializeField] public DType DamageType;
    [SerializeField] public STarget Target;
    [SerializeField] public SType Type;
    [SerializeField] public EType Element;

    [Header("Skill Stats")]
    [SerializeField] public Range DamageValues;
    [SerializeField] public int ManaCost;
    [SerializeField] public int Cooldown;
    [SerializeField] public int Duration;
    [SerializeField] public int Range;
    [Range(1, 100)] [SerializeField] public float Area;
    [SerializeField] public List<StatusProbability> StatusEffects;

    [Header("Skill Weight")]
    [Range(1, 10)] [SerializeField] public float SkillRarity;

    [Header("Skill Unlock Requirements")]
    [SerializeField] public int requiredPlayerLevel;
    [SerializeField] public int requiredYear;
    [SerializeField] public int requiredTowerFloor;
    [SerializeField] public int requiredCharLevel;


    public override int GetStatValue(string stat)
    {
        switch (stat)
        {
            //Main stats
            case "Damage":
                return DamageValues.RandomValue();
            case "ManaCost":
                return ManaCost;
            case "Cooldown":
                return Cooldown;
            
            default:
                return 0;
        }
    }

}

