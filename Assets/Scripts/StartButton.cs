using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    public string gameSceneName = "SampleScene"; // Nombre exacto de la escena
    public AudioClip gameMusicClip; // Clip de música para la nueva escena

    public void StartGame()
    {
        // Detener la música actual
        MusicManager.StopMusic();

        // Cargar la nueva escena
        SceneManager.LoadScene(gameSceneName);

        // Reproducir la nueva canción
        PlayGameMusic();
    }

    private void PlayGameMusic()
    {
        // Crear un nuevo objeto para la música del juego
        GameObject musicObject = new GameObject("GameMusic");
        AudioSource audioSource = musicObject.AddComponent<AudioSource>();
        audioSource.clip = gameMusicClip;
        audioSource.loop = true;
        audioSource.playOnAwake = false;
        DontDestroyOnLoad(musicObject); // Asegurar que no se destruya al cambiar de escena
        audioSource.Play();
    }
}
