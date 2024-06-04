using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterGenderSelection
{
    private static readonly System.Random random = new System.Random();

    public static string GetRandomGender()
    {
        int randomNumber = random.Next(1, 101); // Generates a random number between 1 and 100

        if (randomNumber <= 40)
        {
            return "Male";
        }
        else if (randomNumber <= 80)
        {
            return "Female";
        }
        else
        {
            return "Non Binary";
        }
    }
}

