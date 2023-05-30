using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShroomLight : MonoBehaviour
{
    public Light light;
    public Color colorA;
    public Color colorB;
    public float timePassed, timeToReturnToColorA =300f;
    public bool playerNear;
    // Start is called before the first frame update
    void Start()
    {
        playerNear = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(playerNear){
            if(timePassed >= timeToReturnToColorA){
                timePassed = 0;
                playerNear = false;
                light.color = colorA;
            }
            else{
                timePassed+= Time.deltaTime;
                light.color = colorB;
            }
        }
        
    }
    public void OnTriggerEnter(Collider other){
        if(other.tag == "Player")
            playerNear = true;
        
    }
}
