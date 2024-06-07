using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterTownAI : MonoBehaviour
{
    public CharacterProfile character;
    private Dictionary<string, UtilityAI> utilities;

    [Header("Utility Variables")]
    [SerializeField] private float stress;
    [SerializeField] private float comfort;
    [SerializeField] private float loneliness;
    [SerializeField] private float fatigue;
    [SerializeField] private bool isHurt;
    [SerializeField] private float mood;
    [SerializeField] public int explorationPotential;

    [Header("Utility Variable Modifiers")]
    [SerializeField] private float stressModifier;
    [SerializeField] private float comfortModifier;
    [SerializeField] private float lonelinessModifier;
    [SerializeField] private float fatigueModifier;
    [SerializeField] private float moodModifier;


    private enum State
    {
        Idle,
        Walking,
        Interacting
    }

    private State state;

    // Update is called once per frame
    void Start()
    {
        character = GetComponent<CharacterProfile>();
        utilities = new Dictionary<string, UtilityAI>
        {
            { "Creativity", new UtilityAI(0, 0) },
            { "Exploration", new UtilityAI(0, 0) },
            { "Helping", new UtilityAI(0, 0) },
            { "Relaxation", new UtilityAI(0, 0) },
            { "Rest", new UtilityAI(0, 0) },
            { "Shopping", new UtilityAI(0, 0) },
            { "Training", new UtilityAI(0, 0) },
            { "Socialization", new UtilityAI(0, 0) },
            { "Spirituality", new UtilityAI(0, 0) },
            { "Entertainment", new UtilityAI(0, 0) },
            { "Studying", new UtilityAI(0, 0) },
            { "Villainy", new UtilityAI(0, 0) }
        };

        stress = 0;
        comfort = 0;
        loneliness = 0;
        fatigue = 0;
        isHurt = false;
        mood = 0;
        explorationPotential = 20;

        stressModifier = 0;
        comfortModifier = 0;
        lonelinessModifier = 0;
        fatigueModifier = 0;
        moodModifier = 0;
    }

    void Update()
    {
        utilities["Creativity"] = CalculateCreativityUtility();
        utilities["Exploration"] = CalculateExplorationUtility();
        utilities["Helping"] = CalculateHelpingUtility();
        utilities["Relaxation"] = CalculateRelaxationUtility();
        utilities["Rest"] = CalculateRestUtility();
        utilities["Shopping"] = CalculateShoppingUtility();
        utilities["Training"] = CalculateTrainingUtility();
        utilities["Socialization"] = CalculateSocializationUtility();
        utilities["Spirituality"] = CalculateSpiritualityUtility();
        utilities["Entertainment"] = CalculateEntertainmentUtility();
        utilities["Studying"] = CalculateStudyingUtility();
        utilities["Villainy"] = CalculateVillainyUtility();

        CalculateStress();
    }


    private void Idle()
    {
        // Implement Idle behavior here
    }

    private void Walk()
    {
        // Implement Walking behavior here
    }

    private void Interact()
    {
        // Implement Interacting behavior here
    }

    private void CalculateStress()
    {
        float stress = fatigue * 0.4f - mood * 0.1f + loneliness * 0.1f;
        stress += stressModifier;
        // Use the calculated stress value for further processing
    }

    private void CalculateComfort()
    {
        float comfort = 30f + mood * 0.1f - stress * 0.3f;
        comfort += comfortModifier;
        // Use the calculated comfort value for further processing
    }

    private void CalculateLoneliness()
    {
        float loneliness = ((float)character.Sociability * 0.2f) - (mood * 0.1f) + (UnityEngine.Random.Range(0f, 15f));
        loneliness += lonelinessModifier;
        // Use the calculated loneliness value for further processing
    }

    private void CalculateFatigue()
    {
        float fatigue = ((float)character.Constitution * 0.3f) - (mood * 0.1f) + (UnityEngine.Random.Range(0f, 15f));
        fatigue += fatigueModifier;
        fatigue += isHurt? 50 : 0;
        // Use the calculated fatigue value for further processing
    }

    private void CalculateMood()
    {
        float mood = ((float)character.Morale * 0.3f) + (UnityEngine.Random.Range(0f, 15f));
        mood += moodModifier;
        // Use the calculated mood value for further processing
    }

    private UtilityAI CalculateCreativityUtility()
    {
        float desire = ((float)character.Creativity * 0.3f) + ((float)character.Curiosity * 0.1f) - (stress * 0.1f);
        float need = ((float)character.Creativity * 0.3f) + ((float)character.Patience * 0.1f) - ((float)character.Morale * 0.1f);
        UtilityAI creativityUtility = new UtilityAI(desire, need);
        creativityUtility.CalculateUtilityValue();
        return creativityUtility;
    }

    private UtilityAI CalculateExplorationUtility()
    {
        // Replace these with your actual calculations
        float desire = ((float)character.Curiosity * 0.3f) + ((float)character.Perception * 0.1f) + (explorationPotential * 0.3f);
        float need = 0;
        UtilityAI explorationUtility = new UtilityAI(desire, need);
        explorationUtility.CalculateUtilityValue();
        return explorationUtility;
    }

    private UtilityAI CalculateHelpingUtility()
    {
        // Replace these with your actual calculations
        float desire = 0;
        float need = 0;
        UtilityAI helpingUtility = new UtilityAI(desire, need);
        helpingUtility.CalculateUtilityValue();
        return helpingUtility;
    }

    private UtilityAI CalculateRelaxationUtility()
    {
        // Replace these with your actual calculations
        float desire = 0;
        float need = 0;
        UtilityAI relaxationUtility = new UtilityAI(desire, need);
        relaxationUtility.CalculateUtilityValue();
        return relaxationUtility;
    }

    private UtilityAI CalculateRestUtility()
    {
        // Replace these with your actual calculations
        float desire = 0;
        float need = 0;
        UtilityAI restUtility = new UtilityAI(desire, need);
        restUtility.CalculateUtilityValue();
        return restUtility;
    }

    private UtilityAI CalculateShoppingUtility()
    {
        // Replace these with your actual calculations
        float desire = 0;
        float need = 0;
        UtilityAI shoppingUtility = new UtilityAI(desire, need);
        shoppingUtility.CalculateUtilityValue();
        return shoppingUtility;
    }

    private UtilityAI CalculateTrainingUtility()
    {
        // Replace these with your actual calculations
        float desire = 0;
        float need = 0;
        UtilityAI trainingUtility = new UtilityAI(desire, need);
        trainingUtility.CalculateUtilityValue();
        return trainingUtility;
    }

    private UtilityAI CalculateSocializationUtility()
    {
        // Replace these with your actual calculations
        float desire = 0;
        float need = 0;
        UtilityAI socializationUtility = new UtilityAI(desire, need);
        socializationUtility.CalculateUtilityValue();
        return socializationUtility;
    }

    private UtilityAI CalculateSpiritualityUtility()
    {
        // Replace these with your actual calculations
        float desire = 0;
        float need = 0;
        UtilityAI spiritualityUtility = new UtilityAI(desire, need);
        spiritualityUtility.CalculateUtilityValue();
        return spiritualityUtility;
    }

    private UtilityAI CalculateEntertainmentUtility()
    {
        // Replace these with your actual calculations
        float desire = 0;
        float need = 0;
        UtilityAI entertainmentUtility = new UtilityAI(desire, need);
        entertainmentUtility.CalculateUtilityValue();
        return entertainmentUtility;
    }

    private UtilityAI CalculateStudyingUtility()
    {
        // Replace these with your actual calculations
        float desire = 0;
        float need = 0;
        UtilityAI studyingUtility = new UtilityAI(desire, need);
        studyingUtility.CalculateUtilityValue();
        return studyingUtility;
    }

    private UtilityAI CalculateVillainyUtility()
    {
        // Replace these with your actual calculations
        float desire = 0;
        float need = 0;
        UtilityAI villainyUtility = new UtilityAI(desire, need);
        villainyUtility.CalculateUtilityValue();
        return villainyUtility;
    }



}
