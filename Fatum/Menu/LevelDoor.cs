using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDoor : MonoBehaviour
{
    public int doorType;
    // Start is called before the first frame update
    public void OnTriggerEnter(Collider collision){
        if(collision.tag == "Player"){
            switch (doorType){
                case 0:
                MenuManager.LoadSecondLevel();
                break;
                case 1:
                MenuManager.LoadThirdLevel();
                break;
                case 2:
                MenuManager.LoadBossLevel();
                break;
                case 3:
                MenuManager.LoadFirstLevel();
                break;
                case 4:
                MenuManager.LoadFinalScreen();
                break;
                default:
                break;
            }
        }
    }
}
