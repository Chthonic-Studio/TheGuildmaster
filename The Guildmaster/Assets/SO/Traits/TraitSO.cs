using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Trait", menuName = "Characters/Trait")]
public class TraitSO : BaseSO
{

    [Header("Trait Info")]
    [SerializeField] public string TraitName;
    [SerializeField] public string TraitDescription;

    [Header("Trait Stats")]
    [SerializeField] public int Health;
    [SerializeField] public int Mana;
    [SerializeField] public int Strength;
    [SerializeField] public int Agility;
    [SerializeField] public int Constitution;
    [SerializeField] public int Wisdom;
    [SerializeField] public int Intelligence;
    [SerializeField] public int Charisma;
    [SerializeField] public int Adaptability;
    [SerializeField] public int Tenacity;
    [SerializeField] public int Loyalty;
    [SerializeField] public int Ambition;
    [SerializeField] public int Good;
    [SerializeField] public int Evil;
    [SerializeField] public int Leadership;
    [SerializeField] public int Willpower;
    [SerializeField] public int Luck;
    [SerializeField] public int Perception;
    [SerializeField] public int Morale;

    [Header("Affinities & Resistances")]

    [SerializeField] public int FireAffinity;
    [SerializeField] public int WaterAffinity;
    [SerializeField] public int EarthAffinity;
    [SerializeField] public int AirAffinity;
    [SerializeField] public int LightAffinity;
    [SerializeField] public int DarkAffinity;
    [SerializeField] public int ArcaneAffinity;
    [SerializeField] public int NatureAffinity;
    [SerializeField] public int LightningAffinity;
    [SerializeField] public int PsionicAffinity;
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

    [Header("Social Stats")]
    [SerializeField] public int Sociability;
    [SerializeField] public int Confidence;
    [SerializeField] public int Empathy;
    [SerializeField] public int Humor;
    [SerializeField] public int Curiosity;
    [SerializeField] public int Creativity;
    [SerializeField] public int Discipline;
    [SerializeField] public int Patience;
    [SerializeField] public int Honesty;
    [SerializeField] public int Bravery;
    [SerializeField] public int Persuasion;
    [SerializeField] public int Intimidation;
    [SerializeField] public int Deception;
    [SerializeField] public int Diplomacy;
    [SerializeField] public int Aggression;
    [SerializeField] public int Resourcefulness;
    [SerializeField] public int Cunning;
    [SerializeField] public int Integrity;
    [SerializeField] public int Humility;

    [Header("Trait Weight")]
    [Range(1, 10)]
    [SerializeField] public float weight;

    public override int GetStatValue(string stat)
    {
        switch (stat)
        {
            //Main stats
            case "Health":
                return Health;
            case "Mana":
                return Mana;
            case "Strength":
                return Strength;
            case "Agility":
                return Agility;
            case "Constitution":
                return Constitution;
            case "Wisdom":
                return Wisdom;
            case "Intelligence":
                return Intelligence;
            case "Charisma":
                return Charisma;
            //Personality stats
            case "Adaptability":
                return Adaptability;
            case "Tenacity":
                return Tenacity;
            case "Loyalty":
                return Loyalty;
            case "Ambition":
                return Ambition;
            case "Good":
                return Good;
            case "Evil":
                return Evil;
            case "Leadership":
                return Leadership;
            case "Willpower":
                return Willpower;
            case "Luck":
                return Luck;
            case "Perception":
                return Perception;
            case "Morale":
                return Morale;
            //Resistances & Affinities stats
            case "FireAffinity":
                return FireAffinity;
            case "WaterAffinity":
                return WaterAffinity;
            case "EarthAffinity":
                return EarthAffinity;
            case "AirAffinity":
                return AirAffinity;
            case "LightAffinity":
                return LightAffinity;
            case "DarkAffinity":
                return DarkAffinity;
            case "ArcaneAffinity":
                return ArcaneAffinity;
            case "NatureAffinity":
                return NatureAffinity;
            case "LightningAffinity":
                return LightningAffinity;
            case "PsionicAffinity":
                return PsionicAffinity;
            case "FireResistance":
                return FireResistance;
            case "WaterResistance":
                return WaterResistance;
            case "EarthResistance":
                return EarthResistance;
            case "AirResistance":
                return AirResistance;
            case "LightResistance":
                return LightResistance;
            case "DarkResistance":
                return DarkResistance;
            case "ArcaneResistance":
                return ArcaneResistance;
            case "NatureResistance":
                return NatureResistance;
            case "LightningResistance":
                return LightningResistance;
            case "PsionicResistance":
                return PsionicResistance;
            //Social stats
            case "Sociability":
                return Sociability;
            case "Confidence":
                return Confidence;
            case "Empathy":
                return Empathy;
            case "Humor":
                return Humor;
            case "Curiosity":
                return Curiosity;
            case "Creativity":
                return Creativity;
            case "Discipline":
                return Discipline;
            case "Patience":
                return Patience;
            case "Honesty":
                return Honesty;
            case "Bravery":
                return Bravery;
            case "Persuasion":
                return Persuasion;
            case "Intimidation":
                return Intimidation;
            case "Deception":
                return Deception;
            case "Diplomacy":
                return Diplomacy;
            case "Aggression":
                return Aggression;
            case "Resourcefulness":
                return Resourcefulness;
            case "Cunning":
                return Cunning;
            case "Integrity":
                return Integrity;
            case "Humility":
                return Humility;
            
            default:
                return 0;
        }
    }


}

