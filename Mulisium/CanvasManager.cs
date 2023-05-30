using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CanvasManager : MonoBehaviour
{
    public Image crosshair;
    public Image imageDescription;
    public TextMeshProUGUI textDescription;
    public GameObject pauseMenu;
    public GameObject interactButton;

    private void OnEnable()
    {
        EventManager.OnChangeDescription += ChangeDescription;
        EventManager.OnDisableDescription += DisableDescription;
        EventManager.OnChangePauseMenu += ChangePauseMenu;
        EventManager.OnShowInteractButton += ShowInteractButton;
    }
    private void OnDisable()
    {
        EventManager.OnChangeDescription -= ChangeDescription;
        EventManager.OnDisableDescription -= DisableDescription;
        EventManager.OnChangePauseMenu -= ChangePauseMenu;
        EventManager.OnShowInteractButton -= ShowInteractButton;
    }

    private void Awake()
    {
        
        crosshair.enabled = true;
        imageDescription.enabled = false;
        textDescription.enabled = false;
    }

    void ChangeDescription(string descriptionText)
    {
        if (imageDescription.enabled)
            DisableDescription();
        else
            EnableDescription(descriptionText);
    }

    void DisableDescription()
    {
        crosshair.enabled = true;
        imageDescription.enabled = false;
        textDescription.enabled = false;
    }
    void EnableDescription(string descriptionText)
    {
        textDescription.text = descriptionText;

        crosshair.enabled = false;
        imageDescription.enabled = true;
        textDescription.enabled = true;
    }

    void ChangePauseMenu()
    {
        pauseMenu.SetActive(!pauseMenu.activeSelf);

        if (pauseMenu.activeSelf)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            EventManager.OnDisablePlayerMovement?.Invoke();
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            EventManager.OnEnablePlayerMovement?.Invoke();
        }
    }

    void ShowInteractButton(bool stateInteract)
    {
        if (stateInteract)
        {
            interactButton.SetActive(true);
        }
        else
        {
            interactButton.SetActive(false);
        }
    }

    #region Pause menu Button Methods

    public void OnResumeButton()
    {
        ChangePauseMenu();
    }
    public void OnExitMenuButton()
    {
        SceneManager.LoadScene(0);
    }
    #endregion
}
