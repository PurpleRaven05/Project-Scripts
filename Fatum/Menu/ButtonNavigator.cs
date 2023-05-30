using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonNavigator : MonoBehaviour
{
    // Start is called before the first frame update
    public int type;
    public bool active;
    public Transform self;
    public Transform itemUp, itemDown, itemLeft, itemRight;
    public Transform border;
    void Update(){
        border.gameObject.SetActive(active);
    }
}
