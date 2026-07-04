using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public enum EViewID
{
    None      = 0,
    BallPower = 1,
    Score     = 2,
}

public enum EViewTypes
{
    None       = 0,
    Persistent = 1,
    Temporary  = 2,
}

public class UI_View_Manager : SingletonHelper<UI_View_Manager>
{
    
  
    private Dictionary<EViewID, UI_View_Base> views = new();

    private Dictionary<EViewTypes, List<UI_View_Base>> activeViews = new();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Initializing Types
        activeViews.Add(EViewTypes.Persistent,new List<UI_View_Base>());
        activeViews.Add(EViewTypes.Temporary,new List<UI_View_Base>());
        
        
        views = new Dictionary<EViewID, UI_View_Base>();
        views.Add(EViewID.BallPower, GetComponentInChildren<UI_View_BallPower>());
        views.Add(EViewID.Score, GetComponentInChildren<UI_View_ScoreTracker>());
    }
    
    public void PushView(EViewID aViewID,EViewTypes aViewTypes)
    {
        UI_View_Base viewBase = views[aViewID];

        viewBase.OnPush();
        activeViews[aViewTypes].Add(viewBase);
    }
    
    public UI_View_Base PushAndGetView(EViewID aViewID,EViewTypes aViewTypes)
    {
        UI_View_Base viewBase = views[aViewID];

        viewBase.OnPush();
        activeViews[aViewTypes].Add(viewBase);
        
        return viewBase;
    }
    
    public void PopView(EViewID aViewID)
    {
        
    }

    public void PopTop(EViewTypes aViewTypes)
    {
        
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
