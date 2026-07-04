using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Interactable_Base : MonoBehaviour
{

    [HideInInspector]
    public Rigidbody interactableRigidbody;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        interactableRigidbody = GetComponent<Rigidbody>();
    }

   public void Interact(Vector3 aForwardVector,float heldPercentage)
    {
        interactableRigidbody = GetComponent<Rigidbody>();
        transform.SetParent(null);

        interactableRigidbody.isKinematic = false;
        interactableRigidbody.useGravity = true;
        float result = Mathf.Lerp(3, 12, heldPercentage);

        interactableRigidbody.AddForce(aForwardVector * result, ForceMode.Impulse);
    }

    public Vector3 GetVelocity(Vector3 aForwardVector,float heldPercentage)
    {
        float result = Mathf.Lerp(3, 12, heldPercentage);
        return aForwardVector * result / interactableRigidbody.mass;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
