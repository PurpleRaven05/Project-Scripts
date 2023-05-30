using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowObjectDescription : Interactable
{
    [TextArea(10, 15)]
    public string text;
    public override void OnInteract()
    {

        EventManager.OnShowInteractButton?.Invoke(false);
        EventManager.OnChangeDescription?.Invoke(text);
    }
    public override void OnLoseFocus()
    {
        EventManager.OnDisableDescription?.Invoke();
        this.outlineObject.enabled = false;
        EventManager.OnShowInteractButton?.Invoke(false);

    }

}
