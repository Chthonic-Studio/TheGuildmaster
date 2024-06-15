using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownDoor : MonoBehaviour
{
    private Animator animator;

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
            // Move the character in the Z position to make them invisible
            Vector3 newPosition = other.gameObject.transform.position;
            newPosition.z = -100; // Adjust this value as needed
            other.gameObject.transform.position = newPosition;

            // Play the door animation
            animator.Play("DoorAnimation"); // Replace "DoorAnimation" with the name of your animation
        }
    }
}
