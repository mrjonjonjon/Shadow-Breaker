using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootStepSFXController : MonoBehaviour
{
    public AudioSource audioSource; // Reference to the AudioSource component
    public AudioClip footstepClip; // Single footstep sound clip
    public float stepInterval = 0.5f; // Time interval between footsteps
    public float pitchMin = 0.9f; // Minimum pitch value
    public float pitchMax = 1.1f; // Maximum pitch value

    private float stepTimer; // Timer to control footstep playback

    // Start is called before the first frame update
    void Start()
    {
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
        stepTimer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsPlayerMoving())
        {
            stepTimer -= Time.deltaTime;
            if (stepTimer <= 0f)
            {
                PlayFootstepSound();
                stepTimer = stepInterval;
            }
        }
        else
        {
            stepTimer = 0f; // Reset timer when not moving
        }
    }

    private bool IsPlayerMoving()
    {
        // Replace this with your movement detection logic
        return Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0;
    }

    private void PlayFootstepSound()
    {
        if (footstepClip != null)
        {
            audioSource.pitch = Random.Range(pitchMin, pitchMax); // Randomize pitch
            audioSource.PlayOneShot(footstepClip);
        }
    }
}
