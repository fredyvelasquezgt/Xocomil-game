using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    [SerializeField]
    private AudioClip backgroundMusic; // El clip de música de fondo
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("AudioSource component not found on " + gameObject.name);
            return;
        }

        // Asegurarse de que el audio esté en loop
        audioSource.loop = true;

        // Asignar el clip de música de fondo
        if (backgroundMusic != null)
        {
            audioSource.clip = backgroundMusic;
            audioSource.Play(); // Reproducir la música de fondo
        }
        else
        {
            Debug.LogError("Background music clip not assigned in " + gameObject.name);
        }
    }
}
