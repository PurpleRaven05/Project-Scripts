using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    //Outline
    [HideInInspector]public Outline outlineObject;
    public Color outlineColorObject = Color.white;
    public Outline.Mode outlineModeObject;
    public float outlineWidthObject = 2f;

    public void Start()
    {
        gameObject.layer = LayerMask.NameToLayer("Interaction");
        SetUpOutline();
    }
    public void SetUpOutline()
    {
        outlineObject = gameObject.AddComponent<Outline>();
        outlineObject.OutlineMode = outlineModeObject;
        outlineObject.OutlineColor = outlineColorObject;
        outlineObject.OutlineWidth = outlineWidthObject;

        outlineObject.enabled = false;
    }
    public abstract void OnInteract();
    public virtual void OnFocus()
    {
        outlineObject.enabled = true;
        EventManager.OnShowInteractButton?.Invoke(true);
    }
    public virtual void OnLoseFocus()
    {
        outlineObject.enabled = false;
        EventManager.OnShowInteractButton?.Invoke(false);

    }

}
