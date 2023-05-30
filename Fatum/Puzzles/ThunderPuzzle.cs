using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderPuzzle : MonoBehaviour
{
    // Start is called before the first frame update
    public List<ThunderSlate> puzzleSlates;
    public List<Transform> platforms;
    public List<MirrorWall> walls;
    public List<float> movingDistances1;
    public List<float> movingDistances2;
    public List<float> lowerdistances;
    public List<float> modifiers;
    public List<int> movingType;
    public AudioSource source;
    public float speed =0.02f, timeToMove = 20f;
    private bool allPressurePlatesPressed, broken, audioPlayed = false;


    void Start()
    {
        allPressurePlatesPressed = false;
        broken = false;
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
    void FixedUpdate()
    {
        if(!broken){
            CheckAllWalls();
        }
        if(!allPressurePlatesPressed){
            CheckAllPlates();
                
        }
        else{
            if(!broken){
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
        else{
            for(int i = 0; i < platforms.Count; i++){
            switch(movingType[i]){
            case 0:
                if(platforms[i].position.y <= movingDistances1[i])
                    ReturnDoor(i);
                    
            break;
            case 1:
                if(platforms[i].position.x <= movingDistances1[i])
                    ReturnDoor(i);
             break;
            case 2:
                if(platforms[i].position.z <= movingDistances1[i])
                    ReturnDoor(i);
            break;
            case 3:
                if(platforms[i].position.y >= movingDistances1[i])
                    ReturnDoor(i);
                    
            break;
            case 4:
                if(platforms[i].position.x >= movingDistances1[i])
                    ReturnDoor(i);
            break;
            case 5:
                if(platforms[i].position.z >= movingDistances1[i])
                    ReturnDoor(i);
            break;
            default:
            break;
            }
            } 
        }
        }
    }
    private void CheckAllPlates(){
        
        for(int i = 0; i< puzzleSlates.Count;i++){
            if(puzzleSlates[i].activated == false){
                allPressurePlatesPressed = false;
                return;
            }
        }
        allPressurePlatesPressed = true;  

            if(!audioPlayed){
                source.Play();
                audioPlayed = true;
            }
    }
    private void CheckAllWalls(){
        
        for(int i = 0; i< walls.Count;i++){
            if(walls[i].hitRecieved == true){
                broken = true;
                source.Play();
                return;
            }
        }
        broken = false;  
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
    private void ReturnDoor(int id){

        switch(movingType[id]){
            case 0:
            case 3:
                Vector3 a = platforms[id].position;
                Vector3 b = new Vector3(platforms[id].position.x, movingDistances1[id], platforms[id].position.z);
                platforms[id].position = Vector3.MoveTowards(a, Vector3.Lerp(a,b,timeToMove), speed);
            break;
            case 1:
            case 4:
                Vector3 c = platforms[id].position;
                Vector3 d = new Vector3(movingDistances1[id], platforms[id].position.y, platforms[id].position.z);
                platforms[id].position = Vector3.MoveTowards(c, Vector3.Lerp(c,d,timeToMove), speed);
            break;
            case 2:
            case 5:
                Vector3 e = platforms[id].position;
                Vector3 f = new Vector3(platforms[id].position.x, platforms[id].position.y, movingDistances1[id]);
                platforms[id].position = Vector3.MoveTowards(e, Vector3.Lerp(e,f,timeToMove), speed);
            break;
            default:
            break;
        }
        
    }
}
