using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySlot:MonoBehaviour
{
    public string skillName;
    public bool unlocked, active, equiped;
    public Transform self;
    public Transform itemUp, itemDown, itemLeft, itemRight;
    public Transform border;
    
    
    void Update(){
        border.gameObject.SetActive(active);
    }
}
