using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.TextCore.Text;

public class PlayerMovementController : MonoBehaviour
{
    
    public Transform          cameraTransform;
    public PlayerMovementData PlayerMovementData;
    
    private BasketBallInput     controls;
    private CharacterController controller;
    
    private Vector2 moveInput;
    private Vector2 lookInput;

    private float   verticalVelocity;
    private float   cameraPitch;

   
    public void Initialize(BasketBallInput aControls)
    {
        controls             = aControls;
        controller           = GetComponent<CharacterController>();
  
        InitializeInput(controls);
    }

    void InitializeInput(BasketBallInput aBallInput)
    {
        BasketBallInput ballInput = aBallInput;
        // Move
        ballInput.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        ballInput.Player.Move.canceled += ctx => moveInput = Vector2.zero;

        // Look
        ballInput.Player.Look.performed += ctx => lookInput = ctx.ReadValue<Vector2>();
        ballInput.Player.Look.canceled += ctx => lookInput = Vector2.zero;
        
        // Jump
        ballInput.Player.Jump.performed += ctx => OnJump();   
    }
    
    void Update()
    {
        HandleMovement();
        HandleLook();
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

    
    void OnJump()
    {
        if (controller.isGrounded)
        {
            verticalVelocity = Mathf.Sqrt(PlayerMovementData.jumpHeight * -2f * PlayerMovementData.gravity);
        }
    }
}
