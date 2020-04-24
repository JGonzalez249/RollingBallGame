using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class LevelControlMenu : MonoBehaviour
{
    public Button currentButton;

    //LevelSelection
    public Button Lvl1;
    public Button Lvl2;
    public Button Back;

    public Scene currentScene;
    public string sceneName;

    public bool controlMenu;

    public int numOfButtons;
    public int buttonSelection;

    void Start()
    {
        // Create a temporary reference to the current scene.
        currentScene = SceneManager.GetActiveScene();
        buttonSelection = 1;

        // Retrieve the name of this scene.
        sceneName = currentScene.name;
    }
    public void Update()
    {
        numOfButtons = 2;
        if (GameObject.Find("LevelCount").GetComponent<LevelCount>().levelCount >= 1)
        {
            numOfButtons = 3;
        }

        if (buttonSelection == 1) // button 1 hover - lvl 1
        {
            Lvl1.Select();
            currentButton = Lvl1;
        }
        if (buttonSelection == 2 && GameObject.Find("LevelCount").GetComponent<LevelCount>().levelCount < 1) // button 2 hover - Back
        {
            Back.Select();
            currentButton = Back;
        }
        if (buttonSelection == 2 && GameObject.Find("LevelCount").GetComponent<LevelCount>().levelCount >= 1) // button 2 hover - lvl 2 with lvl 2 active
        {
            Lvl2.Select();
            currentButton = Lvl2;
        }
        if (buttonSelection == 3 && GameObject.Find("LevelCount").GetComponent<LevelCount>().levelCount >= 1) // button 3 hover - Back with lvl 2 active
        {
            Back.Select();
            currentButton = Back;
        }

        if (buttonSelection > numOfButtons) // too high
        {
            buttonSelection = 1;
        }
        if (buttonSelection < 1) // too low
        {
            buttonSelection = numOfButtons;
        }

        //confirm press
        if (Gamepad.current.buttonEast.wasPressedThisFrame || Keyboard.current.enterKey.wasPressedThisFrame)
        {
            Debug.Log("Click");
            currentButton.onClick.Invoke();
        }
    }

    private IEnumerator OnUp(InputValue value) // up
    {
        yield return buttonSelection--;
    }

    private IEnumerator OnDown(InputValue value) // down
    {
        yield return buttonSelection++;
    }
}
