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
        if (gameIsPaused == true) // unpause
        {
            pauseMenuUI.SetActive(false);
            Time.timeScale = 1f;
        }
        if (gameIsPaused == false) // pause
        {
            pauseMenuUI.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    private IEnumerator OnPause(InputValue value)
    {
        if (gameIsPaused == true) // unpause
        {
            yield return gameIsPaused = false;
            Debug.Log("unpause");
            yield return new WaitForSeconds(1f);
        }
        if (gameIsPaused == false) // pause
        {
            yield return gameIsPaused = true;
            Debug.Log("pause");
            yield return new WaitForSeconds(1f);
        }
    }
}
