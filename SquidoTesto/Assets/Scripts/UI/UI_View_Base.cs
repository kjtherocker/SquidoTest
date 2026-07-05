using System;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class UI_View_Base : MonoBehaviour
{
    protected CanvasGroup canvasGroup;
    protected virtual void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            return;
        }
        canvasGroup.alpha = 0;
    }

    public virtual void OnPush()
    {
        if (canvasGroup == null)
        {
            return;
        }
        canvasGroup.alpha = 1;
    }

    public virtual void OnPop()
    {
        if (canvasGroup == null)
        {
            return;
        }
        canvasGroup.alpha = 0;
    }
    
}
