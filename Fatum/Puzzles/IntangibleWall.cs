using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntangibleWall : MonoBehaviour
{
    public BoxCollider box;
    public float modifier;
    
    void update(){
        Vector3 halfbox = new Vector3(transform.localScale.x+modifier, transform.localScale.y+modifier, transform.localScale.z+modifier);
        Collider[] hits =Physics.OverlapBox(transform.position,halfbox);
        Physics.CheckBox(transform.position,halfbox);
        foreach(Collider hit in hits){
            if(hit.tag == "Player"){
                if(hit.GetComponent<PlayerController>().clone){
                    box.isTrigger = true;
                }
                else{
                    box.isTrigger = false;
                }

                if(hit.GetComponentInParent<PlayerController>().clone){
                    box.isTrigger = true;
                }
                else{
                    box.isTrigger = false;
                }  
            }
        }
    }
    // Start is called before the first frame update
    private void OnCollisionEnter(Collision collision){
        if(collision.collider.tag == "Player"){
            if(collision.collider.GetComponent<PlayerController>()){
                if(collision.collider.GetComponent<PlayerController>().clone)
                    box.isTrigger = true; 
            }
            if(collision.collider.GetComponentInParent<PlayerController>()){
                if(collision.collider.GetComponentInParent<PlayerController>().clone){
                    box.isTrigger = true;
                }
            }      
        }

    }
    private void OnCollisionExit(Collision collision){
        if(collision.collider.tag == "Player"){
            
                
        }

    }

    private void OnTriggerExit(Collider other){
      /* if(other.tag == "Player"){
            if(other.GetComponent<PlayerController>()){
                if(other.GetComponent<PlayerController>().clone)
                    box.isTrigger = true; 
            }
            if(other.GetComponentInParent<PlayerController>()){
                if(other.GetComponentInParent<PlayerController>().clone){
                    box.isTrigger = true;
                }
            }      
        }*/

    }
    void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Player"){
            if(other.GetComponent<PlayerController>()){
                if(!other.GetComponent<PlayerController>().clone)
                    box.isTrigger = false; 
            }
            if(other.GetComponentInParent<PlayerController>()){
                if(!other.GetComponentInParent<PlayerController>().clone){
                    box.isTrigger = false;
                }
            }      
        }
	}
}
