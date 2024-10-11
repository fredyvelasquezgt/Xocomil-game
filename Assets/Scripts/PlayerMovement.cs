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
    private CharacterController characterController;
    private Animator animator;
    private Vector3 moveDirection = Vector3.zero;
    private bool isJumping = false;

    // Start is called before the first frame update
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
    }

    // Update is called once per frame
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
            }
            else
            {
                animator.SetFloat("Sprint", 0);
            }

            // Detectar si el jugador presiona la barra espaciadora para saltar
            if (Input.GetKeyDown(KeyCode.Space))
            {
                moveDirection.y = jumpHeight;
                isJumping = true;

                // Reproducir la animación de salto
                animator.SetTrigger("Jumps");
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
    }
}
