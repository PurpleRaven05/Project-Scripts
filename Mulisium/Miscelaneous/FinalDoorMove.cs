using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalDoorMove : MonoBehaviour
{
    public EndGameManager manager;
    public float finaPosition;
    public Transform door;
    [SerializeField] private float timeToMove;
    [SerializeField] private float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(manager.gameCompleted){
            if(door.position.y >= finaPosition){
                Vector3 a = door.position;
                Vector3 b = new Vector3(door.position.x, finaPosition, door.position.z);
                door.position = Vector3.MoveTowards(a, Vector3.Lerp(a,b,timeToMove),speed);
            }
            
        }
    }
}
