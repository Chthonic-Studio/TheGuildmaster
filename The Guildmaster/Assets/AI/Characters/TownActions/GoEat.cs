using System.Collections;
using UnityEngine;
using Pathfinding;

public class GoEat
{
    private characterTownAI characterTownAI;
    private CharacterProfile characterProfile;

    public GoEat(characterTownAI characterTownAI, CharacterProfile characterProfile)
    {
        this.characterTownAI = characterTownAI;
        this.characterProfile = characterProfile;

        StartGoEat();
    }

    public void StartGoEat()
    {
        // Get the AIDestinationSetter component of the character
        AIDestinationSetter destinationSetter = characterTownAI.GetComponent<AIDestinationSetter>();

        // Find the tavern and restaurant doors
        GameObject Tavern1Door = GameObject.Find("Tavern1Door");
        GameObject Tavern2Door = GameObject.Find("Tavern2Door");
        GameObject Tavern3Door = GameObject.Find("Tavern3Door");
        GameObject Restaurant1Door = GameObject.Find("Restaurant1Door");
        GameObject Restaurant2Door = GameObject.Find("Restaurant2Door");

        // Create arrays of the tavern and restaurant doors
        GameObject[] tavernDoors = new GameObject[] { Tavern1Door, Tavern2Door, Tavern3Door };
        GameObject[] restaurantDoors = new GameObject[] { Restaurant1Door, Restaurant2Door };

        // Calculate the probability of going to a restaurant based on the character's gold
        float restaurantProbability = CalculateRestaurantProbability(characterProfile.gold);

        // Select a random eating place based on the calculated probability
        GameObject randomEatingPlaceDoor;
        if (Random.value < restaurantProbability)
        {
            randomEatingPlaceDoor = restaurantDoors[Random.Range(0, restaurantDoors.Length)];
        }
        else
        {
            randomEatingPlaceDoor = tavernDoors[Random.Range(0, tavernDoors.Length)];
        }

        // Get the TownDoor component of the random eating place door
        TownDoor door = randomEatingPlaceDoor.GetComponent<TownDoor>();

        if (door != null)
        {
            // Set the target of the AIDestinationSetter to the door
            destinationSetter.target = door.transform;

            // Start the coroutine to make the character wait at the door. Select a min and max waiting time
            characterTownAI.StartCoroutine(characterTownAI.WaitAtDoor(45, 60));
        }
        else
        {
            Debug.LogError("No TownDoor found in the selected eating place door");
        }
    }

    private float CalculateRestaurantProbability(int gold)
    {
        // Base probabilities
        float minRestaurantProbability = 0.1f;
        float maxRestaurantProbability = 0.8f;

        // Clamp gold between 1000 and 10000
        gold = Mathf.Clamp(gold, 1000, 10000);

        // Calculate the probability increment based on the gold
        float increment = (gold - 1000) / 9000f * (maxRestaurantProbability - minRestaurantProbability);

        // Return the final probability
        return minRestaurantProbability + increment;
    }
}