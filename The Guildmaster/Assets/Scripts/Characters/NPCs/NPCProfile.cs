using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class NPCProfile : MonoBehaviour
{
    public enum occupationType
    {
        Farmer,
        Blacksmith,
        Merchant,
        Innkeeper,
        Alchemist,
        Hunter,
        Fisherman,
        Baker,
        Tailor,
        Carpenter,
        Miner,
        Lumberjack,
        Beggar,
        Bandit,
        Thief,
        Scholar,
        Scribe,
        Sailor,
        Shipwright,
        Cook,
        Healer,
        Herbalist,
        Apothecary,
        Engineer,
        Architect,
        Mason,
        Mechanic,
        Potter,
        Glassblower,
        Weaver,
        Dyer,
        Cobbler,
        Shoemaker,
        Leatherworker,
        Tanner,
        Bowyer,
        Fletcher,
        Jeweler,
        Printer,
        Cartographer,
        Painter,
        Sculptor,
        Spinner
    }
    
    
    [Header("NPC Profile")]
    public string npcID;
    [SerializeField] public string firstName;
    [SerializeField] public string lastName;
    [SerializeField] public string npcGender;
    [SerializeField] public RaceSO npcRace;
    [SerializeField] public int age;
    [SerializeField] public occupationType Occupation;
    [SerializeField] public int Gold;
    [SerializeField] public HouseController house;

    [Header("NPC Personality")]
    [SerializeField] public int sociability;
    [SerializeField] public int patience;
    [SerializeField] public int honesty;
    [SerializeField] public int bravery;
    [SerializeField] public int curiosity;
    [SerializeField] public int ambition;
    [SerializeField] public int loyalty;
    [SerializeField] public int greed;
    [SerializeField] public int kindness;
    [SerializeField] public int workEthic;
    [SerializeField] public int intelligence;
    [SerializeField] public int creativity;

    [Header("NPC Inventory")]
    [SerializeField] public List<ItemSO> BarterInventory = new List<ItemSO>(); // Barter inventory for NPCs
    [SerializeField] public Store Store;

    private List<GameObject> spawners;



    public void Initialize()
    {
        // Add this NPCProfile to the activeNPC list in the NPCManager.
        NPCManager.Instance.activeNPC.Add(this);

        //Select random personality values
        sociability = UnityEngine.Random.Range(-50,50);
        patience = UnityEngine.Random.Range(-50,50);
        honesty = UnityEngine.Random.Range(-50,50);
        bravery = UnityEngine.Random.Range(-50,50);
        curiosity = UnityEngine.Random.Range(-50,50);
        ambition = UnityEngine.Random.Range(-50,50);
        loyalty = UnityEngine.Random.Range(-50,50);
        greed = UnityEngine.Random.Range(-50,50);
        kindness = UnityEngine.Random.Range(-50,50);
        workEthic = UnityEngine.Random.Range(-50,50);
        intelligence = UnityEngine.Random.Range(-50,50);
        creativity = UnityEngine.Random.Range(-50,50);

        InitializeBarterInventory(); // Initialize barter inventory based on profession
    }
    
    void Awake()
    {
        // Find the spawners and store them in a list.
        spawners = new List<GameObject>
        {
            GameObject.Find("NPCSpawnerEast"),
            GameObject.Find("NPCSpawnerWest"),
            GameObject.Find("NPCSpawnerSouth")
        };

        DontDestroyOnLoad(this.gameObject);
    }
    
    // Start is called before the first frame update
    void Start()
    {
        // Randomly select one of the spawners.
        GameObject selectedSpawner = spawners[UnityEngine.Random.Range(0, spawners.Count)];

        // Set the NPC's position to the spawner's position.
        transform.position = selectedSpawner.transform.position;   

        // Assign the NPC to a house
        house = HousingManager.Instance.AssignHouseToNPC(this);     
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        NPCManager.Instance.UnspawnNPC(this);
    }

    private void InitializeBarterInventory()
    {
        // Example logic to add items based on NPC's profession
        switch (Occupation)
        {
            case occupationType.Farmer:
                // Add farming-related items
                BarterInventory.Add(Resources.Load<ItemSO>("Items/Farmer/Seeds"));
                BarterInventory.Add(Resources.Load<ItemSO>("Items/Farmer/Tools"));
                break;
            case occupationType.Blacksmith:
                // Add blacksmith-related items
                BarterInventory.Add(Resources.Load<ItemSO>("Items/Blacksmith/Weapons"));
                BarterInventory.Add(Resources.Load<ItemSO>("Items/Blacksmith/Armor"));
                break;
            // Add cases for other professions...
            default:
                // Add general items
                BarterInventory.Add(Resources.Load<ItemSO>("Items/Miscellaneous"));
                break;
        }
    }

    
}
