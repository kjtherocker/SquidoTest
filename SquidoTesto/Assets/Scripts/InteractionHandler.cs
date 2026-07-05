using System;
using UnityEngine;

public class InteractionHandler : MonoBehaviour
{
    
 
    public Transform interactableTransform;
  
    public Camera playerCamera;
    
    public Action<float>  OnHeldShoot;
    
    public Action<bool>  OnisLookingAtInteractableObj;
    
    private BallTrajectorySpline ballTrajectorySpline;
    private BasketBallInput      controls;
    private Interactable_Base    heldInteractable;
    private CharacterController  controller;

    private float   verticalVelocity;
    private float   cameraPitch;
    private float   interactionShootHold = 0;


    private const float             BASKETBALL_POWER_MULTI = 0.5f;
    private static readonly Vector3 RAYCAST_SPAWN_LOCATION = new Vector3(0.5f, 0.5f, 0f);
    private const float             RAYCAST_DISTANCE       = 3;
    
    public void Initialize(BasketBallInput aControls)
    {
        controls             = aControls;
        ballTrajectorySpline = GetComponentInChildren<BallTrajectorySpline>();

        InitializeInput(controls);
    }
    
    void InitializeInput(BasketBallInput aBallInput)
    {
        BasketBallInput basketBallInput = aBallInput;
        //Holding Down to shoot
        basketBallInput.Player.Shoot.started   += ctx => OnShootStart();
        basketBallInput.Player.Shoot.performed += ctx => OnShootPerformed();
        basketBallInput.Player.Shoot.canceled  += ctx => OnShootEnd();
        
        //Interaction
        basketBallInput.Player.Interact.performed += ctx => OnInteract(); 
    }

    private void Update()
    {
        InteractRayCast();
        if (controls.Player.Shoot.IsPressed())
        {
            OnShootPerformed();
        }
    }

    void InteractRayCast()
    {
        if (playerCamera == null)
        {
            return;
        }
        
        if (isHoldingInteractionItem())
        {
            OnisLookingAtInteractableObj?.Invoke(false);
            return;
        }

        Ray ray = playerCamera.ViewportPointToRay(RAYCAST_SPAWN_LOCATION);
        RaycastHit hit;

        //If I was going to continue I would make a function for both this raycast and the other one
        if (Physics.Raycast(ray, out hit, RAYCAST_DISTANCE))
        {

            if (hit.collider.CompareTag("Interactable"))
            {
                OnisLookingAtInteractableObj?.Invoke(true);
            }
            else
            {
                OnisLookingAtInteractableObj?.Invoke(false);
            }
           
        }
    }

    bool isHoldingInteractionItem()
    {
        return heldInteractable != null;
    }

    void OnInteract()
    {
        if (playerCamera == null)
        {
            return;
        }

        if (isHoldingInteractionItem() )
        {
            return;
        }

        Ray ray = playerCamera.ViewportPointToRay(RAYCAST_SPAWN_LOCATION);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, RAYCAST_DISTANCE))
        {
           
            if (hit.collider.CompareTag("Interactable"))
            {
                heldInteractable = hit.collider.GetComponent<Interactable_Base>();
                if (heldInteractable == null)
                {
                    return;
                }
                heldInteractable.SetEquip(interactableTransform);
            }
            
        }
    }
    
    void OnShootStart()
    {
        if (!isHoldingInteractionItem())
        {
            return;
        }
        
        OnisLookingAtInteractableObj?.Invoke(false);
        interactionShootHold = 0;
        OnHeldShoot?.Invoke(interactionShootHold);
    }

    void OnShootEnd()
    {
        if (!isHoldingInteractionItem())
        {
            return;
        }
        
        OnHeldShoot?.Invoke(interactionShootHold);

        heldInteractable.Interact(playerCamera.transform.forward,interactionShootHold);
        ballTrajectorySpline.SetVelocity(heldInteractable.GetVelocity(playerCamera.transform.forward,interactionShootHold));
        heldInteractable = null;
        ballTrajectorySpline.ClearLineRenderer();
    }

    void OnShootPerformed()
    {
        if (!isHoldingInteractionItem())
        {
            return;
        }
    
        interactionShootHold += Time.deltaTime * BASKETBALL_POWER_MULTI;
        ballTrajectorySpline.SetVelocity(heldInteractable.GetVelocity(playerCamera.transform.forward,interactionShootHold));
        OnHeldShoot?.Invoke(interactionShootHold);
    }

}
