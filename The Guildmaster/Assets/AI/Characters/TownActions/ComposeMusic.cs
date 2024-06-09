using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComposeMusic
{
    private characterTownAI characterTownAI;
    private CharacterProfile characterProfile;

    public ComposeMusic(characterTownAI characterTownAI, CharacterProfile characterProfile)
    {
        this.characterTownAI = characterTownAI;
        this.characterProfile = characterProfile;
    }

    public void StartComposing()
    {
        // Use characterTownAI and characterProfile to implement the logic for composing music
    }
}