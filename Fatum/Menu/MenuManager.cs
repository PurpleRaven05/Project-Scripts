using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public static void ExitGame(){
        Application.Quit();
    }
    public static void LoadSpecificScene(string sceneToLoad){
        SceneManager.LoadScene(sceneToLoad);
    }
    public static void LoadFirstLevel(){
        SceneManager.LoadScene("TutoLevel1");
    }
    public static void LoadSecondLevel(){
        SceneManager.LoadScene("TutoLevel2");
    }
    public static void LoadThirdLevel(){
        SceneManager.LoadScene("TutoLevel3");
    }
    public static void LoadBossLevel(){
        SceneManager.LoadScene("TutoBoss");
    }
    public static void ReturnToMenu(){
        SceneManager.LoadScene("Main Menu");
    }
    public static void LoadFinalScreen(){
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene("FinalScene");
    }
}
