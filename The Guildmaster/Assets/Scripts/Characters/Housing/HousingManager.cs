using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HousingManager : MonoBehaviour
{
    public List<HouseController> houses;

    void Start()
    {
        houses = new List<HouseController>();
    }

    public void AssignHouse(CharacterProfile character, HouseController house)
    {
        if (!houses.Contains(house))
        {
            Debug.LogError("House not managed by this HousingManager");
            return;
        }

        if (!house.IsFull())
        {
            house.AddInhabitant(character);
        }
        else
        {
            Debug.LogError("House is full");
        }
    }

    public void RemoveHouse(CharacterProfile character, HouseController house)
    {
        if (!houses.Contains(house))
        {
            Debug.LogError("House not managed by this HousingManager");
            return;
        }

        if (house.ContainsInhabitant(character))
        {
            house.RemoveInhabitant(character);
        }
        else
        {
            Debug.LogError("Character not found in house");
        }
    }

    public void MoveCharacter(CharacterProfile character, HouseController oldHouse, HouseController newHouse)
    {
        RemoveHouse(character, oldHouse);
        AssignHouse(character, newHouse);
    }
}
