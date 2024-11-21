using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTimer : MonoBehaviour
{
    public float delay = 10f; // Tiempo en segundos antes de cambiar de escena
    public string nextSceneName; // Nombre de la siguiente escena

    void Start()
    {
        // Inicia el temporizador
        StartCoroutine(ChangeSceneAfterDelay());
    }

    IEnumerator ChangeSceneAfterDelay()
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(nextSceneName); // Cambia a la siguiente escena
    }
}
