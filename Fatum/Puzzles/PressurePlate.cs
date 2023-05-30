using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    public bool activated;
    private float height;
    private float height2;
    private float maxSpeed = 5f, time = 0.5f;
    public bool Autolowering;
    public AudioSource source;

    
    // Start is called before the first frame update
    void Start()
    {
       activated = false; 
       height = transform.position.y;
       height2 = transform.position.y -0.2f;
    }
    private void OnCollisionEnter(Collision collision){
        if(collision.collider.tag == "Player"){
            if(!activated){
                activated = true;
                source.Play();
            }
                
        }

    }
    private void OnCollisionExit(Collision collision){
        if(collision.collider.tag == "Player"){
            if(Autolowering){
                if(activated){
                    activated = false;
                    source.Play();
                }
            }
            
                
        }

    }

    private void OnTriggerExit(Collider other){
        if(other.tag == "Player"){
            if(Autolowering){
                if(activated){
                    activated = false;
                    source.Play();
                }
            }
            
                
        }

    }
    void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player") {
			if(!activated){
                activated = true;
                source.Play();
            }
		}
	}
    private void LowerPlate(){
        Vector3 a = transform.position;
        Vector3 b = new Vector3(transform.position.x, height2, transform.position.z);
        transform.position = Vector3.MoveTowards(a, Vector3.Lerp(a,b,time), maxSpeed);
    }
    public void ElevatePlate(){
        Vector3 a = transform.position;
        Vector3 b = new Vector3(transform.position.x, height, transform.position.z);
        transform.position = Vector3.MoveTowards(a, Vector3.Lerp(a,b,time), maxSpeed);
    }
    void Update(){
        if(activated){
            if(transform.position.y >= height2){
                LowerPlate();
                
            }
        }
        else{
            if(transform.position.y <= height){
                ElevatePlate();
                
            }
        }
    }
}
