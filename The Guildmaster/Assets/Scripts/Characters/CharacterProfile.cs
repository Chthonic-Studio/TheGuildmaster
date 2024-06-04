using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class CharacterProfile : MonoBehaviour
{
    private GameObject charSpawner;
    
    [Header("Character Info")]
    public string characterID;
    [SerializeField] public string characterFirstName;
    [SerializeField] public string characterLastName;
    [SerializeField] public string characterGender;
    [SerializeField] public int level;
    [SerializeField] public int experience;
    
    [Header("Character Variables")]
    public RaceSO selectedRace;
    public ClassSO selectedClass;
    public List<TraitSO> selectedTraits = new List<TraitSO>(3);

    // Character Main Stats
    [Header("Character Main Stats")]
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

        //Character Affinities & Resistances
    [Header("Character Affinities & Resistances")]
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
    
    // Character Social Stats
    [Header("Character Social Stats")]
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
  
    private void Awake()
    {
        charSpawner = GameObject.Find("CharacterSpawner");
        if (charSpawner == null)
        {
            Debug.LogError("CharacterSpawner not found");
        
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        if (charSpawner != null)
        {
            transform.position = charSpawner.transform.position;
        }
        else
        {
            Debug.LogError("CharacterSpawner not found at start of CharacterProfile script");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
