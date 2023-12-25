using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class DragonSound : MonoBehaviour
{
    public AudioClip roarSound; // The roar sound effect
    private AudioSource audioSource;

    public float triggerDistance = 5.0f; // Adjust this distance as needed
    private ARSessionOrigin arSessionOrigin;
    private Transform arCameraTransform;

    private bool soundPlayed = false;
    void Start()
    {
        // Add AudioSource component to the dinosaur object
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = roarSound;

        // Set other parameters of the audio, such as volume, pitch, etc.
        audioSource.volume = 1.0f;
        audioSource.pitch = 1.0f;

        // Ensure that the audio source doesn't play automatically when the scene starts
        audioSource.playOnAwake = false;

        // Find AR components
        arSessionOrigin = FindObjectOfType<ARSessionOrigin>();
        arCameraTransform = arSessionOrigin.camera.transform;
    }

    void Update()
    {
        // Check the distance between the dinosaur and the AR camera
        float distanceToCamera = Vector3.Distance(transform.position, arCameraTransform.position);

        // If the distance is less than or equal to the trigger distance, play the roar sound
        if (distanceToCamera <= triggerDistance && !soundPlayed)
        {
            Debug.Log("Dragon sounds");
            soundPlayed = true;
            PlayRoarSound();
        }
    }

    void PlayRoarSound()
    {
        // Play the roar sound effect
        if (roarSound != null && audioSource != null)
        {
            audioSource.Stop(); // Stop any previous playback
            audioSource.Play(); // Start playback from the beginning
            //soundPlayed = false;
        }
    }
}
