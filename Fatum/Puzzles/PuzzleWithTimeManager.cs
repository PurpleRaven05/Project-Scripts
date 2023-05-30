using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleWithTimeManager : MonoBehaviour
{
   public List<PressurePlate> switches;
   public PressurePlate disableSwitch;
    public Transform platform;
    public int puzzleType; //0 se mueve en y, 1 se mueve en x, 2 se mueve en z
    public float timer, movingDistance1, movingDistance2, lowerDistance, modifier;
    public float speed =5f, timeToMove = 2f, timeToReturn = 5f;
    private bool allPressurePlatesPressed, timerActive, disabled, audioPlayed = false;
    public AudioSource source;
    // Start is called before the first frame update
    void Start()
    {
        switch(puzzleType){
            case 0:
            case 3:
                movingDistance1 = platform.position.y;
                movingDistance2 = platform.position.y -lowerDistance*modifier;
            break;
            case 1:
            case 4:
                movingDistance1 = platform.position.x;
                movingDistance2 = platform.position.x -lowerDistance*modifier;
            break;
            case 2:
            case 5:
                movingDistance1 = platform.position.z;
                movingDistance2 = platform.position.z -lowerDistance*modifier;
            break;
            default:
            break;
        }
        allPressurePlatesPressed = false;
        timerActive = false;
        disabled = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        switch(puzzleType){
            case 0:
            if(!allPressurePlatesPressed){
                CheckAllPlates();
                if(platform.position.y < movingDistance1&&!disabled){
                    UnityEngine.Debug.Log("Hello");
                    ReturnDoor();
                }
            }
            else{
                if(platform.position.y >= movingDistance2){
                    LowerDoor();
                }
            }
            if(disabled){
                if(platform.position.y >= movingDistance2){
                    LowerDoor();
                }
            }
            break;
            case 1:
                if(!allPressurePlatesPressed){
                    CheckAllPlates();
                    if(platform.position.x < movingDistance1&&!disabled){
                        UnityEngine.Debug.Log("Hello");
                        ReturnDoor();
                    }
                }
                else{
                    if(platform.position.x >= movingDistance2){
                        LowerDoor();
                    }
                }
                if(disabled){
                    if(platform.position.x >= movingDistance2){
                        LowerDoor();
                }
                }
            break;
            case 2:
                if(!allPressurePlatesPressed){
                    CheckAllPlates();
                    if(platform.position.z < movingDistance1&&!disabled){
                        UnityEngine.Debug.Log("Hello");
                        ReturnDoor();
                    }
                }
                else{
                    if(platform.position.z >= movingDistance2){
                        LowerDoor();
                    }
                }
                if(disabled){
                    if(platform.position.z >= movingDistance2){
                        LowerDoor();
                    }
                }
            break;
            case 3:
            if(!allPressurePlatesPressed){
                CheckAllPlates();
                //UnityEngine.Debug.Log("Bruh");
                if(platform.position.y > movingDistance1&&!disabled){

                    ReturnDoor();
                }
            }
            else{
                //UnityEngine.Debug.Log("Bruh2");
                if(platform.position.y <= movingDistance2){
                    //UnityEngine.Debug.Log("Bruh3");
                    LowerDoor();
                }
            }
            if(disabled){
                if(platform.position.y <= movingDistance2){
                    LowerDoor();
                }
            }
            break;
            case 4:
                if(!allPressurePlatesPressed){
                    CheckAllPlates();
                    if(platform.position.x > movingDistance1&&!disabled){
                        //UnityEngine.Debug.Log("Hello");
                        ReturnDoor();
                    }
                }
                else{
                    if(platform.position.x <= movingDistance2){
                        LowerDoor();
                    }
                }
                if(disabled){
                    if(platform.position.x <= movingDistance2){
                        LowerDoor();
                }
                }
            break;
            case 5:
                if(!allPressurePlatesPressed){
                    CheckAllPlates();
                    if(platform.position.z > movingDistance1&&!disabled){
                        //UnityEngine.Debug.Log("Hello");
                        ReturnDoor();
                    }
                }
                else{
                    if(platform.position.z <= movingDistance2){
                        LowerDoor();
                    }
                }
                if(disabled){
                    if(platform.position.z <= movingDistance2){
                        LowerDoor();
                    }
                }
            break;
            default:
            break;
        }
        
        ManageTimers();
        
    }
    private void CheckAllPlates(){
        bool allPressed = true;
        foreach(PressurePlate plate in switches){
            if(plate.activated == false)
                allPressed = false;
        }
        allPressurePlatesPressed = allPressed;
        disabled = disableSwitch.activated;
        if(allPressed||disabled){
            if(!audioPlayed){
                source.Play();
                audioPlayed = true;
            }
        }
        
    }
    private void ResetAllPlates(){
        //UnityEngine.Debug.Log("Reset");
        foreach(PressurePlate plate in switches){
            if(plate.activated == true){
                plate.activated = false;
                plate.ElevatePlate();
                //UnityEngine.Debug.Log("PlateReset =" + !plate.activated);
            }
                

        }
        
    }
    private void LowerDoor(){
        switch(puzzleType){
            case 0:
            case 3:
                Vector3 a = platform.position;
                Vector3 b = new Vector3(platform.position.x, movingDistance2, platform.position.z);
                platform.position = Vector3.MoveTowards(a, Vector3.Lerp(a,b,timeToMove), speed);
            break;
            case 1:
            case 4:
                Vector3 c = platform.position;
                Vector3 d = new Vector3(movingDistance2, platform.position.y, platform.position.z);
                platform.position = Vector3.MoveTowards(c, Vector3.Lerp(c,d,timeToMove), speed);
            break;
            case 2:
            case 5:
                Vector3 e = platform.position;
                Vector3 f = new Vector3(platform.position.x, platform.position.y, movingDistance2);
                platform.position = Vector3.MoveTowards(e, Vector3.Lerp(e,f,timeToMove), speed);
            break;
            default:
            break;
        }
        
    }
    private void ReturnDoor(){
        switch(puzzleType){
            case 0:
            case 3:
                Vector3 a = platform.position;
                Vector3 b = new Vector3(platform.position.x, movingDistance1, platform.position.z);
                platform.position = Vector3.MoveTowards(a, Vector3.Lerp(a,b,timeToMove), speed);
            break;
            case 1:
            case 4:
                Vector3 c = platform.position;
                Vector3 d = new Vector3(movingDistance1, platform.position.y, platform.position.z);
                platform.position = Vector3.MoveTowards(c, Vector3.Lerp(c,d,timeToMove), speed);
            break;
            case 2:
            case 5:
                Vector3 e = platform.position;
                Vector3 f = new Vector3(platform.position.x, platform.position.y, movingDistance1);
                platform.position = Vector3.MoveTowards(e, Vector3.Lerp(e,f,timeToMove), speed);
            break;
            default:
            break;
        }
        
    }
    private void ManageTimers(){
        if(allPressurePlatesPressed){
            if(timer >= timeToReturn){
                timer = 0;
                ResetAllPlates();
                allPressurePlatesPressed = false;
                source.Play();
                audioPlayed = false;
            }
            else{
                timer += Time.deltaTime;
            }
        }
    }
}
