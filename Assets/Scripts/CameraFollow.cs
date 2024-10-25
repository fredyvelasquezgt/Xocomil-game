using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;  // Referencia al jugador que la cámara seguirá
    public Vector3 offset = new Vector3(0, 5, -10);  // La distancia entre la cámara y el jugador
    public float smoothSpeed = 0.125f;  // Velocidad de suavizado del seguimiento

    void Start()
    {
        // Si no se asignó un jugador en el inspector, buscar al jugador por su tag
        if (player == null)
        {
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
            if (playerObject != null)
            {
                player = playerObject.transform;
            }
            else
            {
                Debug.LogError("Player object with 'Player' tag not found!");
            }
        }
    }

    void LateUpdate()
    {
        if (player == null) return;

        // Posición deseada: posición del jugador más el offset
        Vector3 desiredPosition = player.position + offset;

        // Interpolación suave entre la posición actual de la cámara y la posición deseada
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Actualizar la posición de la cámara
        transform.position = smoothedPosition;

        // Opcional: hacer que la cámara mire siempre al jugador
        transform.LookAt(player);
    }
}
