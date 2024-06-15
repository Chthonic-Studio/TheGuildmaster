using System.Collections.Generic;
using UnityEngine;

public class HouseController : MonoBehaviour
{
    // List to store the characters living in the house
    public List<CharacterProfile> inhabitants;
    [SerializeField] private int maxInhabitants = 5;

        void Awake() 
    {
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        // Initialize the inhabitants list
        inhabitants = new List<CharacterProfile>();
    }

    public bool IsFull()
    {
        return inhabitants.Count >= maxInhabitants;
    }

    public bool ContainsInhabitant(CharacterProfile character)
    {
        return inhabitants.Contains(character);
    }

    // Method to add a character to the house
    public void AddInhabitant(CharacterProfile character)
    {
        if (!inhabitants.Contains(character))
        {
            inhabitants.Add(character);
        }
    }

    // Method to remove a character from the house
    public void RemoveInhabitant(CharacterProfile character)
    {
        if (inhabitants.Contains(character))
        {
            inhabitants.Remove(character);
        }
    }

    // Method to get all inhabitants of the house
    public List<CharacterProfile> GetInhabitants()
    {
        return inhabitants;
    }
}