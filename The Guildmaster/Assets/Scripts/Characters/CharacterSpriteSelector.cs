using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSpriteSelector : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private CharacterProfile characterProfile;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        characterProfile = GetComponent<CharacterProfile>();
        SelectSprite();
    }

    public void SelectSprite()
    {
        List<CharacterSpriteSO> possibleSprites = SpriteManager.Instance.characterSprites.FindAll(
            spriteSO => spriteSO.races.Contains(characterProfile.selectedRace) && spriteSO.classes.Contains(characterProfile.selectedClass)
        );

        if (possibleSprites.Count > 0)
        {
            CharacterSpriteSO selectedSpriteSO = possibleSprites[UnityEngine.Random.Range(0, possibleSprites.Count)];
            spriteRenderer.sprite = selectedSpriteSO.sprite;
            animator.runtimeAnimatorController = selectedSpriteSO.animatorController;
        }
        else
        {
            Debug.LogError("No matching sprite found for character race and class.");
        }
    }
}
