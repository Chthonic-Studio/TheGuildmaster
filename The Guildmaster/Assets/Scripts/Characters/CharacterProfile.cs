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
    [SerializeField] public int age;
    [SerializeField] public HouseController house;
    
    [Header("Character Variables")]
    public RaceSO selectedRace;
    public ClassSO selectedClass;
    public List<TraitSO> selectedTraits = new List<TraitSO>();
    public PersonalitySO selectedPersonality;
    public BackstorySO selectedBackstory;

    // Character Statuses
    [Header("Character Statuses")]
    [SerializeField] public List<StatusSO> CharacterStatuses = new List<StatusSO>();

    [Header("Character Inventory")]
    [SerializeField] public List<ItemSO> CharacterInventory = new List<ItemSO>();
    private Inventory inventory;
    [SerializeField] public int gold;
    

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
    [SerializeField] public int Armor;
    
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
  

    //Dictionary for all stats
    #region Stats Dictionary
    Dictionary<string, int> Stats = new Dictionary<string, int>()
    {
        {"Health", 0},
        {"Mana", 0},
        {"Strength", 0},
        {"Agility", 0},
        {"Constitution", 0},
        {"Wisdom", 0},
        {"Intelligence", 0},
        {"Charisma", 0},
        {"Adaptability", 0},
        {"Tenacity", 0},
        {"Loyalty", 0},
        {"Ambition", 0},
        {"Good", 0},
        {"Evil", 0},
        {"Leadership", 0},
        {"Willpower", 0},
        {"Luck", 0},
        {"Perception", 0},
        {"Morale", 0},
        {"FireAffinity", 0},
        {"WaterAffinity", 0},
        {"EarthAffinity", 0},
        {"AirAffinity", 0},
        {"LightAffinity", 0},
        {"DarkAffinity", 0},
        {"ArcaneAffinity", 0},
        {"NatureAffinity", 0},
        {"PsionicAffinity", 0},
        {"LightningAffinity", 0},
        {"FireResistance", 0},
        {"WaterResistance", 0},
        {"EarthResistance", 0},
        {"AirResistance", 0},
        {"LightResistance", 0},
        {"DarkResistance", 0},
        {"ArcaneResistance", 0},
        {"NatureResistance", 0},
        {"LightningResistance", 0},
        {"PsionicResistance", 0},
        {"Armor", 0},
        {"Sociability", 0},
        {"Confidence", 0},
        {"Empathy", 0},
        {"Humor", 0},
        {"Curiosity", 0},
        {"Creativity", 0},
        {"Discipline", 0},
        {"Patience", 0},
        {"Honesty", 0},
        {"Bravery", 0},
        {"Persuasion", 0},
        {"Intimidation", 0},
        {"Deception", 0},
        {"Diplomacy", 0},
        {"Aggression", 0},
        {"Resourcefulness", 0},
        {"Cunning", 0},
        {"Integrity", 0},
        {"Humility", 0},
        // Add all other stats here
    };
    #endregion

    private void Awake()
    {
        charSpawner = GameObject.Find("CharacterSpawner");
        if (charSpawner == null)
        {
            Debug.LogError("CharacterSpawner not found");
        
        }

        inventory = new Inventory(CharacterInventory); // Initialize Inventory with CharacterInventory list
        
        DontDestroyOnLoad(gameObject);

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

        // Assign a house to the character
        house = HousingManager.Instance.AssignHouseToCharacter(this);

        // Set the name of the GameObject
        gameObject.name = characterFirstName + " " + characterLastName + " " + characterID;

        // Initialize Level and Experience
        level = 1;
        experience = 0;

        // Initialize gold
        gold = UnityEngine.Random.Range(0, 100);

        // Initialize Age
        age = UnityEngine.Random.Range(15, 30);
        
        // Calculate initial stats
        CalculateStats();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CalculateStats()
    {
        if (Stats == null)
        {
            Debug.LogError("Stats dictionary is null");
            return;
        }        
        foreach (var stat in Stats.Keys.ToList())
        {
            if (!Stats.ContainsKey(stat))
            {
                Debug.LogError($"Stats dictionary does not contain key {stat}");
                continue;
            }
            
            try
            {
                int raceValue = selectedRace.GetStatValue(stat);

                int totalTraitValue = selectedTraits.Sum(x => x.GetStatValue(stat));

                int classValue = selectedClass.GetStatValue(stat);

                int personalityValue = selectedPersonality.GetStatValue(stat);

                int backstoryValue = selectedBackstory.GetStatValue(stat);

                int totalStatusValue = CharacterStatuses != null ? CharacterStatuses.Sum(x => x != null ? x.GetStatValue(stat) : 0) : 0;

                // Final value calculation
                Stats[stat] = CalculateFinalValue(raceValue, totalTraitValue, classValue, personalityValue, backstoryValue, totalStatusValue);
            }
            catch (Exception e)
            {
                Debug.LogError($"Error calculating stat {stat}: {e.Message}");
            }

            #region Stats Dictionary in Calculate Stats
            Health = Stats["Health"];
            Mana = Stats["Mana"];
            Strength = Stats["Strength"];
            Agility = Stats["Agility"];
            Constitution = Stats["Constitution"];
            Wisdom = Stats["Wisdom"];
            Intelligence = Stats["Intelligence"];
            Charisma = Stats["Charisma"];
            Adaptability = Stats["Adaptability"];
            Tenacity = Stats["Tenacity"];
            Loyalty = Stats["Loyalty"];
            Ambition = Stats["Ambition"];
            Good = Stats["Good"];
            Evil = Stats["Evil"];
            Leadership = Stats["Leadership"];
            Willpower = Stats["Willpower"];
            Luck = Stats["Luck"];
            Perception = Stats["Perception"];
            Morale = Stats["Morale"];
            FireAffinity = Stats["FireAffinity"];
            WaterAffinity = Stats["WaterAffinity"];
            EarthAffinity = Stats["EarthAffinity"];
            AirAffinity = Stats["AirAffinity"];
            LightAffinity = Stats["LightAffinity"];
            DarkAffinity = Stats["DarkAffinity"];
            ArcaneAffinity = Stats["ArcaneAffinity"];
            NatureAffinity = Stats["NatureAffinity"];
            PsionicAffinity = Stats["PsionicAffinity"];
            LightningAffinity = Stats["LightningAffinity"];
            FireResistance = Stats["FireResistance"];
            WaterResistance = Stats["WaterResistance"];
            EarthResistance = Stats["EarthResistance"];
            AirResistance = Stats["AirResistance"];
            LightResistance = Stats["LightResistance"];
            DarkResistance = Stats["DarkResistance"];
            ArcaneResistance = Stats["ArcaneResistance"];
            NatureResistance = Stats["NatureResistance"];
            LightningResistance = Stats["LightningResistance"];
            PsionicResistance = Stats["PsionicResistance"];
            Armor = Stats["Armor"];
            Sociability = Stats["Sociability"];
            Confidence = Stats["Confidence"];
            Empathy = Stats["Empathy"];
            Humor = Stats["Humor"];
            Curiosity = Stats["Curiosity"];
            Creativity = Stats["Creativity"];
            Discipline = Stats["Discipline"];
            Patience = Stats["Patience"];
            Honesty = Stats["Honesty"];
            Bravery = Stats["Bravery"];
            Persuasion = Stats["Persuasion"];
            Intimidation = Stats["Intimidation"];
            Deception = Stats["Deception"];
            Diplomacy = Stats["Diplomacy"];
            Aggression = Stats["Aggression"];
            Resourcefulness = Stats["Resourcefulness"];
            Cunning = Stats["Cunning"];
            Integrity = Stats["Integrity"];
            Humility = Stats["Humility"];
            #endregion
        }
    }

    public int CalculateFinalValue (int raceValue, int traitValue, int classValue, int personalityValue, int backstoryValue, int statusValue) 
    {
        int finalValue = raceValue + traitValue + classValue + personalityValue + backstoryValue + statusValue;
                
        return finalValue;
    }

    public void AddExperience(int amount)
    {
        int newExperience = experience + amount;

        while (newExperience >= level * 100)
        {
            int excessExperience = newExperience - level * 100;
            LevelUp();
            newExperience = excessExperience;
        }

        // If the experience is not enough to level up, add it to the value already stored in the CharacterProfile
        experience += newExperience;
    }

    private void LevelUp()
    {
        level++;
    }

    // Inventory management methods
    public bool AddItemToInventory(ItemSO item)
    {
        return inventory.AddItem(item);
    }

    public bool RemoveItemFromInventory(ItemSO item)
    {
        return inventory.RemoveItem(item);
    }

    public List<ItemSO> GetInventoryItems()
    {
        return inventory.GetItems();
    }
}
