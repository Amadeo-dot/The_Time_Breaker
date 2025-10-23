using UnityEngine;
using UnityEngine.InputSystem;

public class player_control : MonoBehaviour
{
    public float speed = 5f;
    public float sensitivity = 2f;
    public float jumpHeight = 2f;
    public float gravity = -9.81f;
    public Camera playerCamera;

    private CharacterController controller;
    private Vector2 moveInput;
    private Vector2 lookInput;
    private float xRotation = 0f;
    private float verticalVelocity = 0f;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void OnLook(InputValue value)
    {
        lookInput = value.Get<Vector2>();
    }

    void Update()
    {
        // Movimiento horizontal
        Vector3 move = transform.right * moveInput.x + transform.forward * moveInput.y;

        // Aplicar gravedad
        if (controller.isGrounded && verticalVelocity < 0)
        {
            verticalVelocity = -2f; // Pequeña fuerza hacia abajo para mantener al jugador pegado al suelo
        }

        verticalVelocity += gravity * Time.deltaTime;
        move.y = verticalVelocity;

        // Mover al jugador
        controller.Move(move * speed * Time.deltaTime);

        // Rotación de cámara
        float mouseX = lookInput.x * sensitivity;
        float mouseY = lookInput.y * sensitivity;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);

        playerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        transform.Rotate(Vector3.up * mouseX);

        if (Keyboard.current.f5Key.wasPressedThisFrame)
        {
            playerCamera.transform.position = new Vector3(0f, 2f, -5f);
        }
    }

    // Función de salto
    void OnJump()
    {
        if (controller.isGrounded)
        {
            verticalVelocity = Mathf.Sqrt(jumpHeight * 1.5f);
        }
    }
}