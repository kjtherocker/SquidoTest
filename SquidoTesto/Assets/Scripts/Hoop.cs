using System;
using TMPro;
using UnityEngine;
public class Hoop : MonoBehaviour
{
    
    public Action<int>  OnScore;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Interactable"))
        {
            OnScore?.Invoke(3);
            Debug.Log("Ball entered hoop trigger!");
        }
        
    }
}
