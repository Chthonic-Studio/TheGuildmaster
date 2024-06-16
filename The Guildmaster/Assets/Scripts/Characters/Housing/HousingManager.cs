using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class HousingManager : MonoBehaviour
{
    public List<HouseController> houses;
    public static HousingManager Instance { get; private set; }

    void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {

    }

    public HouseController AssignHouseToCharacter(CharacterProfile character)
    {
        // Shuffle the houses list
        houses = houses.OrderBy(a => Guid.NewGuid()).ToList();

        foreach (HouseController house in houses)
        {
            if (!house.IsFull())
            {
                house.AddCharacter(character);
                return house;
            }
        }

        // If all houses are full
        Debug.LogError("All houses are full");
        return null;
    }

    public HouseController AssignHouseToNPC(NPCProfile npc)
    {
        // Shuffle the houses list
        houses = houses.OrderBy(a => Guid.NewGuid()).ToList();

        foreach (HouseController house in houses)
        {
            if (!house.IsFull())
            {
                house.AddNPC(npc);
                return house;
            }
        }

        // If all houses are full
        Debug.LogError("All houses are full");
        return null;
    }

    public void RemoveHouseFromCharacter(CharacterProfile character, HouseController house)
    {
        if (!houses.Contains(house))
        {
            Debug.LogError("House not managed by this HousingManager");
            return;
        }

        if (house.ContainsCharacter(character))
        {
            house.RemoveCharacter(character);
        }
        else
        {
            Debug.LogError("Character not found in house");
        }
    }

    public void RemoveHouseFromNPC(NPCProfile npc, HouseController house)
    {
        if (!houses.Contains(house))
        {
            Debug.LogError("House not managed by this HousingManager");
            return;
        }

        if (house.ContainsNPC(npc))
        {
            house.RemoveNPC(npc);
        }
        else
        {
            Debug.LogError("NPC not found in house");
        }
    }

    public void MoveCharacter(CharacterProfile character, HouseController oldHouse, HouseController newHouse)
    {
        RemoveHouseFromCharacter(character, oldHouse);
        AssignHouseToCharacter(character);
    }

    public void MoveNPC(NPCProfile npc, HouseController oldHouse, HouseController newHouse)
    {
        RemoveHouseFromNPC(npc, oldHouse);
        AssignHouseToNPC(npc);
    }
}
