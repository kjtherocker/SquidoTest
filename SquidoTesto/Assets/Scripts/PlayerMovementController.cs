using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.TextCore.Text;

public class PlayerMovementController : MonoBehaviour
{
  

    public Transform          cameraTransform;
    public Transform          interactableTransform;
    public PlayerMovementData PlayerMovementData;

    public Camera playerCamera;
    
    private BasketBallInput     controls;

    private Interactable_Base heldInteractable;
    
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


        controls.Player.Interact.performed += ctx => OnInteract();
        // Jump
        controls.Player.Jump.performed += ctx => OnJump();
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

        if (heldInteractable != null )
        {
            return;
        }

        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 5))
        {
           
            if (hit.collider.CompareTag("Interactable"))
            {
                Debug.Log("Hit interactable: " + hit.collider.name);

                heldInteractable = hit.collider.GetComponent<Interactable_Base>();
                if (heldInteractable == null)
                {
                    return;
                }
          
                Rigidbody interactableRigidbody = heldInteractable.GetComponent<Rigidbody>();
                if (interactableRigidbody == null)
                {
                    heldInteractable = null;
                    return;
                }
                interactableRigidbody.isKinematic = true;
                interactableRigidbody.useGravity = false;
                
                heldInteractable.transform.SetParent(interactableTransform);
                heldInteractable.transform.localPosition = Vector3.zero;
                heldInteractable.transform.localRotation = Quaternion.identity;

                // Example: call interaction
                // hit.collider.GetComponent<IInteractable>()?.Interact();
            }
           
        }
    }

    void OnInteract()
    {
        heldInteractable.Interact(playerCamera.transform.forward);
        heldInteractable = null;
    }

    void OnJump()
    {
        if (controller.isGrounded)
        {
            verticalVelocity = Mathf.Sqrt(PlayerMovementData.jumpHeight * -2f * PlayerMovementData.gravity);
        }
    }
}
