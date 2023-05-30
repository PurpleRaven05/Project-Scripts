using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleSlateOrder : MonoBehaviour
{
    public List<PressurePlate> plates;
    public List<Transform> platforms;
    public List<float> movingDistances1;
    public List<float> movingDistances2;
    public List<float> lowerdistances;
    public List<float> modifiers;
    public List<int> movingType;
    public List<AudioSource> sources;
    public int puzzleType;
    public float speed =5f, timeToMove = 2f;
    private bool allPressurePlatesPressed, audioPlayed = false;
    // Start is called before the first frame update
    void Start()
    {
        allPressurePlatesPressed = false;
        for(int i = 0; i < platforms.Count; i++){
            switch(movingType[i]){
            case 0:
            case 3:
                movingDistances1[i] = platforms[i].position.y;
                movingDistances2[i] = platforms[i].position.y - lowerdistances[i]*modifiers[i];
            break;
            case 1:
            case 4:
                movingDistances1[i] = platforms[i].position.x;
                movingDistances2[i] = platforms[i].position.x - lowerdistances[i]*modifiers[i];
            break;
            case 2:
            case 5:
                    movingDistances1[i] = platforms[i].position.z;
                    movingDistances2[i] = platforms[i].position.z - lowerdistances[i]*modifiers[i];
            break;
            default:
            break;
            }
        }
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(!allPressurePlatesPressed){
                CheckAllPlates();
                
            }
        else{
            for(int i = 0; i < platforms.Count; i++){
            switch(movingType[i]){
            case 0:
               if(platforms[i].position.y >= movingDistances2[i])
                    LowerDoor(i);
                    
            break;
            case 1:
                if(platforms[i].position.x >= movingDistances2[i])
                    LowerDoor(i);
            break;
            case 2:
                if(platforms[i].position.z >= movingDistances2[i])
                    LowerDoor(i);
            break;
            case 3:
               if(platforms[i].position.y <= movingDistances2[i])
                    LowerDoor(i);
                    
            break;
            case 4:
                if(platforms[i].position.x <= movingDistances2[i])
                    LowerDoor(i);
            break;
            case 5:
                if(platforms[i].position.z <= movingDistances2[i])
                    LowerDoor(i);
            break;
            default:
            break;
            }
        }
        
        }
    }
    private void CheckAllPlates(){
        
        for(int i = 0; i<plates.Count;i++){
            if(plates[i].activated == false){
                allPressurePlatesPressed = false;
                return;
            }
        }
        allPressurePlatesPressed = true;
        if(allPressurePlatesPressed){
            if(!audioPlayed){
                foreach(AudioSource audio in sources){
                    audio.Play();
                }
                audioPlayed = false;
            }       
        }
        
        
    }
    private void LowerDoor(int id){
         switch(movingType[id]){
            case 0:
            case 3:
                Vector3 a = platforms[id].position;
                Vector3 b = new Vector3(platforms[id].position.x, movingDistances2[id], platforms[id].position.z);
                platforms[id].position = Vector3.MoveTowards(a, Vector3.Lerp(a,b,timeToMove), speed);
            break;
            case 1:
            case 4:
                Vector3 c = platforms[id].position;
                Vector3 d = new Vector3(movingDistances2[id], platforms[id].position.y, platforms[id].position.z);
                platforms[id].position = Vector3.MoveTowards(c, Vector3.Lerp(c,d,timeToMove), speed);
            break;
            case 2:
            case 5:
                Vector3 e = platforms[id].position;
                Vector3 f = new Vector3(platforms[id].position.x, platforms[id].position.y, movingDistances2[id]);
                platforms[id].position = Vector3.MoveTowards(e, Vector3.Lerp(e,f,timeToMove), speed);
            break;
            default:
            break;
        }
    }
}
