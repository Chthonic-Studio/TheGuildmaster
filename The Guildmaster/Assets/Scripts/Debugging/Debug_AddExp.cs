using UnityEngine;
using UnityEngine.UI;

public class Debug_AddExp : MonoBehaviour
{
    public Button button;
    public int experienceToAdd = 100;
    public LayerMask characterLayer; // Add this line
    private bool isAddingExperience = false;

    void Start()
    {
        button.onClick.AddListener(OnButtonClick);
    }

    void Update()
    {
        if (isAddingExperience && Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, characterLayer)) // Modify this line
            {
                CharacterProfile characterProfile = hit.transform.GetComponent<CharacterProfile>();
                if (characterProfile != null)
                {
                    characterProfile.AddExperience(experienceToAdd);
                    isAddingExperience = false;
                }
            }
        }
    }

    public void OnButtonClick()
    {
        isAddingExperience = true;
    }
}