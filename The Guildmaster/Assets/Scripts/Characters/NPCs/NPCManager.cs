using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class NPCManager : MonoBehaviour
{
    public static NPCManager Instance { get; private set; }

    // Maximum number of active NPCs.
    private const int MaxNPCs = 130;

    //Names database.
    [SerializeField] public NamesDatabase namesDatabase;

    //All possible races
    [SerializeField] public List<RaceSO> allRaces;

    //NPC prefab
    [SerializeField] public GameObject npcPrefab;

    public List<NPCProfile> activeNPC = new List<NPCProfile>();

    private Dictionary<NPCProfile.occupationType, int> occupationCounts = new Dictionary<NPCProfile.occupationType, int>();
    private Dictionary<NPCProfile.occupationType, int> occupationLimits = new Dictionary<NPCProfile.occupationType, int>()
    {
        { NPCProfile.occupationType.Innkeeper, 3 },
        { NPCProfile.occupationType.Blacksmith, 2 },
        { NPCProfile.occupationType.Merchant, 6 },
        { NPCProfile.occupationType.Farmer, 10 },
        { NPCProfile.occupationType.Alchemist, 2 },
        { NPCProfile.occupationType.Hunter, 3 },
        { NPCProfile.occupationType.Fisherman, 5 },
        { NPCProfile.occupationType.Baker, 2 },
        { NPCProfile.occupationType.Tailor, 3 },
        { NPCProfile.occupationType.Carpenter, 3 },
        { NPCProfile.occupationType.Miner, 10 },
        { NPCProfile.occupationType.Lumberjack, 10 },
        { NPCProfile.occupationType.Beggar, 10 },
        { NPCProfile.occupationType.Bandit, 5 },
        { NPCProfile.occupationType.Thief, 5 },
        { NPCProfile.occupationType.Scholar, 3 },
        { NPCProfile.occupationType.Scribe, 2 },
        { NPCProfile.occupationType.Sailor, 8 },
        { NPCProfile.occupationType.Shipwright, 2 },
        { NPCProfile.occupationType.Cook, 5 },
        { NPCProfile.occupationType.Healer, 3 },
        { NPCProfile.occupationType.Herbalist, 2 },
        { NPCProfile.occupationType.Apothecary, 2 },
        { NPCProfile.occupationType.Engineer, 2 },
        { NPCProfile.occupationType.Architect, 2 },
        { NPCProfile.occupationType.Mason, 3 },
        { NPCProfile.occupationType.Mechanic, 1 },
        { NPCProfile.occupationType.Potter, 3 },
        { NPCProfile.occupationType.Glassblower, 1 },
        { NPCProfile.occupationType.Weaver, 1 },
        { NPCProfile.occupationType.Dyer, 1 },
        { NPCProfile.occupationType.Cobbler, 2 },
        { NPCProfile.occupationType.Shoemaker, 1 },
        { NPCProfile.occupationType.Leatherworker, 4 },
        { NPCProfile.occupationType.Tanner, 2 },
        { NPCProfile.occupationType.Bowyer, 1 },
        { NPCProfile.occupationType.Fletcher, 1 },
        { NPCProfile.occupationType.Jeweler, 1 },
        { NPCProfile.occupationType.Printer, 1 },
        { NPCProfile.occupationType.Cartographer, 1 },
        { NPCProfile.occupationType.Painter, 1 },
        { NPCProfile.occupationType.Sculptor, 1 },
        { NPCProfile.occupationType.Spinner, 1 }
    };

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public bool RegisterNPC(NPCProfile npc)
    {
        if (occupationCounts.ContainsKey(npc.Occupation) && occupationCounts[npc.Occupation] < occupationLimits[npc.Occupation])
        {
            occupationCounts[npc.Occupation]++;
            return true;
        }
        else if (!occupationCounts.ContainsKey(npc.Occupation) && 1 <= occupationLimits[npc.Occupation])
        {
            occupationCounts[npc.Occupation] = 1;
            return true;
        }
        return false;
    }

    public List<NPCProfile.occupationType> GetAvailableOccupations()
    {
        List<NPCProfile.occupationType> availableOccupations = new List<NPCProfile.occupationType>();
        foreach (var occupation in occupationLimits.Keys)
        {
            if (!occupationCounts.ContainsKey(occupation) || occupationCounts[occupation] < occupationLimits[occupation])
            {
                availableOccupations.Add(occupation);
            }
        }
        return availableOccupations;
    }

    public bool CanSpawnNPC()
    {
        return activeNPC.Count < MaxNPCs;
    }

    public void UnspawnNPC(NPCProfile npc)
    {
        // Remove the NPC from the activeNPC list.
        activeNPC.Remove(npc);

        // Decrease the count of the NPC's occupation.
        if (occupationCounts.ContainsKey(npc.Occupation))
        {
            occupationCounts[npc.Occupation]--;
            if (occupationCounts[npc.Occupation] == 0)
            {
                occupationCounts.Remove(npc.Occupation);
            }
        }
    }

    public void SpawnNPC()
    {
        NPCSpawner.SpawnNPC(this, namesDatabase);
    }
}
