using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    public bool gameIsPaused;
    public GameObject pauseMenuUI;

    private void Start()
    {
        gameIsPaused = false;
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
    }

    private IEnumerator OnPause(InputValue value) // Pause
    {
        if (gameIsPaused == false) // if unpaused
        {
            pauseMenuUI.SetActive(true);
            Time.timeScale = 0f;
            yield return gameIsPaused = true;
        }
        else if (gameIsPaused == true) // if paused
        {
            pauseMenuUI.SetActive(false);
            Time.timeScale = 1f;
            yield return gameIsPaused = false;
        }
    }
}