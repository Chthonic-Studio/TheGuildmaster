using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Race", menuName = "Characters/Race")]
public class RaceSO : BaseSO
{

    [Header("Race Info")]
    [SerializeField] public string Name;
    [SerializeField] public string Description;
    [SerializeField] public Range LifeExpentancy;

    [Header("Race Stats")]
    [SerializeField] public Range Health;
    [SerializeField] public Range Mana;
    [SerializeField] public Range Strength;
    [SerializeField] public Range Agility;
    [SerializeField] public Range Constitution;
    [SerializeField] public Range Wisdom;
    [SerializeField] public Range Intelligence;
    [SerializeField] public Range Charisma;
    [SerializeField] public Range Adaptability;
    [SerializeField] public Range Tenacity;
    [SerializeField] public Range Loyalty;
    [SerializeField] public Range Ambition;
    [SerializeField] public Range Good;
    [SerializeField] public Range Evil;
    [SerializeField] public Range Leadership;
    [SerializeField] public Range Willpower;
    [SerializeField] public Range Luck;
    [SerializeField] public Range Perception;
    [SerializeField] public Range Morale;

    [Header("Affinities & Resistances")]

    [SerializeField] public Range FireAffinity;
    [SerializeField] public Range WaterAffinity;
    [SerializeField] public Range EarthAffinity;
    [SerializeField] public Range AirAffinity;
    [SerializeField] public Range LightAffinity;
    [SerializeField] public Range DarkAffinity;
    [SerializeField] public Range ArcaneAffinity;
    [SerializeField] public Range NatureAffinity;
    [SerializeField] public Range PsionicAffinity;
    [SerializeField] public Range LightningAffinity;
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

    [Header("Social Stats")]
    [SerializeField] public Range Sociability;
    [SerializeField] public Range Confidence;
    [SerializeField] public Range Empathy;
    [SerializeField] public Range Humor;
    [SerializeField] public Range Curiosity;
    [SerializeField] public Range Creativity;
    [SerializeField] public Range Discipline;
    [SerializeField] public Range Patience;
    [SerializeField] public Range Honesty;
    [SerializeField] public Range Bravery;
    [SerializeField] public Range Persuasion;
    [SerializeField] public Range Intimidation;
    [SerializeField] public Range Deception;
    [SerializeField] public Range Diplomacy;
    [SerializeField] public Range Aggression;
    [SerializeField] public Range Resourcefulness;
    [SerializeField] public Range Cunning;
    [SerializeField] public Range Integrity;
    [SerializeField] public Range Humility;

    [Header("Race Weight")]
    [Range(1, 10)]
    [SerializeField] public float weight;

    [Header("Race Unlock Requirements")]
    [SerializeField] public int requiredPlayerLevel;
    [SerializeField] public int requiredYear;
    [SerializeField] public int requiredTowerFloor;

    [Header("Race Recruitment & Maintenance Modifiers")]
    [SerializeField] public Range recruitmentCost;
    [SerializeField] public Range maintenanceCost;

    public override int GetStatValue(string stat)
    {
        switch (stat)
        {
            //Main stats
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
            //Personality stats
            case "Adaptability":
                return Adaptability.RandomValue();
            case "Tenacity":
                return Tenacity.RandomValue();
            case "Loyalty":
                return Loyalty.RandomValue();
            case "Ambition":
                return Ambition.RandomValue();
            case "Good":
                return Good.RandomValue();
            case "Evil":
                return Evil.RandomValue();
            case "Leadership":
                return Leadership.RandomValue();
            case "Willpower":
                return Willpower.RandomValue();
            case "Luck":
                return Luck.RandomValue();
            case "Perception":
                return Perception.RandomValue();
            case "Morale":
                return Morale.RandomValue();
            //Resistances & Affinities stats
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
            //Social stats
            case "Sociability":
                return Sociability.RandomValue();
            case "Confidence":
                return Confidence.RandomValue();
            case "Empathy":
                return Empathy.RandomValue();
            case "Humor":
                return Humor.RandomValue();
            case "Curiosity":
                return Curiosity.RandomValue();
            case "Creativity":
                return Creativity.RandomValue();
            case "Discipline":
                return Discipline.RandomValue();
            case "Patience":
                return Patience.RandomValue();
            case "Honesty":
                return Honesty.RandomValue();
            case "Bravery":
                return Bravery.RandomValue();
            case "Persuasion":
                return Persuasion.RandomValue();
            case "Intimidation":
                return Intimidation.RandomValue();
            case "Deception":
                return Deception.RandomValue();
            case "Diplomacy":
                return Diplomacy.RandomValue();
            case "Aggression":
                return Aggression.RandomValue();
            case "Resourcefulness":
                return Resourcefulness.RandomValue();
            case "Cunning":
                return Cunning.RandomValue();
            case "Integrity":
                return Integrity.RandomValue();
            case "Humility":
                return Humility.RandomValue();
            
            default:
                return 0;
        }
    }

}
