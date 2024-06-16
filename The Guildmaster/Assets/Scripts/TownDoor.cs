using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TownDoor : MonoBehaviour
{
    private Animator animator;
    private Dictionary<GameObject, float> previousZPositions = new Dictionary<GameObject, float>();

    void Start()
    {
        // Get the animator component
        animator = GetComponent<Animator>();
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the colliding object has the tag "Character" or "NPC"
        if (other.gameObject.CompareTag("Character") || other.gameObject.CompareTag("NPC"))
        {
            // Store the previous Z position
            previousZPositions[other.gameObject] = other.gameObject.transform.position.z;

            // Tween the character's Z position to make them invisible
            other.gameObject.transform.DOMoveZ(-100, 1f); // Adjust these values as needed
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Check if the exiting object has the tag "Character" or "NPC"
        if (other.gameObject.CompareTag("Character") || other.gameObject.CompareTag("NPC"))
        {
            // Restore the previous Z position
            if (previousZPositions.ContainsKey(other.gameObject))
            {
                other.gameObject.transform.DOMoveZ(previousZPositions[other.gameObject], 1f);

                // Remove the stored Z position
                previousZPositions.Remove(other.gameObject);
            }
        }
    }
}