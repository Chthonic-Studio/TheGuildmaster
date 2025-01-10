using UnityEngine;

public class GuildManager : MonoBehaviour
{
    // Singleton instance
    public static GuildManager Instance { get; private set; }

    // Singleton instance
    public static GuildManager Instance { get; private set; }

    [Header("Guild Stats")]
    [SerializeField] private int funds;

    public int Funds
    {
        get { return funds; }
        private set { funds = value; }
    }

    // References to the guild doors (assigned through the inspector)
    [Header("Guild Areas")]
    [SerializeField] public GameObject GuildDoor;
    [SerializeField] public GameObject Backdoor;
    [SerializeField] public GameObject Weights;
    [SerializeField] public GameObject Arena;

    private void Awake()
    {
        // Ensure only one instance of GuildManager exists
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scenes
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instances
        }
    }

    private void Start()
    {
        // Initialize guild funds
        Funds = 1000; // Default starting funds, can be adjusted

        // Ensure the GuildDoor and Backdoor are assigned in the inspector
        if (GuildDoor == null)
        {
            Debug.LogError("GuildDoor not assigned in the Inspector");
        }

        if (Backdoor == null)
        {
            Debug.LogError("Backdoor not assigned in the Inspector");
        }

        if (Weights == null)
        {
            Debug.LogError("Weights not assigned in the Inspector");
        }

        if (Arena == null)
        {
            Debug.LogError("Arena not assigned in the Inspector");
        }
    }

    // Method to add funds to the guild
    public void AddFunds(int amount)
    {
        Funds += amount;
        Debug.Log($"Guild funds increased by {amount}. Total funds: {Funds}");
    }

    // Method to subtract funds from the guild
    public bool SpendFunds(int amount)
    {
        if (Funds >= amount)
        {
            Funds -= amount;
            Debug.Log($"Guild funds decreased by {amount}. Total funds: {Funds}");
            return true;
        }
        else
        {
            Debug.LogWarning("Not enough funds in the guild");
            return false;
        }
    }
}