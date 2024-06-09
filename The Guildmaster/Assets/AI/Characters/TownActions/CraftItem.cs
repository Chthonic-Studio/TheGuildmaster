using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftItem
{
    private characterTownAI characterTownAI;
    private CharacterProfile characterProfile;

    public CraftItem(characterTownAI characterTownAI, CharacterProfile characterProfile)
    {
        this.characterTownAI = characterTownAI;
        this.characterProfile = characterProfile;
    }

}