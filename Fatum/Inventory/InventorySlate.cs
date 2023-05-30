using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySlate : MonoBehaviour
{
    public float id;
    public float coordX;
    public float coordY;
    public float coordZ;
    public GameManager manager;
    private bool GameSaved;

    void Start(){
        coordX = transform.position.x;
        coordY = transform.position.y;
        coordZ = transform.position.z;
        GameSaved = false;
    }
    private void OnCollisionEnter(Collision collision){
        if(collision.collider.tag == "Player"){
            manager.actualSlate = this;
            if(!GameSaved){
                manager.SaveGameData();
                GameSaved = true;
            }
        }

    }
    private void OnCollisionExit(Collision collision){
        if(collision.collider.tag == "Player"){
            if(GameSaved){
                GameSaved = false;
            }
        }
    }
    private void OnTriggerEnter(Collider collision){
        if(collision.tag == "Player"){
            manager.actualSlate = this;
            if(!GameSaved){
                manager.SaveGameData();
                GameSaved = true;
            }
            
        }

    }
    private void OnTriggerExit(Collider collision){
        if(collision.tag == "Player"){
            if(GameSaved){
                GameSaved = false;
            }
            
        }
    }
}
