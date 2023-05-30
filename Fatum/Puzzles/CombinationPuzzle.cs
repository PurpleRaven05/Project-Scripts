using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombinationPuzzle : MonoBehaviour
{
    // Start is called before the first frame update
    public PressurePlate plate1, plate2, plate3;
    public List<Transform> platform;
    public List<float> movingDistances1;
    public List<float> movingDistances2;
    public int puzzleType;
    public float timer, movingDistance1, movingDistance2, lowerDistance, modifier;
    public float speed =5f, timeToMove = 2f;
    private bool allPressurePlatesPressed;
    // Start is called before the first frame update
    void Start()
    {
        allPressurePlatesPressed = false;
        switch(puzzleType){
            case 0:
                movingDistance1 = platform[0].position.y;
                movingDistance2 = platform[0].position.y -lowerDistance*modifier;
            break;
            case 1:
                movingDistance1 = platform[0].position.x;
                movingDistance2 = platform[0].position.x -lowerDistance*modifier;
            break;
            case 2:
                movingDistance1 = platform[0].position.z;
                movingDistance2 = platform[0].position.z -lowerDistance*modifier;
            break;
            default:
            break;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        switch(puzzleType){
            case 0:
            if(!allPressurePlatesPressed){
                CheckAllPlates();
                
            }
            else{
                if(platform[0].position.y >= movingDistance2){
                    LowerDoor();
                }
            }
            break;
            case 1:
                if(!allPressurePlatesPressed){
                    CheckAllPlates();
                }
                else{
                    if(platform[0].position.x >= movingDistance2){
                        LowerDoor();
                    }
                }
            break;
            case 2:
                if(!allPressurePlatesPressed){
                    CheckAllPlates();
                }
                else{
                    if(platform[0].position.z >= movingDistance2){
                        LowerDoor();
                    }
                }
            break;
            default:
            break;
        }
    }
    private void CheckAllPlates(){
        
        allPressurePlatesPressed  = plate1.activated && plate2.activated && plate3.activated;
        
    }
    private void LowerDoor(){
         switch(puzzleType){
            case 0:
                Vector3 a = platform[0].position;
                Vector3 b = new Vector3(platform[0].position.x, movingDistance2, platform[0].position.z);
                platform[0].position = Vector3.MoveTowards(a, Vector3.Lerp(a,b,timeToMove), speed);
            break;
            case 1:
                Vector3 c = platform[0].position;
                Vector3 d = new Vector3(movingDistance2, platform[0].position.y, platform[0].position.z);
                platform[0].position = Vector3.MoveTowards(c, Vector3.Lerp(c,d,timeToMove), speed);
            break;
            case 2:
                Vector3 e = platform[0].position;
                Vector3 f = new Vector3(platform[0].position.x, platform[0].position.y, movingDistance2);
                platform[0].position = Vector3.MoveTowards(e, Vector3.Lerp(e,f,timeToMove), speed);
            break;
            default:
            break;
        }
    }
}
