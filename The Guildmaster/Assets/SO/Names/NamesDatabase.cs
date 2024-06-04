using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NamesDatabase", menuName = "Characters/NamesDatabase")]
public class NamesDatabase : ScriptableObject
{
    
    public enum Gender
    {
        Male,
        Female,
        NonBinary
    }

    
    [System.Serializable]
    public class FirstNameEntry
    {
        public string name;
        public Gender gender;
        public List<RaceSO> races;
    }

    [System.Serializable]
    public class LastNameEntry
    {
        public string name;
        public List<RaceSO> races;
    }

    public List<FirstNameEntry> firstNames;
    public List<LastNameEntry> lastNames;
}