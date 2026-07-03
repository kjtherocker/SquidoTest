using UnityEngine;


[CreateAssetMenu(fileName = "MovementData", menuName = "Game Data/MovementData")]
public class PlayerMovementData : ScriptableObject
{
    
    [Header("Look")]
    public float mouseSensitivity = 0.5f;
    public float maxLookAngle     = 85f;
    
    [Header("Movement")]
    public float moveSpeed        = 6.0f;
    public float gravity          = -14.00f;
    public float jumpHeight       = 1.0f;
}
