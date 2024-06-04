using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Class", menuName = "Characters/Class")]
public class ClassSO : BaseSO
{

    [Header("Class Info")]
    [SerializeField] public string Name;
    [SerializeField] public string Description;

    [Header("Class Stats")]
    [SerializeField] public Range Health;
    [SerializeField] public Range Mana;
    [SerializeField] public Range Strength;
    [SerializeField] public Range Agility;
    [SerializeField] public Range Constitution;
    [SerializeField] public Range Wisdom;
    [SerializeField] public Range Intelligence;
    [SerializeField] public Range Charisma;

    [Header("Affinities & Resistances")]

    [SerializeField] public Range FireAffinity;
    [SerializeField] public Range WaterAffinity;
    [SerializeField] public Range EarthAffinity;
    [SerializeField] public Range AirAffinity;
    [SerializeField] public Range LightAffinity;
    [SerializeField] public Range DarkAffinity;
    [SerializeField] public Range ArcaneAffinity;
    [SerializeField] public Range NatureAffinity;
    [SerializeField] public Range LightningAffinity;
    [SerializeField] public Range PsionicAffinity;
    [SerializeField] public Range FireResistance;
    [SerializeField] public Range WaterResistance;
    [SerializeField] public Range EarthResistance;
    [SerializeField] public Range AirResistance;
    [SerializeField] public Range LightResistance;
    [SerializeField] public Range DarkResistance;
    [SerializeField] public Range ArcaneResistance;
    [SerializeField] public Range NatureResistance;
    [SerializeField] public Range LightningResistance;
    [SerializeField] public Range PsionicResistance;

    [Header("Class Weight")]
    [Range(1, 10)] [SerializeField] public float weight;

    [Header("Class Skills")]
    [SerializeField] public List<SkillSO> ClassSkills;
    
    [Header("Class Unlock Requirements")]
    [SerializeField] public int requiredPlayerLevel;
    [SerializeField] public int requiredYear;
    [SerializeField] public int requiredTowerFloor;

    [Header("Class Recruitment & Maintenance Modifiers")]
    [SerializeField] public Range recruitmentCost;
    [SerializeField] public Range maintenanceCost;

    public override int GetStatValue(string stat)
    {
        switch (stat)
        {
            case "Health":
                return Health.RandomValue();
            case "Mana":
                return Mana.RandomValue();
            case "Strength":
                return Strength.RandomValue();
            case "Agility":
                return Agility.RandomValue();
            case "Constitution":
                return Constitution.RandomValue();
            case "Wisdom":
                return Wisdom.RandomValue();
            case "Intelligence":
                return Intelligence.RandomValue();
            case "Charisma":
                return Charisma.RandomValue();
            case "FireAffinity":
                return FireAffinity.RandomValue();
            case "WaterAffinity":
                return WaterAffinity.RandomValue();
            case "EarthAffinity":
                return EarthAffinity.RandomValue();
            case "AirAffinity":
                return AirAffinity.RandomValue();
            case "LightAffinity":
                return LightAffinity.RandomValue();
            case "DarkAffinity":
                return DarkAffinity.RandomValue();
            case "ArcaneAffinity":
                return ArcaneAffinity.RandomValue();
            case "NatureAffinity":
                return NatureAffinity.RandomValue();
            case "LightningAffinity":
                return LightningAffinity.RandomValue();
            case "PsionicAffinity":
                return PsionicAffinity.RandomValue();
            case "FireResistance":
                return FireResistance.RandomValue();
            case "WaterResistance":
                return WaterResistance.RandomValue();
            case "EarthResistance":
                return EarthResistance.RandomValue();
            case "AirResistance":
                return AirResistance.RandomValue();
            case "LightResistance":
                return LightResistance.RandomValue();
            case "DarkResistance":
                return DarkResistance.RandomValue();
            case "ArcaneResistance":
                return ArcaneResistance.RandomValue();
            case "NatureResistance":
                return NatureResistance.RandomValue();
            case "LightningResistance":
                return LightningResistance.RandomValue();
            case "PsionicResistance":
                return PsionicResistance.RandomValue();
            default:
                return 0;
        }
    }

}