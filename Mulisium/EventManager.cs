using System;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    //Action Maps
    public static Action OnDisablePlayerMovement;
    public static Action OnEnablePlayerMovement;

    //Player Actions
    public static Action OnInteractPressed;

    //Canvas Actions
    public static Action<string> OnChangeDescription;
    public static Action OnDisableDescription;
    public static Action OnChangePauseMenu;
    public static Action<bool> OnShowInteractButton;

    //End Game
    public static Action OnPlaceAppropiatedObject;
}
