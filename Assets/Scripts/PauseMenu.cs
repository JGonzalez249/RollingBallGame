using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    public  bool gameIsPaused;
    public GameObject pauseMenuUI;

    private void Start()
    {
        gameIsPaused = false;
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
    }

    private void Update()
    {
        if (Gamepad.current.startButton.wasPressedThisFrame || Keyboard.current.escapeKey.wasPressedThisFrame)
        {
                if (gameIsPaused == true) // unpause
            {
                pauseMenuUI.SetActive(false);
                Time.timeScale = 1f;
                gameIsPaused = false;
                Debug.Log("unpause");
            }
            else if (gameIsPaused == false) // pause
            {
                pauseMenuUI.SetActive(true);
                Time.timeScale = 0f;
                gameIsPaused = true;
                Debug.Log("pause");
            }
        }
    }
}
