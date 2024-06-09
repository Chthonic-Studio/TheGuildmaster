using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Painting
{
    private characterTownAI characterTownAI;
    private CharacterProfile characterProfile;

    public Painting(characterTownAI characterTownAI, CharacterProfile characterProfile)
    {
        this.characterTownAI = characterTownAI;
        this.characterProfile = characterProfile;
    }


}