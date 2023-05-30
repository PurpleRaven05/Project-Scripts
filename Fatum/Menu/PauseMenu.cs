using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseCanvas;
    public bool paused;
    public  bool Gamepaused;
    public List<Transform> buttons;
    public Transform actualSelected;
    public bool canMove, delayMovePassed;
    private float timeDelay, delayOnMoving = 0.2f;

    void Awake(){
        actualSelected = buttons[0];
        actualSelected.GetComponent<ButtonNavigator>().active = true;
        pauseCanvas.SetActive(false);
        paused = false;
    }
    void Update(){
        Gamepaused = paused;
        canMove = paused;
        if(Input.GetKeyDown(KeyCode.Escape)){
            Cursor.lockState = CursorLockMode.Locked;
            UnityEngine.Debug.Log("PauseMenu");
            if(!paused){
                Pause();
            }
            else{
                Resume();
            }
        }
        if(canMove && delayMovePassed){
            if(Input.GetKeyDown(KeyCode.W)){
                MoveUp();
                delayMovePassed = false;
            }
            if(Input.GetKeyDown(KeyCode.A)){
                MoveLeft();
                delayMovePassed = false;
            }
            if(Input.GetKeyDown(KeyCode.S)){
                MoveDown();
                delayMovePassed = false;
            }
            if(Input.GetKeyDown(KeyCode.D)){
                MoveRight();
                delayMovePassed = false;
            }
            if(Input.GetKeyDown(KeyCode.Space)){
                UseAction();
            }
        }
        if(!delayMovePassed){
            if(delayOnMoving <= timeDelay){
                timeDelay = 0f;
                delayMovePassed = true;
            }
            else{
                timeDelay+= Time.fixedDeltaTime;
            }
        }
    }

    public void MoveLeft(){
        actualSelected.GetComponent<ButtonNavigator>().active = false;
        actualSelected = actualSelected.GetComponent<ButtonNavigator>().itemLeft;
        actualSelected.GetComponent<ButtonNavigator>().active = true;

    }
    public void MoveRight(){
        actualSelected.GetComponent<ButtonNavigator>().active = false;
        actualSelected = actualSelected.GetComponent<ButtonNavigator>().itemRight;
        actualSelected.GetComponent<ButtonNavigator>().active = true;
        
    }
    public void MoveUp(){
        actualSelected.GetComponent<ButtonNavigator>().active = false;
        actualSelected = actualSelected.GetComponent<ButtonNavigator>().itemUp;
        actualSelected.GetComponent<ButtonNavigator>().active = true;  
    }
    public void MoveDown(){
        actualSelected.GetComponent<ButtonNavigator>().active = false;
        actualSelected = actualSelected.GetComponent<ButtonNavigator>().itemDown;
        actualSelected.GetComponent<ButtonNavigator>().active = true;
    }
    public void Resume(){
        pauseCanvas.SetActive(false);
        paused = false;
        Time.timeScale = 1f;
        
    }
    public void Pause(){
        pauseCanvas.SetActive(true);
        paused = true;
        Time.timeScale = 0f;
    }
    public void ReturnToMenu(){
        Time.timeScale = 1f;
        paused = false;
        MenuManager.ReturnToMenu();
    }
    public void ExitGame(){
        Time.timeScale = 1f;
        paused = false;
        MenuManager.ExitGame();
    }
    public void UseAction(){
        switch(actualSelected.GetComponent<ButtonNavigator>().type){
            case 0:
            Resume();
            break;
            case 1:
            ReturnToMenu();
            break;
            case 2:
            ExitGame();
            break;
            default:
            Resume();
            break;
        }
    }
}
