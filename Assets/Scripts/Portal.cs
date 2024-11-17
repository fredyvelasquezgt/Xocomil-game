using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    // Etiqueta del objeto jugador
    public string playerTag = "Player";

    // Nombre del siguiente nivel editable desde el Inspector de Unity
    public string nextLevelName = "L2";

    // Este método se llama cuando otro objeto colisiona con este objeto
    private void OnTriggerEnter(Collider other)
    {
        // Comprobar si el objeto que colisiona tiene la etiqueta "Player"
        if (other.CompareTag(playerTag))
        {
            // Cargar el siguiente nivel usando el nombre proporcionado
            SceneManager.LoadScene(nextLevelName);
        }
    }
}