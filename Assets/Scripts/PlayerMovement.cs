using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private float jumpHeight = 8f; // Altura del salto
    [SerializeField]
    private float gravity = 20f; // Gravedad para el personaje
    [SerializeField]
    private AudioClip grassStepSound; // Sonido de caminar sobre grama
    [SerializeField]
    private Collider punchCollider; // Collider para el golpe

    private CharacterController characterController;
    private Animator animator;
    private AudioSource audioSource;
    private Vector3 moveDirection = Vector3.zero;
    private bool isJumping = false;
    private bool isSprinting = false;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        if (characterController == null)
        {
            Debug.LogError("CharacterController component not found on " + gameObject.name);
        }

        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator component not found on " + gameObject.name);
        }

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("AudioSource component not found on " + gameObject.name);
        }

        // Asegúrate de que el collider del golpe esté desactivado al inicio
        if (punchCollider != null)
        {
            punchCollider.enabled = false;
        }
    }

    void Update()
    {
        float movx = Input.GetAxis("Horizontal");
        float movz = Input.GetAxis("Vertical");

        // Si el personaje está en el suelo
        if (characterController.isGrounded)
        {
            Vector3 movementVector = new Vector3(movx, 0, movz);
            if (movementVector.magnitude > 0)
            {
                movementVector = movementVector.normalized;
                transform.forward = movementVector;
                moveDirection = movementVector * speed;

                // Configurar la animación de movimiento
                animator.SetFloat("Sprint", movementVector.magnitude);

                // Verificar si está corriendo (movimiento > 0)
                if (!isSprinting)
                {
                    isSprinting = true;

                    // Reproducir sonido de sprint
                    if (!audioSource.isPlaying && grassStepSound != null)
                    {
                        audioSource.clip = grassStepSound;
                        audioSource.loop = true; // Hacer que se repita mientras se mueve
                        audioSource.Play();
                    }
                }
            }
            else
            {
                animator.SetFloat("Sprint", 0);
                if (isSprinting)
                {
                    isSprinting = false;
                    audioSource.Stop(); // Detener el sonido al dejar de correr
                }
            }

            // Detectar si el jugador presiona la barra espaciadora para saltar
            if (Input.GetKeyDown(KeyCode.Space))
            {
                moveDirection.y = jumpHeight;
                isJumping = true;

                // Reproducir la animación de salto
                animator.SetTrigger("Jumps");

                // Detener el sonido cuando se salta
                audioSource.Stop();
            }
        }

        // Aplicar gravedad si no está en el suelo
        moveDirection.y -= gravity * Time.deltaTime;

        // Mover al personaje
        characterController.Move(moveDirection * Time.deltaTime);

        // Verificar si terminó de saltar (si volvió al suelo)
        if (isJumping && characterController.isGrounded)
        {
            isJumping = false;
        }

        // Detectar si el jugador presiona la tecla "P" para ejecutar el puñetazo
        if (Input.GetKeyDown(KeyCode.P))
        {
            animator.SetTrigger("PunchRight"); // Asegúrate de que exista un Trigger llamado "PunchRight" en el Animator
            EnablePunchCollider(); // Activar el collider del golpe
        }
    }

    // Activar el collider del golpe
    private void EnablePunchCollider()
    {
        if (punchCollider != null)
        {
            punchCollider.enabled = true;
            // Llamar a un método para desactivar el collider después de un tiempo
            StartCoroutine(DisablePunchCollider());
        }
    }

    // Desactivar el collider del golpe después de un pequeño retraso
    private IEnumerator DisablePunchCollider()
    {
        yield return new WaitForSeconds(0.3f); // Tiempo durante el cual el collider está activo
        if (punchCollider != null)
        {
            punchCollider.enabled = false;
        }
    }

    // Detectar la colisión del golpe
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            // Desaparecer al enemigo
            Destroy(other.gameObject);
        }
    }
}
