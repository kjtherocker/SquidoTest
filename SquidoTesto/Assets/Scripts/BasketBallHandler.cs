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
        UI_ViewManager.Initialize();
        
        scoreTracker   = (UI_View_ScoreTracker)UI_ViewManager.PushAndGetView(EViewID.Score, EViewTypes.Persistent);
        scoreTracker.SetMaxScore(gamemodeData.howManyToScore);
        
        BallPower      = (UI_View_BallPower)UI_ViewManager.PushAndGetView(EViewID.BallPower, EViewTypes.Persistent);
        
        UI_View_Interaction view_Interaction = (UI_View_Interaction)UI_ViewManager.PushAndGetView(EViewID.Interaction, EViewTypes.Persistent);

        
        List<Hoop> hoops = Object.FindObjectsByType<Hoop>(FindObjectsSortMode.None).ToList();

        foreach (var hoop in hoops)
        {
            hoop.OnScore += BasketScored;
        }
        
        OnScoreChanged += scoreTracker.SetScore;
        var spawnedObject = Instantiate(gamemodeData.player);
        
        Playerhub playerMovementController = spawnedObject.GetComponent<Playerhub>();
        playerMovementController.Initialize();
        
        if (playerMovementController != null)
        {
            playerMovementController.interactionHandler.OnHeldShoot += BallPower.SetSlider;
            view_Interaction.SetInteractionAction(playerMovementController.interactionHandler);
        }
        
    }


    protected override void EndFlow()
    {
        base.EndFlow();
        UI_ViewManager.PopAll();
        UI_ViewManager.PushView(EViewID.Win,EViewTypes.Persistent);
    }

    // Update is called once per frame
    void Update()
    {  
        
    }
}
