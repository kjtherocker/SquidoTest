using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.TextCore.Text;

public class PlayerMovementController : MonoBehaviour
{
  

    public Transform          cameraTransform;
    public PlayerMovementData PlayerMovementData;

    public Camera playerCamera;
    
    private BasketBallInput     controls;

    private CharacterController controller;
    
    private Vector2 moveInput;
    private Vector2 lookInput;

    private float   verticalVelocity;
    private float   cameraPitch;

    void Awake()
    {
        controls   = new BasketBallInput();
        controller = GetComponent<CharacterController>();

        // Move
        controls.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        controls.Player.Move.canceled += ctx => moveInput = Vector2.zero;

        // Look
        controls.Player.Look.performed += ctx => lookInput = ctx.ReadValue<Vector2>();
        controls.Player.Look.canceled += ctx => lookInput = Vector2.zero;

        // Jump
        controls.Player.Jump.performed += ctx => Jump();
    }

    void OnEnable()
    {
        controls.Player.Enable();
    }

    void OnDisable()
    {
        controls.Player.Disable();
    }

    void Update()
    {
        HandleMovement();
        HandleLook();

        InteractRayCast();
    }

    void HandleMovement()
    {
        Vector3 move = transform.right * moveInput.x + transform.forward * moveInput.y;

        controller.Move(move * PlayerMovementData.moveSpeed * Time.deltaTime);
        
        if (controller.isGrounded && verticalVelocity < 0)
        {
            verticalVelocity = -2f;
        }

        verticalVelocity += PlayerMovementData.gravity * Time.deltaTime;

        controller.Move(Vector3.up * verticalVelocity * Time.deltaTime);
    }

    void HandleLook()
    {
        float mouseX = lookInput.x * PlayerMovementData.mouseSensitivity;
        float mouseY = lookInput.y * PlayerMovementData.mouseSensitivity;

    
        transform.Rotate(Vector3.up * mouseX);

        float maxLookAngle = PlayerMovementData.maxLookAngle;
        
        cameraPitch -= mouseY;
        cameraPitch = Mathf.Clamp(cameraPitch, -maxLookAngle, maxLookAngle);

        cameraTransform.localRotation = Quaternion.Euler(cameraPitch, 0f, 0f);
    }

    void InteractRayCast()
    {
        if (playerCamera == null)
        {
            return;
        }

        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 50))
        {
            Debug.Log("Hit: " + hit.collider.name);

            // Example: apply damage or interaction
            // hit.collider.GetComponent<Health>()?.TakeDamage(10);
        }
    }

    void Jump()
    {
        if (controller.isGrounded)
        {
            verticalVelocity = Mathf.Sqrt(PlayerMovementData.jumpHeight * -2f * PlayerMovementData.gravity);
        }
    }
}
