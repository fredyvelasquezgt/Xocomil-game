using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioClip musicClip; // Clip de música que se reproducirá
    private static MusicManager instance; // Instancia única del MusicManager
    private AudioSource audioSource; // Componente de AudioSource

    void Awake()
    {
        // Asegura que solo exista una instancia de MusicManager
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // No destruye este objeto entre escenas
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.clip = musicClip;
            audioSource.loop = true; // Repite la canción
            audioSource.playOnAwake = false;
            audioSource.Play(); // Inicia la reproducción
        }
        else
        {
            Destroy(gameObject); // Elimina duplicados
        }
    }

    // Función para detener la música y destruir el objeto
    public static void StopMusic()
    {
        if (instance != null)
        {
            Destroy(instance.gameObject); // Destruye el objeto que contiene la música
            instance = null; // Resetea la instancia
        }
    }
}
