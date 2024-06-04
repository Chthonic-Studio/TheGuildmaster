using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour

{
    //Names database.
    [SerializeField] public NamesDatabase namesDatabase;

    //Character prefab
    [SerializeField] public GameObject characterPrefab;

    //All possible races
    [SerializeField] public List<RaceSO> allRaces;

    //All possible classes
    [SerializeField] public List<ClassSO> allClasses;

    //All possible traits
    [SerializeField] public List<TraitSO> allTraits;

    // Start is called before the first frame update
    private void Start()
    {
        NameGenerator.Initialize(namesDatabase);
        Debug.Log($"Number of traits: {allTraits.Count}");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnCharacter()
    {
        CharacterSpawner.SpawnCharacter(this, namesDatabase);
    }
}
