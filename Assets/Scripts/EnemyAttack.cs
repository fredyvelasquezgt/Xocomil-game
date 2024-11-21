using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Necesario para cargar escenas

public class EnemyAttack : MonoBehaviour
{
    [SerializeField]
    private float probabilityToKill = 0.5f; // Probabilidad de que el enemigo mate al jugador (50% por defecto)

    // Método llamado cuando el trigger detecta una colisión
    private void OnTriggerEnter(Collider other)
    {
        // Verifica si el objeto detectado tiene el tag "Player"
        if (other.CompareTag("Player"))
        {
            // Genera un número aleatorio entre 0 y 1
            float randomValue = Random.value;

            // Si el valor aleatorio es menor que la probabilidad de matar, se mata al jugador
            if (randomValue < probabilityToKill)
            {
                Debug.Log("Game Over");
                SceneManager.LoadScene("Death"); // Carga la escena con el nombre "Death"
            }
            else
            {
                Debug.Log("Jugador esquivó el ataque");
            }
        }
    }
}
