using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Personality", menuName = "Characters/Personality")]
public class PersonalitySO : BaseSO
{

    [Header("Personality Info")]
    [SerializeField] public string TraitName;
    [SerializeField] public string TraitDescription;

    [Header("Personality Stats")]
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

    public override int GetStatValue(string stat)
    {
        switch (stat)
        {
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
