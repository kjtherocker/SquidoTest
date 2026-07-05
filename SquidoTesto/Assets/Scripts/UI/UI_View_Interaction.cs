using UnityEngine;

public class UI_View_Interaction : UI_View_Base
{

    public void SetInteractionAction(InteractionHandler aInteractionHandler)
    {
        aInteractionHandler.OnisLookingAtInteractableObj += OnIsLookingAtInteractableObj;
    }

    void OnIsLookingAtInteractableObj(bool aisLookingAtInteractableObj)
    {
        float opacity = aisLookingAtInteractableObj ? 1 : 0;

        canvasGroup.alpha = opacity;
    }
}
