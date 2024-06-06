using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteManager : MonoBehaviour
{
    public static SpriteManager Instance;
    public List<CharacterSpriteSO> characterSprites;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Debug.LogError("More than one instance of SpriteManager found!");
            Destroy(this.gameObject);
        }
    }
}
