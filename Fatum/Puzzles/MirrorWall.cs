using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorWall : MonoBehaviour
{
    // Start is called before the first frame update
    public bool hitRecieved;
    void Start()
    {
        hitRecieved = false;
    }

    // Update is called once per frame
    void OnCollisionEnter(Collision other){
        if(other.collider.GetComponent<TrackingBall>()){
            hitRecieved = true;
        }
    }
}
