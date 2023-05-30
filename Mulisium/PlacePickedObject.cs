using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacePickedObject : Interactable
{
    public Interactable objectToPlaceHere;
    public Transform placeToPutObject;
    public bool hasCorrectObject = false;
    public override void OnInteract()
    {
        //Show message
    }
    public bool OnPlaceObject(Interactable objectOnHand)
    {
        if (objectOnHand != objectToPlaceHere) return false;


        objectOnHand.transform.parent = placeToPutObject;
        objectOnHand.transform.localPosition = Vector3.zero;
        objectOnHand.transform.localRotation = Quaternion.Euler(-90f,180,0);
        hasCorrectObject = true;
        EventManager.OnPlaceAppropiatedObject?.Invoke();
        return true;
    }
}
