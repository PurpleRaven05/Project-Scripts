using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameManager : MonoBehaviour
{
    public PlacePickedObject[] placeObjectsArray;
    public bool gameCompleted;

    private void Start(){
        gameCompleted = false;
    }

    private void OnEnable()
    {
        EventManager.OnPlaceAppropiatedObject += CheckGameCompleted;
    }
    private void OnDisable()
    {
        EventManager.OnPlaceAppropiatedObject -= CheckGameCompleted;
    }
    private void OnTriggerEnter(Collider other)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
    void CheckGameCompleted()
    {
        int count = 0;

        foreach(PlacePickedObject ppo in placeObjectsArray)
            if (ppo.hasCorrectObject) count++;

        if (count == placeObjectsArray.Length)
        {
            //Activate something
            Debug.Log("Completed");
            gameCompleted = true;
        }
    }
}
