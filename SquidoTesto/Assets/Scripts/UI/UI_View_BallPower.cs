using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;
using Slider = UnityEngine.UI.Slider;

public class UI_View_BallPower : UI_View_Base
{
    public Slider Slider;


   public virtual void SetSlider(float aPercentage)
    {
        Slider.value = aPercentage;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void  Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
