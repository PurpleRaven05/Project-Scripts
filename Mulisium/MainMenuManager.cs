using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public CanvasTransition canvasTransition;

    public GameObject mainButtonsPanel;
    public GameObject creditsPanel;

    public void OnPressPlay()
    {
        canvasTransition.StartFadeOut();

    }

    public void OnPressCredits()
    {
        creditsPanel.SetActive(true);
        mainButtonsPanel.SetActive(false);
    }
    public void OnGoBackCredits()
    {
        creditsPanel.SetActive(false);
        mainButtonsPanel.SetActive(true);
    }
    public void OnPressExit()
    {
        Application.Quit();
    }
}
