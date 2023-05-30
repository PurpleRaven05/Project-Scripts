using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class simple_rotation : MonoBehaviour
{
    // Start is called before the first frame update
     //Rotational Speed
    public float speed = 0f;
    //Forward Direction
    public bool Forward = false;
   
    void Update ()
    {
        //Forward Direction
        if(Forward == true)
        {
            transform.Rotate(0, 0, Time.deltaTime * speed, Space.Self);
        }
        //Reverse Direction
        else
        {
            transform.Rotate(0, 0, -Time.deltaTime * speed, Space.Self);
        }
       
    }
}
