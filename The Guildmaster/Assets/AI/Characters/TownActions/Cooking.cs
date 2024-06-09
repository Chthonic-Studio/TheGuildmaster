using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cooking
{
    private characterTownAI characterTownAI;
    private CharacterProfile characterProfile;

    public Cooking(characterTownAI characterTownAI, CharacterProfile characterProfile)
    {
        this.characterTownAI = characterTownAI;
        this.characterProfile = characterProfile;
    }


}