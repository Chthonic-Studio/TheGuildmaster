using System.Collections.Generic;
using UnityEngine;

public class HouseController : MonoBehaviour
{
    // List to store the characters living in the house
    public List<CharacterProfile> characterInhabitants;
    public List<NPCProfile> npcInhabitants;
    [SerializeField] private int maxInhabitants = 5;

    void Awake() 
    {
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        // Initialize the inhabitants list
        characterInhabitants = new List<CharacterProfile>();
        npcInhabitants = new List<NPCProfile>();
    }

    public bool IsFull()
    {
        return (characterInhabitants.Count + npcInhabitants.Count) >= maxInhabitants;
    }

    public bool ContainsCharacter(CharacterProfile character)
    {
        return characterInhabitants.Contains(character);
    }

    public bool ContainsNPC(NPCProfile npc)
    {
        return npcInhabitants.Contains(npc);
    }

    // Method to add a character to the house
    public void AddCharacter(CharacterProfile character)
    {
        if (!characterInhabitants.Contains(character) && !IsFull())
        {
            characterInhabitants.Add(character);
        }
    }

    // Method to add an NPC to the house
    public void AddNPC(NPCProfile npc)
    {
        if (!npcInhabitants.Contains(npc) && !IsFull())
        {
            npcInhabitants.Add(npc);
        }
    }

    // Method to remove a character from the house
    public void RemoveCharacter(CharacterProfile character)
    {
        if (characterInhabitants.Contains(character))
        {
            characterInhabitants.Remove(character);
        }
    }

    // Method to remove an NPC from the house
    public void RemoveNPC(NPCProfile npc)
    {
        if (npcInhabitants.Contains(npc))
        {
            npcInhabitants.Remove(npc);
        }
    }

    // Method to get all character inhabitants of the house
    public List<CharacterProfile> GetCharacterInhabitants()
    {
        return characterInhabitants;
    }

    // Method to get all NPC inhabitants of the house
    public List<NPCProfile> GetNPCInhabitants()
    {
        return npcInhabitants;
    }
}