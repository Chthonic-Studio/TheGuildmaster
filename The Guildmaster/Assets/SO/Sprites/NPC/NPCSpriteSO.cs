using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NPCSprite", menuName = "NPC/Sprite")]
public class NPCSpriteSO : ScriptableObject
{
    public Sprite sprite;
    public RuntimeAnimatorController animatorController;
    public List<RaceSO> races;
    public NPCProfile.occupationType occupation;
}

