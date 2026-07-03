using UnityEngine;

public class BasketBallHandler : GameModeHandler
{
    
    public GameModeData gamemodeData;

    private int currentScore = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
 

    void BasketScored(int aScored)
    {
        currentScore += currentScore;
    }

    void ValidateScore()
    {
        if (currentScore >= gamemodeData.howManyToScore)
        {
            EndFlow();
        }
    }

    void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    protected override void StartFlow()
    {
        base.StartFlow();
        LockCursor();
    }


    protected override void EndFlow()
    {
        base.EndFlow();
        
    }

    // Update is called once per frame
    void Update()
    {  
        
    }
}
