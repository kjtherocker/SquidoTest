using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

public class BasketBallHandler : GameModeHandler
{
    
    public GameModeData gamemodeData;
    private UI_View_Manager UI_ViewManager; 
    private int currentScore = 0;

    private UI_View_ScoreTracker scoreTracker;
    private UI_View_BallPower    BallPower;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
 
    public Action<int>  OnSetScore;
    public Action<int> OnScoreChanged;
    
    void BasketScored(int aScored)
    {
        currentScore += aScored;
        OnScoreChanged?.Invoke(currentScore);
        ValidateScore();
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
        
        UI_ViewManager = UI_View_Manager.Instance;   
        scoreTracker   = (UI_View_ScoreTracker)UI_ViewManager.PushAndGetView(EViewID.Score, EViewTypes.Persistent);
        scoreTracker.SetMaxScore(gamemodeData.howManyToScore);
        BallPower   = (UI_View_BallPower)UI_ViewManager.PushAndGetView(EViewID.BallPower, EViewTypes.Persistent);
        
        List<Hoop> hoops = Object.FindObjectsByType<Hoop>(FindObjectsSortMode.None).ToList();

        foreach (var hoop in hoops)
        {
            hoop.OnScore += BasketScored;
        }
        
        OnScoreChanged += scoreTracker.SetScore;
        var spawnedObject = Instantiate(gamemodeData.player);
        PlayerMovementController playerMovementController = spawnedObject.GetComponent<PlayerMovementController>();

        if (playerMovementController != null)
        {
            playerMovementController.OnHeldShoot += BallPower.SetSlider;
        }

        
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
