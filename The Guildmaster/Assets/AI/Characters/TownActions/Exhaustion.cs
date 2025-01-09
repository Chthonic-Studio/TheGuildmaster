using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Exhaustion
{
    private characterTownAI characterTownAI;
    private CharacterProfile characterProfile;

    public Exhaustion(characterTownAI characterTownAI, CharacterProfile characterProfile)
    {
        this.characterTownAI = characterTownAI;
        this.characterProfile = characterProfile;
    }

    public IEnumerator FaintCoroutine()
    {
        characterTownAI.SetStateToFainted(); // Set the character's state to fainted

        // Reduce fatigue each second while fainted
        while (characterProfile.fatigue > 0)
        {
            characterProfile.fatigue -= 1f; // Reduce fatigue each second

            if (characterProfile.fatigue < 50)
            {
                float wakeUpChance = (50 - characterProfile.fatigue) * 0.02f; // Incremental chance per second to wake up

                if (UnityEngine.Random.value < wakeUpChance)
                {
                    // Character wakes up
                    characterTownAI.SetStateToIdle(); // Set the character's state back to idle
                    characterTownAI.GoToSleep(); // Character goes to sleep after waking up
                    yield break; // Exit the faint coroutine
                }
            }

            yield return new WaitForSeconds(1f); // Wait for 1 second before reducing fatigue again
        }
    }
}