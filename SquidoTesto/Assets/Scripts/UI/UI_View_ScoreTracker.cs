using System;
using TMPro;
using UnityEngine;

public class UI_View_ScoreTracker : UI_View_Base
{
    public Action<int>  OnSetScore;
    public Action<int> OnScoreChanged;

    private int MaxScore = 0;
    public TextMeshProUGUI tmp_ScoreUI;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        OnScoreChanged += AddScore;
        OnSetScore += SetScore;
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetMaxScore(int aMaxScore)
    {
        MaxScore = aMaxScore;
        SetScore(0);
    }

    public void SetScore(int aScore)
    {
        tmp_ScoreUI.SetText(aScore.ToString()+"/"+ MaxScore.ToString() );
    }

    public void AddScore(int points)
    {
        OnScoreChanged?.Invoke(points);
    }

}
