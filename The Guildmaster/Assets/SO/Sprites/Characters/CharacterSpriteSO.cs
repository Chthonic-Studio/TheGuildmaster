using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterSprite", menuName = "Characters/Sprite")]
public class CharacterSpriteSO : ScriptableObject
{
    public Sprite sprite;
    public RuntimeAnimatorController animatorController;
    public List<RaceSO> races;
    public List<ClassSO> classes;
}
