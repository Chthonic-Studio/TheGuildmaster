using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class characterTownAI : MonoBehaviour
{
    public CharacterProfile character;
    private Dictionary<string, UtilityAI> utilities;

    private enum State
    {
        Deciding,
        Idle,
        Walking,
        LookingForAction,
        Interacting,
        OnAction,
        Resting
    }

    [SerializeField] public string isDoing;
    [SerializeField] private State state;
    private bool isActive = false;
    private bool isIdling = false;
    private Transform moveTarget;

    [Header("Pathfinding variables")]
    public AIDestinationSetter destinationSetter;
    public TownDoor door;
    
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

    [Header("QoL Modifiers")]
    [SerializeField] private float sleepEfficiency = 1.0f;
    [SerializeField] private float chanceToWakeUp = 0.0f; // Chance to wake up placeholder

    [Header("Debug Utility Values")]
    [SerializeField] private float creativityUtilityValue;
    [SerializeField] private float explorationUtilityValue;
    [SerializeField] private float helpingUtilityValue;
    [SerializeField] private float relaxationUtilityValue;
    [SerializeField] private float restUtilityValue;
    [SerializeField] private float shoppingUtilityValue;
    [SerializeField] private float trainingUtilityValue;
    [SerializeField] private float socializationUtilityValue;
    [SerializeField] private float spiritualityUtilityValue;
    [SerializeField] private float entertainmentUtilityValue;
    [SerializeField] private float studyingUtilityValue;
    [SerializeField] private float villainyUtilityValue;

    #region Action Modifiers
    
    // Creativity Modifiers
    private float composeMusicModifier;
    private float craftItemModifier;
    private float performModifier;
    private float sculptModifier;
    private float singModifier;
    private float poetryModifier;
    private float playwritingModifier;
    private float paintingModifier;
    private float dancingModifier;
    private float gardeningModifier;
    private float cookingModifier;
    // Exploration Modifiers
    private float visitLandmarkModifier;
    private float goOutOfTownModifier;
    private float treasureHuntModifier;
    private float localFestivalModifier;
    private float tombRaidingModifier;
    private float goExploringModifier;
    // Helping Modifiers
    private float assistHomelessModifier;
    private float organizeCharityModifier;
    private float helpTownsfolkModifier;
    private float volunteerWorkModifier;
    private float attendCouncilModifier;
    private float donateModifier;
    // Relaxation Modifiers
    private float bathhouseModifier;
    private float birdwatchingModifier;
    private float fishingModifier;
    private float meditateModifier;
    private float whorehouseModifier;
    private float sunbathingModifier;
    private float stargazingModifier;
    private float tavernRelaxationModifier;
    // Rest Modifiers
    private float sleepModifier;
    private float exhaustionModifier;
    // Shopping Modifiers
    private float goEatModifier;
    private float barterModifier;
    private float buyItemModifier;
    private float sellItemModifier;
    private float windowShoppingModifier;
    private float comissionItemModifier;
    // Training Modifiers
    private float sparModifier;
    private float attendWorkshopModifier;
    private float practiceSkillsModifier;
    private float liftWeightsModifier;
    private float runModifier;
    private float studyTacticsModifier;
    private float mentalTrainingModifier;
    // Socialization Modifiers
    private float goToGuildMeetingModifier;
    private float talkWithCollegueModifier;
    private float talkWithTownsfolkModifier;
    private float romanceModifier;
    private float makeFriendsModifier;
    private float attendPartyModifier;
    // Spirituality Modifiers
    private float goToChurchModifier;
    private float prayModifier;
    private float seekSpiritualGuidanceModifier;
    private float pilgrimageModifier;
    private float innerReflectionModifier;
    // Entertainment Modifiers
    private float watchShowModifier;
    private float goDrinkModifier;
    private float attendPerformanceModifier;
    private float goGamblingModifier;
    private float tavernEntertainmentModifier;
    // Studying Modifiers
    private float goToLibraryModifier;
    private float readSkillBookModifier;
    private float attendLectureModifier;
    private float studyHistoryModifier;
    // Villainy Modifiers
    private float pickpocketModifier;
    private float stealModifier;
    private float spyModifier;
    private float gangWorkModifier;
    private float murderModifier;
    
    #endregion

    #region Action Script References
    // Creativity Scripts //
    private ComposeMusic composeMusic;
    private CraftItem craftItem;
    private Perform perform;
    private Sculpt sculpt;
    private Sing sing;
    private Poetry poetry;
    private Playwriting playwriting;
    private Painting painting;
    private Dancing dancing;
    private Gardening gardening;
    private Cooking cooking;
    // Exploration Scripts //
    private VisitLandmark visitLandmark;
    private GoOutOfTown goOutOfTown;
    private TreasureHunt treasureHunt;
    private LocalFestival localFestival;
    private TombRaiding tombRaiding;
    private GoExploring goExploring;
    // Helping Scripts //
    private AssistHomeless assistHomeless;
    private OrganizeCharity organizeCharity;
    private HelpTownsfolk helpTownsfolk;
    private VolunteerWork volunteerWork;
    private AttendCouncil attendCouncil;
    private Donate donate;
    // Relaxation Scripts //
    private Bathhouse bathhouse;
    private Birdwatching birdwatching;
    private Fishing fishing;
    private Whoring whoring;
    private Meditate meditate;
    private Sunbathing sunbathing;
    private Stargazing stargazing;
    private TavernRelaxation tavernRelaxation;
    // Rest Scripts //
    private Sleep sleep;
    private Exhaustion exhaustion;
    // Shopping Scripts //
    private GoEat goEat;
    private Barter barter;
    private BuyItem buyItem;
    private SellItem sellItem;
    private WindowShopping windowShopping;
    private ComissionItem comissionItem;
    // Training Scripts //
    private Spar spar;
    private AttendWorkshop attendWorkshop;
    private PracticeSkills practiceSkills;
    private LiftWeights liftWeights;
    private Run run;
    private StudyTactics studyTactics;
    private MentalTraining mentalTraining;
    // Socialization Scripts //
    private GoToGuildMeeting goToGuildMeeting;
    private TalkWithCollegue talkWithCollegue;
    private TalkWithTownsfolk talkWithTownsfolk;
    private Romance romance;
    private MakeFriends makeFriends;
    private AttendParty attendParty;
    // Spirituality Scripts //
    private GoToChurch goToChurch;
    private Pray pray;
    private SeekSpiritualGuidance seekSpiritualGuidance;
    private Pilgrimage pilgrimage;
    private InnerReflection innerReflection;
    // Entertainment Scripts //
    private WatchShow watchShow;
    private GoDrink goDrink;
    private AttendPerformance attendPerformance;
    private GoGambling goGambling;
    private TavernEntertainment tavernEntertainment;
    // Studying Scripts //
    private GoToLibrary goToLibrary;
    private ReadSkillBook readSkillBook;
    private AttendLecture attendLecture;
    private StudyHistory studyHistory;
    // Villainy Scripts //
    private Pickpocket pickpocket;
    private Steal steal;
    private Spy spy;
    private GangWork gangWork;
    private Murder murder;
    

    #endregion

    void Awake()
    {
        moveTarget = transform.Find("MoveTarget");
    }
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

        isActive = false;
        
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

        #region Action Modifiers Initialization
        // Creativity Modifiers
        composeMusicModifier = 0;
        craftItemModifier = 0;
        performModifier = 0;
        sculptModifier = 0;
        singModifier = 0;
        poetryModifier = 0;
        playwritingModifier = 0;
        paintingModifier = 0;
        dancingModifier = 0;
        gardeningModifier = 0;
        cookingModifier = 0;
        // Exploration Modifiers
        visitLandmarkModifier = 0;
        goOutOfTownModifier = 0;
        treasureHuntModifier = 0;
        localFestivalModifier = 0;
        tombRaidingModifier = 0;
        goExploringModifier = 0;
        // Helping Modifiers
        assistHomelessModifier = 0;
        organizeCharityModifier = 0;
        helpTownsfolkModifier = 0;
        volunteerWorkModifier = 0;
        attendCouncilModifier = 0;
        donateModifier = 0;
        // Relaxation Modifiers
        bathhouseModifier = 0;
        birdwatchingModifier = 0;
        fishingModifier = 0;
        meditateModifier = 0;
        whorehouseModifier = 0;
        sunbathingModifier = 0;
        stargazingModifier = 0;
        tavernRelaxationModifier = 0;
        // Rest Modifiers
        sleepModifier = 0;
        exhaustionModifier = 0;
        // Shopping Modifiers
        goEatModifier = 0;
        barterModifier = 0;
        buyItemModifier = 0;
        sellItemModifier = 0;
        windowShoppingModifier = 0;
        comissionItemModifier = 0;
        // Training Modifiers
        sparModifier = 0;
        attendWorkshopModifier = 0;
        practiceSkillsModifier = 0;
        liftWeightsModifier = 0;
        runModifier = 0;
        studyTacticsModifier = 0;
        mentalTrainingModifier = 0;
        // Socialization Modifiers
        goToGuildMeetingModifier = 0;
        talkWithCollegueModifier = 0;
        talkWithTownsfolkModifier = 0;
        romanceModifier = 0;
        makeFriendsModifier = 0;
        attendPartyModifier = 0;
        // Spirituality Modifiers
        goToChurchModifier = 0;
        prayModifier = 0;
        seekSpiritualGuidanceModifier = 0;
        pilgrimageModifier = 0;
        innerReflectionModifier = 0;
        // Entertainment Modifiers
        watchShowModifier = 0;
        goDrinkModifier = 0;
        attendPerformanceModifier = 0;
        goGamblingModifier = 0;
        tavernEntertainmentModifier = 0;
        // Studying Modifiers
        goToLibraryModifier = 0;
        readSkillBookModifier = 0;
        attendLectureModifier = 0;
        studyHistoryModifier = 0;
        // Villainy Modifiers
        pickpocketModifier = 0;
        stealModifier = 0;
        spyModifier = 0;
        gangWorkModifier = 0;
        murderModifier = 0;

        #endregion


        #region Action Script Initialization

        // Creativity Scripts //
        composeMusic = new ComposeMusic(this, character);
        craftItem = new CraftItem(this, character);
        perform = new Perform(this, character);
        sculpt = new Sculpt(this, character);
        sing = new Sing(this, character);
        poetry = new Poetry(this, character);
        playwriting = new Playwriting(this, character);
        painting = new Painting(this, character);
        dancing = new Dancing(this, character);
        gardening = new Gardening(this, character);
        cooking = new Cooking(this, character);

        // Exploration Scripts //

        visitLandmark = new VisitLandmark(this, character);
        goOutOfTown = new GoOutOfTown(this, character);
        treasureHunt = new TreasureHunt(this, character);
        localFestival = new LocalFestival(this, character);
        tombRaiding = new TombRaiding(this, character);
        goExploring = new GoExploring(this, character);

        // Helping Scripts //

        assistHomeless = new AssistHomeless(this, character);
        organizeCharity = new OrganizeCharity(this, character);
        helpTownsfolk = new HelpTownsfolk(this, character);
        volunteerWork = new VolunteerWork(this, character);
        attendCouncil = new AttendCouncil(this, character);
        donate = new Donate(this, character);

        // Relaxation Scripts //

        bathhouse = new Bathhouse(this, character);
        birdwatching = new Birdwatching(this, character);
        whoring = new Whoring(this, character);
        fishing = new Fishing(this, character);
        meditate = new Meditate(this, character);
        sunbathing = new Sunbathing(this, character);
        stargazing = new Stargazing(this, character);
        tavernRelaxation = new TavernRelaxation(this, character);

        // Rest Scripts //

        sleep = new Sleep(this, character);
        exhaustion = new Exhaustion(this, character);

        // Shopping Scripts //

        goEat = new GoEat(this, character);
        barter = new Barter(this, character);
        buyItem = new BuyItem(this, character);
        sellItem = new SellItem(this, character);
        windowShopping = new WindowShopping(this, character);
        comissionItem = new ComissionItem(this, character);
        
        // Training Scripts //

        spar = new Spar(this, character);
        attendWorkshop = new AttendWorkshop(this, character);
        practiceSkills = new PracticeSkills(this, character);
        liftWeights = new LiftWeights(this, character);
        run = new Run(this, character);
        studyTactics = new StudyTactics(this, character);
        mentalTraining = new MentalTraining(this, character);

        // Socialization Scripts //

        goToGuildMeeting = new GoToGuildMeeting(this, character);
        talkWithCollegue = new TalkWithCollegue(this, character);
        talkWithTownsfolk = new TalkWithTownsfolk(this, character);
        romance = new Romance(this, character);
        makeFriends = new MakeFriends(this, character);
        attendParty = new AttendParty(this, character);

        // Spirituality Scripts //

        goToChurch = new GoToChurch(this, character);
        pray = new Pray(this, character);
        seekSpiritualGuidance = new SeekSpiritualGuidance(this, character);
        pilgrimage = new Pilgrimage(this, character);
        innerReflection = new InnerReflection(this, character);

        // Entertainment Scripts //

        watchShow = new WatchShow(this, character);
        goDrink = new GoDrink(this, character);
        attendPerformance = new AttendPerformance(this, character);
        goGambling = new GoGambling(this, character);
        tavernEntertainment = new TavernEntertainment(this, character);

        // Studying Scripts //

        goToLibrary = new GoToLibrary(this, character);
        readSkillBook = new ReadSkillBook(this, character);
        attendLecture = new AttendLecture(this, character);
        studyHistory = new StudyHistory(this, character);

        // Villainy Scripts //

        pickpocket = new Pickpocket(this, character);
        steal = new Steal(this, character);
        spy = new Spy(this, character);
        gangWork = new GangWork(this, character);
        murder = new Murder(this, character);

        #endregion

        //Set initial pathfinding variables

        destinationSetter = GetComponent<AIDestinationSetter>();
        door = character.house.GetComponentInChildren<TownDoor>();

        CalculateUtilityVariables();
        CalculateMainUtilityValues();
    }

    private bool isIncreasingFatigue = false; 
    private bool isCheckingState = false;

    void Update()
    {

        // Check for exhaustion
        if (state != State.Fainted && character.fatigue > 90)
        {
            StartCoroutine(exhaustion.FaintCoroutine());
        }

        // Existing Update logic...
        if (state != State.Resting && !isIncreasingFatigue)
        {
            StartCoroutine(IncreaseFatigue());
        }

        if (!isIdling && !isActive && !isCheckingState)
        {
            StartCoroutine(CheckStateCoroutine());
        }

    }

    private IEnumerator IncreaseFatigue()
    {
        isIncreasingFatigue = true;

        while (state != State.Resting)
        {
            fatigueModifier += 0.1f;
            yield return new WaitForSeconds(10f); 
        }

        isIncreasingFatigue = false;
        
        yield return null;
    }

    private IEnumerator CheckStateCoroutine()
    {
        isCheckingState = true;

        while (!isIdling && !isActive)
        {
            CheckState();
            yield return new WaitForSeconds(5f);
        }

        isCheckingState = false;
    }

    private void CheckState()
    {
        if (!isActive)
        {
            
            float actionOdds = 0.7f;

            float random = UnityEngine.Random.value;

            if (random <= actionOdds)        
            
            {
                StopIdle();

                state = State.LookingForAction;

                CalculateMainUtilityValues();
                float maxUtility = Mathf.Max(creativityUtilityValue, explorationUtilityValue, helpingUtilityValue, relaxationUtilityValue, restUtilityValue, shoppingUtilityValue, trainingUtilityValue, socializationUtilityValue, spiritualityUtilityValue, entertainmentUtilityValue, studyingUtilityValue, villainyUtilityValue);
                
                if (maxUtility == creativityUtilityValue)
                {
                    CreativityActionSelector();
                }
                else if (maxUtility == explorationUtilityValue)
                {
                    ExplorationActionSelector();
                }
                else if (maxUtility == helpingUtilityValue)
                {
                    HelpingActionSelector();
                }
                else if (maxUtility == relaxationUtilityValue)
                {
                    RelaxationActionSelector();
                }
                else if (maxUtility == restUtilityValue)
                {
                    RestActionSelector();
                }
                else if (maxUtility == shoppingUtilityValue)
                {
                    ShoppingActionSelector();
                }
                else if (maxUtility == trainingUtilityValue)
                {
                    TrainingActionSelector();
                }
                else if (maxUtility == socializationUtilityValue)
                {
                    SocializationActionSelector();
                }
                else if (maxUtility == spiritualityUtilityValue)
                {
                    SpiritualityActionSelector();
                }
                else if (maxUtility == entertainmentUtilityValue)
                {
                    EntertainmentActionSelector();
                }
                else if (maxUtility == studyingUtilityValue)
                {
                    StudyingActionSelector();
                }
                else if (maxUtility == villainyUtilityValue)
                {
                    VillainyActionSelector();
            }

            else
            {
                Idle();
            }
        }

        if (isActive)
        {
            state = State.Interacting;
            return;
        }

    }
    }

    private void CalculateMainUtilityValues()
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

    }



    private Coroutine idleRoutine;

    private void Idle()
    {
        isActive = true;
        state = State.Idle;
        isIdling = true;
        idleRoutine = StartCoroutine(IdleRoutine());
    }

    private IEnumerator IdleRoutine()
    {
        while (true)
        {
            Vector3 destination;
            do
            {
                var randomDirection = Random.insideUnitCircle.normalized * 10; 
                destination = new Vector3(transform.position.x + randomDirection.x, 
                    transform.position.y + randomDirection.y, transform.position.z);
            } 
            
            while (Physics.CheckSphere(destination, 0.1f)); // Keep generating new destinations until we find one that is not inside a collider

            // Use the moveTarget variable as the target
            GetComponent<AIDestinationSetter>().target = moveTarget;
            GetComponent<AIDestinationSetter>().target.position = destination;

            // Wait for the character to reach the destination
            yield return new WaitUntil(() => Vector3.Distance(transform.position, destination) < 0.1f);

            // Wait for a short random amount of time
            yield return new WaitForSeconds(Random.Range(3, 10)); 

            CheckState();
        }
    }
    public void StopIdle()
    {
        if (idleRoutine != null)
        {
            StopCoroutine(idleRoutine);
            idleRoutine = null;
            isActive = false;
            isIdling = false;
            state = State.Deciding;
        }

    }

    private void Walk()
    {
        // Implement Walking behavior here
    }

    private void Interact()
    {
        // Implement Interacting behavior here
    }

#region Utility Calculations

    private void CalculateStress()
    {
        stress = fatigue * 0.4f - mood * 0.1f + loneliness * 0.1f;
        stress += stressModifier;
    }

    private void CalculateComfort()
    {
        comfort = 30f + mood * 0.1f - stress * 0.3f;
        comfort += comfortModifier;
    }

    private void CalculateLoneliness()
    {
        loneliness = ((float)character.Sociability * 0.2f) - (mood * 0.1f) + (UnityEngine.Random.Range(0f, 10f));
        loneliness += lonelinessModifier;
    }

    private void CalculateFatigue()
    {
        fatigue = (UnityEngine.Random.Range(0f, 5f)) - ((float)character.Constitution * 0.3f);
        fatigue += fatigueModifier;
        fatigue += isHurt? 50 : 0;
    }

    private void CalculateMood()
    {
        mood = ((float)character.Morale * 0.3f) + (UnityEngine.Random.Range(0f, 10f));
        mood += moodModifier;
    }

    private void CalculateUtilityVariables()
    {
        CalculateMood();
        CalculateFatigue();
        CalculateLoneliness();
        CalculateStress();
        CalculateComfort();
    }

    private UtilityAI CalculateCreativityUtility()
    {
        CalculateUtilityVariables();
        float desire = ((float)character.Creativity * 0.3f) + ((float)character.Curiosity * 0.1f) - (stress * 0.1f);
        float need = ((float)character.Creativity * 0.3f) + ((float)character.Patience * 0.1f) - ((float)character.Morale * 0.1f);
        UtilityAI creativityUtility = new UtilityAI(desire, need);
        creativityUtility.CalculateUtilityValue();
        creativityUtilityValue = creativityUtility.UtilityValue;
        return creativityUtility;
    }

    private UtilityAI CalculateExplorationUtility()
    {
        CalculateUtilityVariables();
        float desire = ((float)character.Curiosity * 0.3f) + ((float)character.Perception * 0.1f) + (explorationPotential * 0.3f);
        float need = (desire * 0.3f) + (character.Resourcefulness * 0.1f) - (loneliness * 0.2f);
        UtilityAI explorationUtility = new UtilityAI(desire, need);
        explorationUtility.CalculateUtilityValue();
        explorationUtilityValue = explorationUtility.UtilityValue;
        return explorationUtility;
    }

    private UtilityAI CalculateHelpingUtility()
    {
        CalculateUtilityVariables();
        float desire = ((float)character.Good * 0.5f) + ((float)character.Empathy * 0.3f) + ((float)character.Leadership * 0.1f) - (stress * 0.3f) - ((float)character.Cunning * 0.2f) + ((float)character.Diplomacy * 0.1f);
        float need = (loneliness * 0.1f) + ((float)character.Sociability * 0.1f) + ((float)character.Patience * 0.1f) + (desire * 0.3f) - ((float)character.Evil * 0.2f);
        UtilityAI helpingUtility = new UtilityAI(desire, need);
        helpingUtility.CalculateUtilityValue();
        helpingUtilityValue = helpingUtility.UtilityValue;
        return helpingUtility;
    }

    private UtilityAI CalculateRelaxationUtility()
    {
        CalculateUtilityVariables();
        float desire = (stress * 0.8f) + (fatigue * 0.6f) - (comfort * 0.2f) - (mood * 0.1f) - ((float)character.Morale * 0.1f) - ((float)character.Discipline * 0.2f);
        float need = (stress * 0.8f) + (fatigue * 0.3f) - ((float)character.Ambition * 0.2f) - ((float)character.Tenacity * 0.2f);
        UtilityAI relaxationUtility = new UtilityAI(desire, need);
        relaxationUtility.CalculateUtilityValue();
        relaxationUtilityValue = relaxationUtility.UtilityValue;
        return relaxationUtility;
    }

    private UtilityAI CalculateRestUtility()
    {
        CalculateUtilityVariables();
        float desire = (fatigue * 0.5f) + (stress * 0.2f) - ((float)character.Ambition * 0.1f);
        float need = (fatigue * 0.8f) - (comfort * 0.2f);
        UtilityAI restUtility = new UtilityAI(desire, need);
        restUtility.CalculateUtilityValue();
        restUtilityValue = restUtility.UtilityValue;
        return restUtility;
    }

    private UtilityAI CalculateShoppingUtility()
    {
        CalculateUtilityVariables();
        float desire = ((float)character.gold * 0.1f) + ((float)character.Ambition * 0.2f) + ((float)character.Curiosity * 0.1f);
        float need = (desire * 0.3f) + ((float)character.Resourcefulness * 0.1f) - (stress * 0.2f);
        UtilityAI shoppingUtility = new UtilityAI(desire, need);
        shoppingUtility.CalculateUtilityValue();
        shoppingUtilityValue = shoppingUtility.UtilityValue;
        return shoppingUtility;
    }

    private UtilityAI CalculateTrainingUtility()
    {
        CalculateUtilityVariables();
        float desire = ((float)character.Ambition * 0.3f) + ((float)character.Willpower * 0.2f) + ((float)character.Discipline * 0.2f);
        desire -= isHurt? 50 : 0;
        float need = ((float)character.Ambition * 0.5f) + (float)character.Tenacity * 0.4f - (fatigue * 0.7f);
        UtilityAI trainingUtility = new UtilityAI(desire, need);
        trainingUtility.CalculateUtilityValue();
        trainingUtilityValue = trainingUtility.UtilityValue;
        return trainingUtility;
    }

    private UtilityAI CalculateSocializationUtility()
    {
        CalculateUtilityVariables();
        float desire = ((float)character.Sociability * 0.3f) + ((float)character.Charisma * 0.15f) + ((float)character.Confidence * 0.3f);
        float need = (loneliness * 0.3f) * ((float)character.Sociability * 0.3f);
        UtilityAI socializationUtility = new UtilityAI(desire, need);
        socializationUtility.CalculateUtilityValue();
        socializationUtilityValue = socializationUtility.UtilityValue;
        return socializationUtility;
    }

    private UtilityAI CalculateSpiritualityUtility()
    {
        CalculateUtilityVariables();
        float desire = ((float)character.Discipline * 0.1f) + ((float)character.Humility * 0.2f) + ((float)character.Willpower * 0.1f) + ((float)character.Patience * 0.1f);
        float need = (desire * 0.3f) + ((float)character.Empathy * 0.2f) + ((float)character.Curiosity * 0.1f) - ((float)character.Morale * 0.3f);
        UtilityAI spiritualityUtility = new UtilityAI(desire, need);
        spiritualityUtility.CalculateUtilityValue();
        spiritualityUtilityValue = spiritualityUtility.UtilityValue;
        return spiritualityUtility;
    }

    private UtilityAI CalculateEntertainmentUtility()
    {
        CalculateUtilityVariables();
        float desire = ((float)character.Humor * 0.3f) + ((float)character.Curiosity * 0.1f) + ((float)character.Sociability * 0.1f);
        float need = (stress * 0.3f) + ((float)character.Humor * 0.2f);
        UtilityAI entertainmentUtility = new UtilityAI(desire, need);
        entertainmentUtility.CalculateUtilityValue();
        entertainmentUtilityValue = entertainmentUtility.UtilityValue;
        return entertainmentUtility;
    }

    private UtilityAI CalculateStudyingUtility()
    {
        CalculateUtilityVariables();
        float desire = ((float)character.Resourcefulness * 0.3f) + ((float)character.Adaptability * 0.1f) + ((float)character.Curiosity * 0.1f) + ((float)character.Patience * 0.1f) - (stress * 0.2f);
        float need = ((float)character.Intelligence * 0.4f) + ((float)character.Wisdom * 0.2f) + ((float)character.Willpower * 0.2f);
        UtilityAI studyingUtility = new UtilityAI(desire, need);
        studyingUtility.CalculateUtilityValue();
        studyingUtilityValue = studyingUtility.UtilityValue;
        return studyingUtility;
    }

    private UtilityAI CalculateVillainyUtility()
    {
        CalculateUtilityVariables();
        float desire = ((float)character.Evil * 0.3f) + ((float)character.Cunning * 0.1f) + ((float)character.Aggression * 0.1f) - ((float)character.Good * 0.2f);
        float need = (desire * 0.3f) + ((float)character.Intimidation * 0.15f) + ((float)character.Deception * 0.15f);
        UtilityAI villainyUtility = new UtilityAI(desire, need);
        villainyUtility.CalculateUtilityValue();
        villainyUtilityValue = villainyUtility.UtilityValue;
        return villainyUtility;
    }

#endregion

#region Utility Actions

    #region Creativity Action

        private void CreativityActionSelector()
        {
            float composeMusicUtility = ComposeMusicUtility();
            float craftItemUtility = CraftItemUtility();
            float performUtility = PerformUtility();
            float sculptUtility = SculptUtility();
            float singUtility = SingUtility();
            float poetryUtility = PoetryUtility();
            float playwritingUtility = PlaywritingUtility();
            float paintingUtility = PaintingUtility();
            float dancingUtility = DancingUtility();
            float gardeningUtility = GardeningUtility();
            float cookingUtility = CookingUtility();

            float maxUtility = Mathf.Max(composeMusicUtility, craftItemUtility, performUtility, sculptUtility, singUtility, poetryUtility, playwritingUtility, paintingUtility, dancingUtility, gardeningUtility, cookingUtility);
        
            if (maxUtility == composeMusicUtility)
            {
                ComposeMusicAction();
            }
            else if (maxUtility == craftItemUtility)
            {
                CraftItemAction();
            }
            else if (maxUtility == performUtility)
            {
                PerformAction();
            }
            else if (maxUtility == sculptUtility)
            {
                SculptAction();
            }
            else if (maxUtility == singUtility)
            {
                SingAction();
            }
            else if (maxUtility == poetryUtility)
            {
                PoetryAction();
            }
            else if (maxUtility == playwritingUtility)
            {
                PlaywritingAction();
            }
            else if (maxUtility == paintingUtility)
            {
                PaintingAction();
            }
            else if (maxUtility == dancingUtility)
            {
                DancingAction();
            }
            else if (maxUtility == gardeningUtility)
            {
                GardeningAction();
            }
            else if (maxUtility == cookingUtility)
            {
                CookingAction();
            }
        }
        ////////////////////////////////////
        ////// Action Utilities ////////////
        ////////////////////////////////////
        private float ComposeMusicUtility()
        {
            float utility = UnityEngine.Random.Range(0f, 15f) + ((float)character.Perception * 0.5f) + ((float)character.Intelligence * 0.1f) + (mood * 0.1f) + (comfort * 0.5f) - (stress * 0.5f);       
            utility += composeMusicModifier;
            return utility;
        }

        private float CraftItemUtility()
        {
            float utility = UnityEngine.Random.Range(0f, 15f) + ((float)character.Intelligence * 0.5f) + ((float)character.Adaptability * 0.1f) + ((float)character.Tenacity * 0.1f) - (fatigue * 0.5f);
            utility += craftItemModifier;
            return utility;
        }

        private float PerformUtility()
        {
            float utility = UnityEngine.Random.Range(0f, 15f) + ((float)character.Confidence * 0.2f) + ((float)character.Charisma * 0.2f) + ((float)character.Sociability * 0.15f) + (mood * 0.1f) - (loneliness * 0.1f) - (stress * 0.5f);
            utility += performModifier;
            return utility;        
        }

        private float SculptUtility()
        {
            float utility = UnityEngine.Random.Range(0f, 15f) + ((float)character.Perception * 0.5f) + ((float)character.Intelligence * 0.1f) + ((float)character.Agility * 0.15f) + ((float)character.Patience * 0.1f) - (fatigue * 0.5f);
            utility += sculptModifier;
            return utility;
        }

        private float SingUtility()
        {
            float utility = UnityEngine.Random.Range(0f, 15f) + ((float)character.Confidence * 0.5f) + ((float)character.Charisma * 0.1f) + (mood * 0.1f) - (loneliness * 0.15f) - (stress * 0.5f);
            utility += singModifier;
            return utility;
        }

        private float PoetryUtility()
        {
            float utility = UnityEngine.Random.Range(0f, 15f) + ((float)character.Intelligence * 0.5f) + ((float)character.Perception * 0.1f) + (mood * 0.1f) + (loneliness * 0.15f) - (stress * 0.5f);
            utility += poetryModifier;
            return utility;        
        }

        private float PlaywritingUtility()
        {
            float utility = UnityEngine.Random.Range(0f, 15f) + ((float)character.Intelligence * 0.5f) + ((float)character.Perception * 0.5f) + ((float)character.Wisdom * 0.1f) + (mood * 0.15f) - (stress * 0.5f);
            utility += playwritingModifier;
            return utility;
        }

        private float PaintingUtility()
        {
            float utility = UnityEngine.Random.Range(0f, 15f) + ((float)character.Perception * 0.5f) + ((float)character.Patience * 0.2f) + (mood * 0.1f) - (stress * 0.5f);
            utility += paintingModifier;
            return utility;
        }

        private float DancingUtility()
        {
            float utility = UnityEngine.Random.Range(0f, 15f) + ((float)character.Confidence * 0.5f) + ((float)character.Charisma * 0.1f) + ((float)character.Agility * 0.1f) + (mood * 0.1f) - (fatigue * 0.15f) - (stress * 0.5f);
            utility += dancingModifier;
            return utility;
        }

        private float GardeningUtility()
        {
            float utility = UnityEngine.Random.Range(0f, 15f) + ((float)character.Patience * 0.2f) + ((float)character.Perception * 0.15f) + ((float)character.Adaptability * 0.1f) + ((float)character.Tenacity * 0.1f) + (mood * 0.1f) - (stress * 0.5f);
            utility += gardeningModifier;
            return utility;
        }

        private float CookingUtility()
        {
            float utility = UnityEngine.Random.Range(0f, 15f) + ((float)character.Intelligence * 0.1f) + ((float)character.Patience * 0.15f) + (mood * 0.1f) - (fatigue * 0.5f);
            utility += cookingModifier;
            return utility;
        }

        ////////////////////////////////////
        ////// Action Methods //////////////
        ////////////////////////////////////

        private void ComposeMusicAction()
        {
            isActive = true;
            isDoing = "Compose Music";

            ComposeMusic composeMusic = new ComposeMusic(this, character);
        }

        private void CraftItemAction()
        {
            isActive = true;
            isDoing = "Crafting";

            CraftItem craftItem = new CraftItem(this, character);
        }

        private void PerformAction()
        {
            isActive = true;
            isDoing = "Performing";

            Perform perform = new Perform(this, character);
        }

        private void SculptAction()
        {
            isActive = true;
            isDoing = "Sculpting";

            Sculpt sculpt = new Sculpt(this, character);
        }

        private void SingAction()
        {
            isActive = true;
            isDoing = "Singing";

            Sing sing = new Sing(this, character);
        }

        private void PoetryAction()
        {
            isActive = true;
            isDoing = "Poetry";

            Poetry poetry = new Poetry(this, character);
        }

        private void PlaywritingAction()
        {
            isActive = true;
            isDoing = "Playwriting";

            Playwriting playwriting = new Playwriting(this, character);
        }

        private void PaintingAction()
        {
            isActive = true;   
            isDoing = "Painting";

            Painting painting = new Painting(this, character);
        }

        private void DancingAction()
        {
            isActive = true;
            isDoing = "Dancing";

            Dancing dancing = new Dancing(this, character);
        }

        private void GardeningAction()
        {
            isActive = true;
            isDoing = "Gardening";

            Gardening gardening = new Gardening(this, character);
        }

        private void CookingAction()
        {
            isActive = true;
            isDoing = "Cooking";

            Cooking cooking = new Cooking (this, character);
        }

    #endregion

    #region Exploration Action

        private void ExplorationActionSelector()
        {
            float visitLandmarkUtility = VisitLandmarkUtility();
            float goOutOfTownUtility = GoOutOfTownUtility();
            float treasureHuntUtility = TreasureHuntUtility();
            float localFestivalUtility = LocalFestivalUtility();
            float tombRaidingUtility = TombRaidingUtility();
            float goExploringUtility = GoExploringUtility();

            float maxUtility = Mathf.Max(visitLandmarkUtility, goOutOfTownUtility, treasureHuntUtility, localFestivalUtility, tombRaidingUtility, goExploringUtility);
        
            if (maxUtility == visitLandmarkUtility)
            {
                VisitLandmarkAction();
            }
            else if (maxUtility == goOutOfTownUtility)
            {
                GoOutOfTownAction();
            }
            else if (maxUtility == treasureHuntUtility)
            {
                TreasureHuntAction();
            }
            else if (maxUtility == localFestivalUtility)
            {
                LocalFestivalAction();
            }
            else if (maxUtility == tombRaidingUtility)
            {
                TombRaidingAction();
            }
            else if (maxUtility == goExploringUtility)
            {
                GoExploringAction();
            }
        }
        ////////////////////////////////////
        ////// Action Utilities ////////////
        ////////////////////////////////////
        private float VisitLandmarkUtility()
        {
            float utility = UnityEngine.Random.Range(0f, 15f) + ((float)character.Perception * 0.3f) + ((float)character.Intelligence * 0.1f) + (mood * 0.1f) + (comfort * 0.5f) - (stress * 0.5f);       
            utility += visitLandmarkModifier;
            return utility;
        }

        private float GoOutOfTownUtility()
        {
            float utility = UnityEngine.Random.Range(0f, 15f) + ((float)character.Intelligence * 0.5f) + ((float)character.Adaptability * 0.1f) + ((float)character.Tenacity * 0.1f) - (fatigue * 0.5f);
            utility += goOutOfTownModifier;
            return utility;
        }

        private float TreasureHuntUtility()
        {
            float utility = UnityEngine.Random.Range(0f, 15f) + ((float)character.Confidence * 0.4f) + ((float)character.Ambition * 0.2f) + ((float)character.Sociability * 0.15f) + (mood * 0.1f) - (loneliness * 0.1f) - (stress * 0.5f);
            utility += treasureHuntModifier;
            return utility;
        }  

        private float LocalFestivalUtility()
        {
            float utility = UnityEngine.Random.Range(0f, 15f) + ((float)character.Perception * 0.5f) + ((float)character.Curiosity * 0.1f) + ((float)character.Sociability * 0.15f) + ((float)character.Patience * 0.1f) - (fatigue * 0.5f);
            utility += localFestivalModifier;
            return utility;
        }

        private float TombRaidingUtility()
        {
            float utility = UnityEngine.Random.Range(0f, 15f) + ((float)character.Confidence * 0.5f) + ((float)character.Ambition * 0.2f) - (loneliness * 0.15f) - (stress * 0.5f);
            utility += tombRaidingModifier;
            return utility;
        }

        private float GoExploringUtility()
        {
            float utility = UnityEngine.Random.Range(0f, 15f) + ((float)character.Curiosity * 0.5f) + ((float)character.Perception * 0.1f) + (mood * 0.1f) + (loneliness * 0.15f) - (stress * 0.5f);
            utility += goExploringModifier;
            return utility;
        }

        ////////////////////////////////////
        ////// Action Methods //////////////
        ////////////////////////////////////

        private void VisitLandmarkAction()
        {
            isActive = true;
            isDoing = "Visiting Landmark";

            VisitLandmark visitLandmark = new VisitLandmark(this, character);
        }

        private void GoOutOfTownAction()
        {
            isActive = true;
            isDoing = "Going Out of Town";

            GoOutOfTown goOutOfTown = new GoOutOfTown(this, character);
        }

        private void TreasureHuntAction()
        {
            isActive = true;
            isDoing = "Treasure Hunting";

            TreasureHunt treasureHunt = new TreasureHunt(this, character);
        }

        private void LocalFestivalAction()
        {
            isActive = true;
            isDoing = "Attending Local Festival";

            LocalFestival localFestival = new LocalFestival(this, character);
        }

        private void TombRaidingAction()
        {
            isActive = true;
            isDoing = "Tomb Raiding";

            TombRaiding tombRaiding = new TombRaiding(this, character);
        }

        private void GoExploringAction()
        {
            isActive = true;
            isDoing = "Exploring";

            GoExploring goExploring = new GoExploring(this, character);
        }

    #endregion

    #region Helping Action

        private void HelpingActionSelector()
        {
            float assistHomelessUtility = AssistHomelessUtility();
            float organizeCharityUtility = OrganizeCharityUtility();
            float helpTownsfolkUtility = HelpTownsfolkUtility();
            float volunteerWorkUtility = VolunteerWorkUtility();
            float attendCouncilUtility = AttendCouncilUtility();
            float donateUtility = DonateUtility();

            float maxUtility = Mathf.Max(assistHomelessUtility, organizeCharityUtility, helpTownsfolkUtility, volunteerWorkUtility, attendCouncilUtility, donateUtility);
        
            if (maxUtility == assistHomelessUtility)
            {
                AssistHomelessAction();
            }
            else if (maxUtility == organizeCharityUtility)
            {
                OrganizeCharityAction();
            }
            else if (maxUtility == helpTownsfolkUtility)
            {
                HelpTownsfolkAction();
            }
            else if (maxUtility == volunteerWorkUtility)
            {
                VolunteerWorkAction();
            }
            else if (maxUtility == attendCouncilUtility)
            {
                AttendCouncilAction();
            }
            else if (maxUtility == donateUtility)
            {
                DonateAction();
            }
        }
        ////////////////////////////////////
        ////// Action Utilities ////////////
        ////////////////////////////////////
        private float AssistHomelessUtility()
        {
            float utility = UnityEngine.Random.Range(0f, 15f) + ((float)character.Empathy * 0.5f) + ((float)character.Good * 0.2f) + ((float)character.Sociability * 0.15f) + (loneliness * 0.3f) - (stress * 0.5f);
            utility += assistHomelessModifier;
            return utility;
        }

        private float OrganizeCharityUtility()
        {
            float utility = UnityEngine.Random.Range(0f, 15f) + ((float)character.Leadership * 0.5f) + ((float)character.Good * 0.2f) + ((float)character.Sociability * 0.15f) - (stress * 0.5f) - (fatigue * 0.3f);
            utility += organizeCharityModifier;
            return utility;
        }

        private float HelpTownsfolkUtility()
        {
            float utility = UnityEngine.Random.Range(0f, 15f) + ((float)character.Empathy * 0.5f) + ((float)character.Good * 0.2f) + ((float)character.Sociability * 0.15f) + (mood * 0.1f) - (loneliness * 0.1f) - (stress * 0.5f);
            utility += helpTownsfolkModifier;
            return utility; 
        }

        private float VolunteerWorkUtility()
        {
            float utility = UnityEngine.Random.Range(0f, 15f) + ((float)character.Empathy * 0.5f) + ((float)character.Good * 0.2f) + ((float)character.Sociability * 0.15f) + (mood * 0.1f) - (loneliness * 0.1f) - (stress * 0.5f);
            utility += volunteerWorkModifier;
            return utility;
        }

        private float AttendCouncilUtility()
        {
            float utility = UnityEngine.Random.Range(0f, 15f) + ((float)character.Leadership * 0.5f) + ((float)character.Good * 0.2f) + ((float)character.Sociability * 0.15f) + (mood * 0.1f) - (loneliness * 0.1f) - (stress * 0.5f);
            utility += attendCouncilModifier;
            return utility;
        }

        private float DonateUtility()
        {
            float utility = UnityEngine.Random.Range(0f, 15f) + ((float)character.gold * 0.1f) + ((float)character.Good * 0.2f) + (mood * 0.1f);
            utility += donateModifier;
            return utility;
        }

        ////////////////////////////////////
        ////// Action Methods //////////////
        ////////////////////////////////////

        private void AssistHomelessAction()
        {
            isActive = true;
            isDoing = "Assisting Homeless";

            AssistHomeless assistHomeless = new AssistHomeless(this, character);
        }

        private void OrganizeCharityAction()
        {
            isActive = true;
            isDoing = "Organizing Charity";

            OrganizeCharity organizeCharity = new OrganizeCharity(this, character);
        }

        private void HelpTownsfolkAction()
        {
            isActive = true;
            isDoing = "Helping Townsfolk";

            HelpTownsfolk helpTownsfolk = new HelpTownsfolk(this, character);
        }

        private void VolunteerWorkAction()
        {
            isActive = true;
            isDoing = "Volunteering";

            VolunteerWork volunteerWork = new VolunteerWork(this, character);
        }

        private void AttendCouncilAction()
        {
            isActive = true;
            isDoing = "Attending Council";

            AttendCouncil attendCouncil = new AttendCouncil(this, character);
        }

        private void DonateAction()
        {
            isActive = true;
            isDoing = "Donating";

            Donate donate = new Donate(this, character);
        }

    #endregion

    #region Relaxation Action

        private void RelaxationActionSelector()
        {
            float bathhouseUtility = BathhouseUtility();
            float birdwatchingUtility = BirdwatchingUtility();
            float fishingUtility = FishingUtility();
            float meditateUtility = MeditateUtility();
            float whorehouseUtility = WhorehouseUtility();
            float sunbathingUtility = SunbathingUtility();
            float stargazingUtility = StargazingUtility();
            float tavernRelaxationUtility = TavernRelaxationUtility();

            float maxUtility = Mathf.Max(bathhouseUtility, birdwatchingUtility, fishingUtility, meditateUtility, whorehouseUtility, sunbathingUtility, stargazingUtility, tavernRelaxationUtility);
        
            if (maxUtility == bathhouseUtility)
            {
                BathhouseAction();
            }
            else if (maxUtility == birdwatchingUtility)
            {
                BirdwatchingAction();
            }
            else if (maxUtility == fishingUtility)
            {
                FishingAction();
            }
            else if (maxUtility == meditateUtility)
            {
                MeditateAction();
            }
            else if (maxUtility == whorehouseUtility)
            {
                WhorehouseAction();
            }
            else if (maxUtility == sunbathingUtility)
            {
                SunbathingAction();
            }
            else if (maxUtility == stargazingUtility)
            {
                StargazingAction();
            }
            else if (maxUtility == tavernRelaxationUtility)
            {
                TavernRelaxationAction();
            }

        }

        ////////////////////////////////////
        ////// Action Utilities ////////////
        ////////////////////////////////////

        private float BathhouseUtility()
        {
            float utility = UnityEngine.Random.Range(0f, 15f) + ((float)character.Sociability * 0.3f) + ((float)character.Charisma * 0.1f) + (mood * 0.1f) - (comfort * 0.5f);       
            utility += bathhouseModifier;
            return utility;
        }

        private float BirdwatchingUtility()
        {
            float utility = UnityEngine.Random.Range(0f, 15f) + ((float)character.Perception * 0.5f) + ((float)character.Intelligence * 0.3f) - (comfort * 0.3f) - ((float)character.Sociability * 0.3f);       
            utility += birdwatchingModifier;
            return utility;
        }

        private float FishingUtility()
        {
            float utility = UnityEngine.Random.Range(0f, 15f) + ((float)character.Perception * 0.5f) + ((float)character.Patience * 0.2f) - (mood * 0.1f) - (comfort * 0.5f);       
            utility += fishingModifier;
            return utility;
        }

        private float MeditateUtility()
        {
            float utility = UnityEngine.Random.Range(0f, 15f) + ((float)character.Wisdom * 0.5f) + ((float)character.Humility * 0.1f) - (loneliness * 0.2f) - (comfort * 0.3f);       
            utility += meditateModifier;
            return utility;
        }

        private float WhorehouseUtility()
        {
            float utility = UnityEngine.Random.Range(0f, 15f) + ((float)character.Ambition * 0.3f) + ((float)character.Evil * 0.3f) + (loneliness * 0.3f) - (mood * 0.1f) - ((float)character.Integrity * 0.2f);
            utility += whorehouseModifier;
            return utility;
        }

        private float SunbathingUtility()
        {
            float utility = UnityEngine.Random.Range(0f, 15f) + ((float)character.Charisma * 0.5f) - ((float)character.Sociability * 0.3f) + (mood * 0.1f) - (comfort * 0.5f);
            utility += sunbathingModifier;
            return utility;
        }

        private float StargazingUtility()
        {
            float utility = UnityEngine.Random.Range(0f, 15f) + ((float)character.Perception * 0.5f) + ((float)character.Curiosity * 0.1f) - (comfort * 0.5f);
            utility += stargazingModifier;
            return utility;
        }

        private float TavernRelaxationUtility()
        {
            float utility = UnityEngine.Random.Range(0f, 15f) + ((float)character.Sociability * 0.5f) + ((float)character.Charisma * 0.1f) - (comfort * 0.3f) + (loneliness * 0.3f) + (stress * 0.2f);
            utility += tavernRelaxationModifier;
            return utility;
        }
        ////////////////////////////////////
        ////// Action Methods //////////////
        ////////////////////////////////////

        private void BathhouseAction()
        {
            isActive = true;
            isDoing = "Bathhouse";

            Bathhouse bathhouse = new Bathhouse(this, character);
        }

        private void BirdwatchingAction()
        {
            isActive = true;
            isDoing = "Birdwatching";

            Birdwatching birdwatching = new Birdwatching(this, character);
        }

        private void FishingAction()
        {
            isActive = true;
            isDoing = "Fishing";

            Fishing fishing = new Fishing(this, character);
        }

        private void MeditateAction()
        {
            isActive = true;
            isDoing = "Meditating";

            Meditate meditate = new Meditate(this, character);
        }

        private void WhorehouseAction()
        {
            isActive = true;
            isDoing = "Whoring";

            Whoring whoring = new Whoring(this, character);
        }

        private void SunbathingAction()
        {
            isActive = true;
            isDoing = "Sunbathing";

            Sunbathing sunbathing = new Sunbathing(this, character);
        }

        private void StargazingAction()
        {
            isActive = true;
            isDoing = "Stargazing";

            Stargazing stargazing = new Stargazing(this, character);
        }

        private void TavernRelaxationAction()
        {
            isActive = true;
            isDoing = "Tavern Relaxation";

            TavernRelaxation tavernRelaxation = new TavernRelaxation(this, character);
        }

    #endregion

    #region Rest Action

        private void RestActionSelector()
        {
            float sleepUtility = SleepUtility();
            float exhaustionUtility = ExhaustionUtility();

            float maxUtility = Mathf.Max(sleepUtility, exhaustionUtility);
        
            if (maxUtility == sleepUtility)
            {
                SleepAction();
            }
            else if (maxUtility == exhaustionUtility)
            {
                ExhaustionAction();
            }
        }

        ////////////////////////////////////
        ////// Action Utilities ////////////
        ////////////////////////////////////

        private float SleepUtility()
        {
            float utility = ((float)character.Tenacity * 0.2f) + ((float)character.Constitution * 0.3f) - (fatigue * 0.8f) + (stress * 0.3f);       
            utility += sleepModifier;
            return utility;
        }

        private float ExhaustionUtility()
        {
            float utility = ((float)character.Tenacity * 0.3f) - ((float)character.Constitution * 0.5f) - ((float)character.Willpower * 0.3f) + (fatigue * 0.3f) + (stress * 0.5f);       
            utility += exhaustionModifier;
            return utility;
        }

        ////////////////////////////////////
        ////// Action Methods //////////////
        ////////////////////////////////////

        private void SleepAction()
        {
            isActive = true;
            isDoing = "Sleeping";

            Sleep sleep = new Sleep(this, character);
        }

        private void ExhaustionAction()
        {
            isActive = true;
            isDoing = "Exhaustion";

            Exhaustion exhaustion = new Exhaustion(this, character);
        }


    #endregion

    #region Shopping Action

        private void ShoppingActionSelector()
        {
            float goEatUtility = GoEatUtility();
            float barterUtility = BarterUtility();
            float buyItemUtility = BuyItemUtility();
            float sellItemUtility = SellItemUtility();
            float windowShoppingUtility = WindowShoppingUtility();
            float comissionItemUtility = ComissionItemUtility();

            float maxUtility = Mathf.Max(sellItemUtility, windowShoppingUtility, comissionItemUtility, goEatUtility, barterUtility, buyItemUtility);
        
            if (maxUtility == sellItemUtility)
            {
                SellItemAction();
            }
            else if (maxUtility == windowShoppingUtility)
            {
                WindowShoppingAction();
            }
            else if (maxUtility == comissionItemUtility)
            {
                ComissionItemAction();
            }
            else if (maxUtility == goEatUtility)
            {
                GoEatAction();
            }
            else if (maxUtility == barterUtility)
            {
                BarterAction();
            }
            else if (maxUtility == buyItemUtility)
            {
                BuyItemAction();
            }
        }

        ////////////////////////////////////
        ////// Action Utilities ////////////
        ////////////////////////////////////

        private float GoEatUtility()
        {
            float utility = UnityEngine.Random.Range(0f, 15f) +  + ((float)character.gold * 0.1f) + ((float)character.Ambition * 0.2f) + ((float)character.Curiosity * 0.1f) + (mood * 0.2f) - (stress * 0.2f);
            utility += goEatModifier;
            return utility;
        }

        private float BarterUtility()
        {
            float utility = UnityEngine.Random.Range(0f, 15f) + ((float)character.Resourcefulness * 0.3f) + ((float)character.Persuasion * 0.3f) + ((float)character.Charisma * 0.1f) + ((float)character.Sociability * 0.1f) + ((float)character.gold * 0.1f) - (stress * 0.2f) - (fatigue * 0.2f);       
            utility += barterModifier;
            return utility;
        }

        private float BuyItemUtility()
        {
            float utility = UnityEngine.Random.Range(0f, 15f) + ((float)character.gold * 0.1f) + ((float)character.Ambition * 0.2f) + ((float)character.Curiosity * 0.1f) + (mood * 0.1f) - (stress * 0.2f);
            utility += buyItemModifier;
            return utility;
        }
        
        private float SellItemUtility()
        {
            float utility = UnityEngine.Random.Range(0f, 15f) + ((float)character.Resourcefulness * 0.3f) + ((float)character.Intelligence * 0.1f) + ((float)character.Sociability * 0.1f) + (stress * 0.5f);
            
            // Calculate the sellItemModifier based on the character's gold
            if (character.gold < 100)
            {
                sellItemModifier += 20f;
            }
            else if (character.gold < 500)
            {
                sellItemModifier += 10f;
            }
            else if (character.gold < 1000)
            {
                sellItemModifier += 5f;
            }
            
            utility += sellItemModifier;
            return utility;
        }

        private float WindowShoppingUtility()
        {
            float utility = UnityEngine.Random.Range(0f, 15f) + ((float)character.Curiosity * 0.8f) + ((float)character.Intelligence * 0.1f) + ((float)character.Perception * 0.3f) + ((float)character.Sociability * 0.1f);       
            utility += windowShoppingModifier;
            return utility;
        }

        private float ComissionItemUtility()
        {
            float utility = UnityEngine.Random.Range(0f, 15f) + ((float)character.Resourcefulness * 0.5f) + ((float)character.Ambition * 0.3f) + ((float)character.Charisma * 0.1f) + ((float)character.Confidence * 0.1f) + ((float)character.gold * 0.09f);       
            utility += comissionItemModifier;
            return utility;
        }

        ////////////////////////////////////
        ////// Action Methods //////////////
        ////////////////////////////////////

        private void GoEatAction()
        {
            isActive = true;
            isDoing = "Going to Eat";

            GoEat goEat = new GoEat(this, character);
        }

        private void BarterAction()
        {
            isActive = true;
            isDoing = "Bartering";

            Barter barter = new Barter(this, character);
        }

        private void BuyItemAction()
        {
            isActive = true;
            isDoing = "Buying Item";

            BuyItem buyItem = new BuyItem(this, character);
        }
        
        private void SellItemAction()
        {
            isActive = true;
            isDoing = "Selling Item";

            SellItem sellItem = new SellItem(this, character);
        }

        private void WindowShoppingAction()
        {
            isActive = true;
            isDoing = "Window Shopping";

            WindowShopping windowShopping = new WindowShopping(this, character);
        }

        private void ComissionItemAction()
        {
            isActive = true;
            isDoing = "Comissioning Item";

            ComissionItem comissionItem = new ComissionItem(this, character);
        }


    #endregion

    #region Training Action

        private void TrainingActionSelector()
        {
            float sparUtility = SparUtility();
            float attendWorkshopUtility = AttendWorkshopUtility();
            float practiceSkillsUtility = PracticeSkillsUtility();
            float liftWeightsUtility = LiftWeightsUtility();
            float runUtility = RunUtility();
            float studyTacticsUtility = StudyTacticsUtility();
            float mentalTrainingUtility = MentalTrainingUtility();

            float maxUtility = Mathf.Max(sparUtility, attendWorkshopUtility, practiceSkillsUtility, liftWeightsUtility, runUtility, studyTacticsUtility, mentalTrainingUtility);

            if (maxUtility == sparUtility)
            {
                SparAction();
            }
            else if (maxUtility == attendWorkshopUtility)
            {
                AttendWorkshopAction();
            }
            else if (maxUtility == practiceSkillsUtility)
            {
                PracticeSkillsAction();
            }
            else if (maxUtility == liftWeightsUtility)
            {
                LiftWeightsAction();
            }
            else if (maxUtility == runUtility)
            {
                RunAction();
            }
            else if (maxUtility == studyTacticsUtility)
            {
                StudyTacticsAction();
            }
            else if (maxUtility == mentalTrainingUtility)
            {
                MentalTrainingAction();
            }

        }

        ////////////////////////////////////
        ////// Action Utilities ////////////
        ////////////////////////////////////

        private float SparUtility()
        {
            float utility = UnityEngine.Random.Range(0f, 15f) + ((float)character.Ambition * 0.3f) + ((float)character.Willpower * 0.2f) + ((float)character.Discipline * 0.5f) - (fatigue * 0.7f);
            utility += sparModifier;
            return utility;
        }

        private float AttendWorkshopUtility()
        {
            float utility = UnityEngine.Random.Range(0f, 15f) + ((float)character.Intelligence * 0.5f) + ((float)character.Wisdom * 0.2f) + ((float)character.Resourcefulness * 0.1f) - (stress * 0.3f) - (fatigue * 0.2f);
            utility += attendWorkshopModifier;
            return utility;
        }

        private float PracticeSkillsUtility()
        {
            float utility = UnityEngine.Random.Range(0f, 15f) + ((float)character.Resourcefulness * 0.3f) + ((float)character.Tenacity * 0.3f) + ((float)character.Discipline * 0.3f) - (fatigue * 0.5f);
            utility += practiceSkillsModifier;
            return utility;
        }

        private float LiftWeightsUtility()
        {
            float utility = UnityEngine.Random.Range(0f, 15f) + ((float)character.Strength * 0.8f) + ((float)character.Constitution * 0.5f) + ((float)character.Tenacity * 0.1f) - (fatigue * 0.5f);
            utility += liftWeightsModifier;
            return utility;
        }

        private float RunUtility()
        {
            float utility = UnityEngine.Random.Range(0f, 15f) + ((float)character.Agility * 0.8f) + ((float)character.Constitution * 0.2f) + ((float)character.Tenacity * 0.1f) - (fatigue * 0.5f);
            utility += runModifier;
            return utility;
        }

        private float StudyTacticsUtility()
        {
            float utility = UnityEngine.Random.Range(0f, 15f) + ((float)character.Intelligence * 0.5f) + ((float)character.Wisdom * 0.5f) + ((float)character.Resourcefulness * 0.1f) + ((float)character.Leadership * 0.5f) - (stress * 0.5f);
            utility += studyTacticsModifier;
            return utility;
        }

        private float MentalTrainingUtility()
        {
            float utility = UnityEngine.Random.Range(0f, 15f) + ((float)character.Wisdom * 0.8f) + ((float)character.Willpower * 0.2f) + ((float)character.Intelligence * 0.8f) - (stress * 0.5f);
            utility += mentalTrainingModifier;
            return utility;
        }

        ////////////////////////////////////
        ////// Action Methods //////////////
        ////////////////////////////////////

        private void SparAction()
        {
            isActive = true;
            isDoing = "Sparing";

            Spar spar = new Spar(this, character);
        }

        private void AttendWorkshopAction()
        {
            isActive = true;
            isDoing = "Attending Workshop";

            AttendWorkshop attendWorkshop = new AttendWorkshop(this, character);
        }

        private void PracticeSkillsAction()
        {
            isActive = true;
            isDoing = "Practicing Skills";

            PracticeSkills practiceSkills = new PracticeSkills(this, character);
        }

        private void LiftWeightsAction()
        {
            isActive = true;
            isDoing = "Lifting Weights";

            LiftWeights liftWeights = new LiftWeights(this, character);
        }

        private void RunAction()
        {
            isActive = true;
            isDoing = "Running";

            Run run = new Run(this, character);
        }

        private void StudyTacticsAction()
        {
            isActive = true;
            isDoing = "Studying Tactics";

            StudyTactics studyTactics = new StudyTactics(this, character);
        }

        private void MentalTrainingAction()
        {
            isActive = true;
            isDoing = "Mental Training";

            MentalTraining mentalTraining = new MentalTraining(this, character);
        }

    #endregion

    #region Socialization Action

        private void SocializationActionSelector()
        {
            float goToGuildMeetingUtility = GoToGuildMeetingUtility();
            float talkwithCollegueUtility = TalkWithCollegueUtility();
            float talkwithTownsfolkUtility = TalkWithTownsfolkUtility();
            float romanceUtility = RomanceUtility();
            float makeFriendsUtility = MakeFriendsUtility();
            float attendPartyUtility = AttendPartyUtility();

            float maxUtility = Mathf.Max(goToGuildMeetingUtility, talkwithCollegueUtility, talkwithTownsfolkUtility, romanceUtility, makeFriendsUtility, attendPartyUtility);

            if (maxUtility == goToGuildMeetingUtility)
            {
                GoToGuildMeetingAction();
            }
            else if (maxUtility == talkwithCollegueUtility)
            {
                TalkWithCollegueAction();
            }
            else if (maxUtility == talkwithTownsfolkUtility)
            {
                TalkWithTownsfolkAction();
            }
            else if (maxUtility == romanceUtility)
            {
                RomanceAction();
            }
            else if (maxUtility == makeFriendsUtility)
            {
                MakeFriendsAction();
            }
            else if (maxUtility == attendPartyUtility)
            {
                AttendPartyAction();
            }

        }

        ////////////////////////////////////
        ////// Action Utilities ////////////
        ////////////////////////////////////

        private float GoToGuildMeetingUtility()
        {
            float utility = UnityEngine.Random.Range(0f, 15f) + ((float)character.Leadership * 0.5f) + ((float)character.Sociability * 0.3f) + ((float)character.Intelligence * 0.1f) - (stress * 0.3f) - (fatigue * 0.2f);
            utility += goToGuildMeetingModifier;
            return utility;
        }

        private float TalkWithCollegueUtility()
        {
            float utility = UnityEngine.Random.Range(0f, 15f) + ((float)character.Sociability * 0.5f) + (loneliness * 0.3f) - (stress * 0.3f);
            utility += talkWithCollegueModifier;
            return utility;
        }

        private float TalkWithTownsfolkUtility()
        {
            float utility = UnityEngine.Random.Range(0f, 15f) + ((float)character.Sociability * 0.5f) + (loneliness * 0.3f) - (stress * 0.3f);
            utility += talkWithTownsfolkModifier;
            return utility;
        }

        private float RomanceUtility()
        {
            float utility = UnityEngine.Random.Range(0f, 15f) + ((float)character.Charisma * 0.5f) + ((float)character.Sociability * 0.3f) + (loneliness * 0.2f) - (stress * 0.3f);
            utility += romanceModifier;
            return utility;
        }

        private float MakeFriendsUtility()
        {
            float utility = UnityEngine.Random.Range(0f, 15f) + ((float)character.Sociability * 0.5f) + ((float)character.Charisma * 0.3f) - (stress * 0.3f);
            utility += makeFriendsModifier;
            return utility;
        }

        private float AttendPartyUtility()
        {
            float utility = UnityEngine.Random.Range(0f, 15f) + ((float)character.Sociability * 0.5f) + ((float)character.Charisma * 0.3f) + (loneliness * 0.1f) + ((float)character.Diplomacy * 0.1f) - (stress * 0.5f);
            utility += attendPartyModifier;
            return utility;
        }

        ////////////////////////////////////
        ////// Action Methods //////////////
        ////////////////////////////////////

        private void GoToGuildMeetingAction()
        {
            isActive = true;
            isDoing = "Going to Guild Meeting";

            GoToGuildMeeting goToGuildMeeting = new GoToGuildMeeting(this, character);
        }

        private void TalkWithCollegueAction()
        {
            isActive = true;
            isDoing = "Talking with Collegue";

            TalkWithCollegue talkWithCollegue = new TalkWithCollegue(this, character);
        }

        private void TalkWithTownsfolkAction()
        {
            isActive = true;
            isDoing = "Talking with Townsfolk";

            TalkWithTownsfolk talkWithTownsfolk = new TalkWithTownsfolk(this, character);
        }

        private void RomanceAction()
        {
            isActive = true;
            isDoing = "Romancing";

            Romance romance = new Romance(this, character);
        }

        private void MakeFriendsAction()
        {
            isActive = true;
            isDoing = "Making Friends";

            MakeFriends makeFriends = new MakeFriends(this, character);
        }

        private void AttendPartyAction()
        {
            isActive = true;
            isDoing = "Attending Party";

            AttendParty attendParty = new AttendParty(this, character);
        }


    #endregion

    #region Spirituality Action

        private void SpiritualityActionSelector()
        {
            float goToChurchUtility = GoToChurchUtility();
            float prayUtility = PrayUtility();
            float pilgrimageUtility = PilgrimageUtility();
            float innerReflectionUtility = InnerReflectionUtility();
            float seekSpiritualGuidanceUtility = SeekSpiritualGuidanceUtility();

            float maxUtility = Mathf.Max(goToChurchUtility, prayUtility, pilgrimageUtility, innerReflectionUtility, seekSpiritualGuidanceUtility);

            if (maxUtility == goToChurchUtility)
            {
                GoToChurchAction();
            }
            else if (maxUtility == prayUtility)
            {
                PrayAction();
            }
            else if (maxUtility == pilgrimageUtility)
            {
                PilgrimageAction();
            }
            else if (maxUtility == innerReflectionUtility)
            {
                InnerReflectionAction();
            }
            else if (maxUtility == seekSpiritualGuidanceUtility)
            {
                SeekSpiritualGuidanceAction();
            }

        }

        ////////////////////////////////////
        ////// Action Utilities ////////////
        ////////////////////////////////////

        private float GoToChurchUtility()
        {
            float utility = UnityEngine.Random.Range(0f, 15f) + ((float)character.Discipline * 0.5f) + ((float)character.Humility * 0.3f) + ((float)character.Wisdom * 0.8f) - (stress * 0.3f);
            utility += goToChurchModifier;
            return utility;
        }

        private float PrayUtility()
        {
            float utility = UnityEngine.Random.Range(0f, 15f) + ((float)character.Humility * 0.3f) + ((float)character.Wisdom * 0.5f) + (stress * 0.3f);
            utility += prayModifier;
            return utility;
        }

        private float PilgrimageUtility()
        {
            float utility = UnityEngine.Random.Range(0f, 15f) + ((float)character.Discipline * 0.3f) + ((float)character.Willpower * 0.3f) + ((float)character.Tenacity * 0.1f) - (fatigue * 0.8f);
            utility += pilgrimageModifier;
            return utility;
        }

        private float InnerReflectionUtility()
        {
            float utility = UnityEngine.Random.Range(0f, 15f) + ((float)character.Patience * 0.5f) + (stress * 0.3f) + ((float)character.Wisdom * 0.8f) - (loneliness * 0.8f);
            utility += innerReflectionModifier;
            return utility;
        }

        private float SeekSpiritualGuidanceUtility()
        {
            float utility = UnityEngine.Random.Range(0f, 15f) + ((float)character.Sociability * 0.5f) + ((float)character.Humility * 0.3f) + ((float)character.Wisdom * 0.1f) - ((float)character.Ambition * 0.9f);
            utility += seekSpiritualGuidanceModifier;
            return utility;
        }

        ////////////////////////////////////
        ////// Action Methods //////////////
        ////////////////////////////////////

        private void GoToChurchAction()
        {
            isActive = true;
            isDoing = "Going to Church";

            GoToChurch goToChurch = new GoToChurch(this, character);
        }

        private void PrayAction()
        {
            isActive = true;
            isDoing = "Praying";

            Pray pray = new Pray(this, character);
        }

        private void PilgrimageAction()
        {
            isActive = true;
            isDoing = "Pilgrimage";

            Pilgrimage pilgrimage = new Pilgrimage(this, character);
        }

        private void InnerReflectionAction()
        {
            isActive = true;
            isDoing = "Inner Reflection";

            InnerReflection innerReflection = new InnerReflection(this, character);
        }

        private void SeekSpiritualGuidanceAction()
        {
            isActive = true;
            isDoing = "Seeking Spiritual Guidance";

            SeekSpiritualGuidance seekSpiritualGuidance = new SeekSpiritualGuidance(this, character);
        }

    #endregion

    #region Entertainment Action

        private void EntertainmentActionSelector()
        {
            float watchShowUtility = WatchShowUtility();
            float goDrinkUtility = GoDrinkUtility();
            float attendPerformanceUtility = AttendPerformanceUtility();
            float goGamblingUtility = GoGamblingUtility();
            float tavernEntertainmentUtility = TavernEntertainmentUtility();

            float maxUtility = Mathf.Max(watchShowUtility, goDrinkUtility, attendPerformanceUtility, goGamblingUtility, tavernEntertainmentUtility);

            if (maxUtility == watchShowUtility)
            {
                WatchShowAction();
            }
            else if (maxUtility == goDrinkUtility)
            {
                GoDrinkAction();
            }
            else if (maxUtility == attendPerformanceUtility)
            {
                AttendPerformanceAction();
            }
            else if (maxUtility == goGamblingUtility)
            {
                GoGamblingAction();
            }
            else if (maxUtility == tavernEntertainmentUtility)
            {
                TavernEntertainmentAction();
            }

        }

        ////////////////////////////////////
        ////// Action Utilities ////////////
        ////////////////////////////////////

        private float WatchShowUtility()
        {
            float utility = UnityEngine.Random.Range(0f, 15f) + ((float)character.Curiosity * 0.7f) + ((float)character.Perception * 0.3f) + (mood * 0.3f) - (stress * 0.3f) - (fatigue * 0.3f);
            utility += watchShowModifier;
            return utility;
        }

        private float GoDrinkUtility()
        {
            float utility = UnityEngine.Random.Range(0f, 15f) + ((float)character.Sociability * 0.5f) + ((float)character.Charisma * 0.3f) + (mood * 0.2f) + (stress * 0.2f) - (fatigue * 0.5f);
            utility += goDrinkModifier;
            return utility;
        }

        private float AttendPerformanceUtility()
        {
            float utility = UnityEngine.Random.Range(0f, 15f) + ((float)character.Curiosity * 0.5f) + ((float)character.Sociability * 0.3f) + (mood * 0.5f) - (stress * 0.3f) - (fatigue * 0.3f);
            utility += attendPerformanceModifier;
            return utility;
        }

        private float GoGamblingUtility()
        {
            float utility = UnityEngine.Random.Range(0f, 15f) + ((float)character.Luck * 0.5f) + ((float)character.Confidence * 0.7f) + ((float)character.Ambition * 0.1f) - ((float)character.Humility * 0.3f) - ((float)character.Honesty * 0.3f);
            utility += goGamblingModifier;
            return utility;
        }

        private float TavernEntertainmentUtility()
        {
            float utility = UnityEngine.Random.Range(0f, 15f) + ((float)character.Sociability * 0.7f) + ((float)character.Morale * 0.3f) + (mood * 0.3f) - (stress * 0.3f);
            utility += tavernEntertainmentModifier;
            return utility;
        }

        ////////////////////////////////////
        ////// Action Methods //////////////
        ////////////////////////////////////

        private void WatchShowAction()
        {
            isActive = true;
            isDoing = "Watching Show";

            WatchShow watchShow = new WatchShow(this, character);
        }

        private void GoDrinkAction()
        {
            isActive = true;
            isDoing = "Going to Drink";

            GoDrink goDrink = new GoDrink(this, character);
        }

        private void AttendPerformanceAction()
        {
            isActive = true;
            isDoing = "Attending Performance";

            AttendPerformance attendPerformance = new AttendPerformance(this, character);
        }

        private void GoGamblingAction()
        {
            isActive = true;
            isDoing = "Going to Gamble";

            GoGambling goGambling = new GoGambling(this, character);
        }

        private void TavernEntertainmentAction()
        {
            isActive = true;
            isDoing = "Tavern Entertainment";

            TavernEntertainment tavernEntertainment = new TavernEntertainment(this, character);
        }

    #endregion

    #region Studying Action

        private void StudyingActionSelector()
        {
            float goToLibraryUtility = GoToLibraryUtility();
            float readSkillBookUtility = ReadSkillBookUtility();
            float attendLectureUtility = AttendLectureUtility();
            float studyHistoryUtility = StudyHistoryUtility();

            float maxUtility = Mathf.Max(goToLibraryUtility, readSkillBookUtility, attendLectureUtility, studyHistoryUtility);

            if (maxUtility == goToLibraryUtility)
            {
                GoToLibraryAction();
            }
            else if (maxUtility == readSkillBookUtility)
            {
                ReadSkillBookAction();
            }
            else if (maxUtility == attendLectureUtility)
            {
                AttendLectureAction();
            }
            else if (maxUtility == studyHistoryUtility)
            {
                StudyHistoryAction();
            }

        }

        ////////////////////////////////////
        ////// Action Utilities ////////////
        ////////////////////////////////////

        private float GoToLibraryUtility()
        {
            float utility = UnityEngine.Random.Range(0f, 15f) + ((float)character.Intelligence * 0.5f) + ((float)character.Curiosity * 0.3f) + ((float)character.Wisdom * 0.2f) - (stress * 0.3f) - (fatigue * 0.2f);
            utility += goToLibraryModifier;
            return utility;
        }

        private float ReadSkillBookUtility()
        {
            float utility = UnityEngine.Random.Range(0f, 15f) + ((float)character.Curiosity * 0.5f) + ((float)character.Perception * 0.2f) + ((float)character.Patience * 0.3f) - (stress * 0.3f) - (fatigue * 0.2f);
            utility += readSkillBookModifier;
            return utility;
        }

        private float AttendLectureUtility()
        {
            float utility = UnityEngine.Random.Range(0f, 15f) + ((float)character.Intelligence * 0.5f) + ((float)character.Ambition * 0.3f) + ((float)character.Morale * 0.2f) - (stress * 0.3f) - (fatigue * 0.3f);
            utility += attendLectureModifier;
            return utility;
        }

        private float StudyHistoryUtility()
        {
            float utility = UnityEngine.Random.Range(0f, 15f) + ((float)character.Intelligence * 0.5f) + ((float)character.Curiosity * 0.5f) + ((float)character.Wisdom * 0.5f) + ((float)character.Creativity * 0.3f) - (fatigue * 0.4f);
            utility += studyHistoryModifier;
            return utility;
        }

        ////////////////////////////////////
        ////// Action Methods //////////////
        ////////////////////////////////////

        private void GoToLibraryAction()
        {
            isActive = true;
            isDoing = "Going to Library";

            GoToLibrary goToLibrary = new GoToLibrary(this, character);
        }

        private void ReadSkillBookAction()
        {
            isActive = true;
            isDoing = "Reading Skill Book";

            ReadSkillBook readSkillBook = new ReadSkillBook(this, character);
        }

        private void AttendLectureAction()
        {
            isActive = true;
            isDoing = "Attending Lecture";

            AttendLecture attendLecture = new AttendLecture(this, character);
        }

        private void StudyHistoryAction()
        {
            isActive = true;
            isDoing = "Studying History";

            StudyHistory studyHistory = new StudyHistory(this, character);
        }

    #endregion

    #region Villainy Action

        private void VillainyActionSelector()
        {
            float pickpocketUtility = PickpocketUtility();
            float stealUtility = StealUtility();
            float spyUtility = SpyUtility();
            float gangWorkUtility = GangWorkUtility();
            float murderUtility = MurderUtility();

            float maxUtility = Mathf.Max(pickpocketUtility, stealUtility, spyUtility, gangWorkUtility, murderUtility);

            if (maxUtility == pickpocketUtility)
            {
                PickpocketAction();
            }
            else if (maxUtility == stealUtility)
            {
                StealAction();
            }
            else if (maxUtility == spyUtility)
            {
                SpyAction();
            }
            else if (maxUtility == gangWorkUtility)
            {
                GangWorkAction();
            }
            else if (maxUtility == murderUtility)
            {
                MurderAction();
            }

        }

        ////////////////////////////////////
        ////// Action Utilities ////////////
        ////////////////////////////////////

        private float PickpocketUtility()
        {
            float utility = UnityEngine.Random.Range(0f, 15f) + ((float)character.Agility * 0.9f) + ((float)character.Perception * 0.3f) + ((float)character.Cunning * 0.2f) + ((float)character.Resourcefulness * 0.2f) - (stress * 0.3f);
            utility += pickpocketModifier;
            return utility;
        }

        private float StealUtility()
        {
            float utility = UnityEngine.Random.Range(0f, 15f) + ((float)character.Agility * 0.9f) + ((float)character.Confidence * 0.3f) + ((float)character.Resourcefulness * 0.2f) + ((float)character.Tenacity * 0.1f) - (stress * 0.5f);
            utility += stealModifier;
            return utility;
        }

        private float SpyUtility()
        {
            float utility = UnityEngine.Random.Range(0f, 15f) + ((float)character.Perception * 0.5f) + ((float)character.Curiosity * 0.5f) + ((float)character.Cunning * 0.2f) - (stress * 0.3f);
            utility += spyModifier;
            return utility;
        }

        private float GangWorkUtility()
        {
            float utility = UnityEngine.Random.Range(0f, 15f) + ((float)character.Ambition * 0.5f) + ((float)character.Intimidation * 0.2f) + ((float)character.Aggression * 0.2f) - ((float)character.Loyalty * 0.3f) - (stress * 0.3f);
            utility += gangWorkModifier;
            return utility;
        }

        private float MurderUtility()
        {
            float utility = UnityEngine.Random.Range(0f, 15f) + ((float)character.Cunning * 0.5f) + ((float)character.Resourcefulness * 0.3f) + ((float)character.Cunning * 0.2f) - ((float)character.Empathy * 0.5f) - (stress * 0.3f);
            utility += murderModifier;
            return utility;
        }

        ////////////////////////////////////
        ////// Action Methods //////////////
        ////////////////////////////////////

        private void PickpocketAction()
        {
            isActive = true;
            isDoing = "Pickpocketing";

            Pickpocket pickpocket = new Pickpocket(this, character);
        }

        private void StealAction()
        {
            isActive = true;
            isDoing = "Stealing";

            Steal steal = new Steal(this, character);
        }

        private void SpyAction()
        {
            isActive = true;
            isDoing = "Spying";

            Spy spy = new Spy(this, character);
        }

        private void GangWorkAction()
        {
            isActive = true;
            isDoing = "Gang Work";

            GangWork gangWork = new GangWork(this, character);
        }

        private void MurderAction()
        {
            isActive = true;
            isDoing = "Murdering";

            Murder murder = new Murder(this, character);
        }

    #endregion

#endregion

    public IEnumerator WaitAtDoor(float minSeconds, float maxSeconds)
    {
        // Wait until the character reaches the door
        yield return new WaitUntil(() => Vector3.Distance(transform.position, door.transform.position) < 0.1f);
        
        // Wait for a random number of seconds between minSeconds and maxSeconds
        yield return new WaitForSeconds(Random.Range(minSeconds, maxSeconds));

        isActive = false;

        CheckState();

        // Implement the logic for what the character should do after waiting at the door
    }

    public IEnumerator SleepCoroutine(float fatigue)
    {
        // Set the state to Resting
        state = State.Resting;
        isDoing = "Sleeping";

        // Wait until the character reaches the door
        yield return new WaitUntil(() => Vector3.Distance(transform.position, door.transform.position) < 0.1f);

        // Simulate sleep duration (you can adjust the duration as needed)
        float sleepInterval = 1f; // Interval in seconds for checking sleep status and reducing fatigue
        float wakeUpChanceIncrement = 0.1f; // Incremental chance to wake up per second

        while (true)
        {
            // Reduce fatigue each interval
            this.fatigue -= 1f; // Reduce fatigue each second

            // Ensure fatigue does not go below zero
            if (this.fatigue < 0)
            {
                this.fatigue = 0;
            }

            // Check if fatigue is below 30 and increment wake up chance
            if (this.fatigue < 30)
            {
                wakeUpChanceIncrement += 0.1f;
                if (UnityEngine.Random.value < wakeUpChanceIncrement)
                {
                    break; // Break the loop to wake up the character
                }
            }

            yield return new WaitForSeconds(sleepInterval);
        }

        // Set the state back to Idle or another appropriate state
        state = State.Idle;
        isDoing = "Idle";
        wakeUpChanceIncrement = 0.0f; // Reset wake up chance

        // Optionally, you can add other effects of sleeping, such as increasing health or mood
        Debug.Log("Character finished sleeping. Fatigue reduced.");
    }

    public void SetStateToFainted()
    {
        state = State.Fainted;
        isDoing = "Fainted";
        isActive = true;
        Debug.Log("Character fainted from exhaustion.");
    }

    public void SetStateToIdle()
    {
        state = State.Idle;
        isDoing = "Idle";
        isActive = false;
        Debug.Log("Character woke up from fainting.");
    }

    public void GoToSleep()
    {
        // Get the AIDestinationSetter component of the character
        AIDestinationSetter destinationSetter = GetComponent<AIDestinationSetter>();

        // Get the door of the character's house
        TownDoor door = character.house.GetComponentInChildren<TownDoor>();

        if (door != null)
        {
            // Set the target of the AIDestinationSetter to the door
            destinationSetter.target = door.transform;

            // Start the coroutine to sleep
            StartCoroutine(SleepCoroutine(fatigue));
        }
        else
        {
            Debug.LogError("No TownDoor found in the character's house");
        }
    }
    
}







