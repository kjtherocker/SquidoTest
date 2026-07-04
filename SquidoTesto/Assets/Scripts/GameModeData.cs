using UnityEngine;


[CreateAssetMenu(fileName = "GameModeData", menuName = "Game Data/GameMode Data")]
public class GameModeData : ScriptableObject
{

    public int howManyToScore;
    public GameObject player;
    public GameObject ball;
}

