using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSpriteSelector : MonoBehaviour
{

    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private NPCProfile npcProfile;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        npcProfile = GetComponent<NPCProfile>();
        SelectSprite();       
    }

    public void SelectSprite()
    {
        List<NPCSpriteSO> possibleSprites = SpriteManager.Instance.npcSprites.FindAll(
            spriteSO => spriteSO.races.Contains(npcProfile.npcRace) && spriteSO.occupation == npcProfile.Occupation
        );

        if (possibleSprites.Count > 0)
        {
            NPCSpriteSO selectedSpriteSO = possibleSprites[UnityEngine.Random.Range(0, possibleSprites.Count)];
            spriteRenderer.sprite = selectedSpriteSO.sprite;
            animator.runtimeAnimatorController = selectedSpriteSO.animatorController;
        }
        else
        {
            Debug.LogError("No matching sprite found for NPC race and occupation.");
        }
    }
}
