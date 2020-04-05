using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    
    public GameObject pauseMenuUI;

    public GameObject PauseMenuObj;
    public GameObject ControlsObj;

    public bool inPauseMenu;
    public bool inControlsMenu;

    public bool gameIsPaused;

    private void Start()
    {
        gameIsPaused = false;
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
    }

    private IEnumerator OnPause(InputValue value) // Pause
    {
        Debug.Log("pause");
        if (gameIsPaused == false) // if unpaused
        {
            pauseMenuUI.SetActive(true);
            Time.timeScale = 0f;
            inPauseMenu = true;
            yield return gameIsPaused = true;
            yield return GameObject.Find("EventSystem").GetComponent<ControlPauseMenu>().controlPauseMenu = true; // allowing to control menu
        }
    }

    public void ResumeGame()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        inPauseMenu = false;
        gameIsPaused = false;
        GameObject.Find("EventSystem").GetComponent<ControlPauseMenu>().controlPauseMenu = false; // disable controlling menu
    }

    public void Menu()
    {
        SceneManager.LoadScene("Main_Menu");
    }

    public void Controls()
    {
        PauseMenuObj.SetActive(false);
        ControlsObj.SetActive(true);

        inPauseMenu = false;
        inControlsMenu = true;
    }

    public void Back()
    {
        PauseMenuObj.SetActive(true);
        ControlsObj.SetActive(false);

        inPauseMenu = true;
        inControlsMenu = false;
    }
}