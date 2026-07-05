using System;
using UnityEngine;


public class Interactable_Base : MonoBehaviour
{

    private const int MIN_POWER_RANGE = 3;
    private const int MAX_POWER_RANGE = 12;

    public Transform ParentTransform;
    
    public Rigidbody interactableRigidbody;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
      
    }

   public void Interact(Vector3 aForwardVector,float heldPercentage)
    {
      
        ParentTransform.SetParent(null);

        interactableRigidbody.isKinematic = false;
        interactableRigidbody.useGravity  = true;
        float result = Mathf.Lerp(MIN_POWER_RANGE, MAX_POWER_RANGE, heldPercentage);

        interactableRigidbody.AddForce(aForwardVector * result, ForceMode.Impulse);
    }

    public void SetEquip(Transform aEquipTransform)
    {
   
        
        if (!interactableRigidbody)
        {
            return;
        }
        
        interactableRigidbody.isKinematic = true;
        interactableRigidbody.useGravity = false;
        
        ParentTransform.SetParent(aEquipTransform);
        ParentTransform.localPosition = Vector3.zero;
        ParentTransform.localRotation = Quaternion.identity;
    }

    public Vector3 GetVelocity(Vector3 aForwardVector,float heldPercentage)
    {
        float result = Mathf.Lerp(MIN_POWER_RANGE, MAX_POWER_RANGE, heldPercentage);
        return aForwardVector * result / interactableRigidbody.mass;
    }

 
}
