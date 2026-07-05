using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
public class Hoop : MonoBehaviour
{
    
    public Action<int>  OnScore;
    private readonly Dictionary<GameObject, bool> ballEnteredFromTop = new();

    private const int ADDED_SCORE = 3;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Ball"))
        {
            return;
        }
        
        bool enteredFromTop = other.transform.position.y > transform.position.y;

        ballEnteredFromTop[other.gameObject] = enteredFromTop;
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Ball"))
        {
            return;
        }

        if (!ballEnteredFromTop.TryGetValue(other.gameObject, out bool enteredFromTop))
        {
            return;
        }

        bool exitedBelow = other.transform.position.y < transform.position.y;

        if (enteredFromTop && exitedBelow)
        {
            OnScore?.Invoke(ADDED_SCORE);
        }

        ballEnteredFromTop.Remove(other.gameObject);
    }

}
