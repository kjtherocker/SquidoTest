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

   public void Interact(Vector3 aForwardVector)
    {
        interactableRigidbody = GetComponent<Rigidbody>();
        transform.SetParent(null);

        interactableRigidbody.isKinematic = false;
        interactableRigidbody.useGravity = true;

        interactableRigidbody.AddForce(aForwardVector * 16, ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
