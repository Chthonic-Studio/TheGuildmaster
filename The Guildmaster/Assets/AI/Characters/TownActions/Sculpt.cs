using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sculpt
{
    private characterTownAI characterTownAI;
    private CharacterProfile characterProfile;

    public Sculpt(characterTownAI characterTownAI, CharacterProfile characterProfile)
    {
        this.characterTownAI = characterTownAI;
        this.characterProfile = characterProfile;
    }


}