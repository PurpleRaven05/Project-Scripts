using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderSlate : MonoBehaviour
{
    public bool activated;
    private float height;
    private float height2;
    private float maxSpeed = 5f, time = 0.5f;
    
    // Start is called before the first frame update
    void Start(){
        activated = false;
        height = transform.position.y;
        height2 = transform.position.y -0.2f;
    }
    public void Deactivate(){
        activated = false;
    }
    public void Activate(){
        activated = true;
    }
    public void Update(){
        if(activated){
            if(transform.position.y >= height2){
                LowerPlate();
            }
        }
    }
    private void LowerPlate(){
        Vector3 a = transform.position;
        Vector3 b = new Vector3(transform.position.x, height2, transform.position.z);
        transform.position = Vector3.MoveTowards(a, Vector3.Lerp(a,b,time), maxSpeed);
    }
}
