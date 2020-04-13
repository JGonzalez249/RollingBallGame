using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class ControlMenu : MonoBehaviour
{
    public Button currentButton;

    //Main Menu
    public Button button1;
    public Button button2;
    public Button button3;
    public Button button4;

    //Controls Menu
    public Button controlBackButton;

    //Credits Menu
    public Button creditsBackButton;

    public Scene currentScene;
    public string sceneName;

    public bool controlMenu;

    public int numOfButtons;
    public int buttonSelection;

    void Start()
    {
        // Create a temporary reference to the current scene.
        currentScene = SceneManager.GetActiveScene();
        controlMenu = true;
        buttonSelection = 1;
        // Retrieve the name of this scene.
        sceneName = currentScene.name;
    }
    public void Update()
    {
        if (controlMenu == true)
        {
            if (GameObject.Find("Canvas").GetComponent<MainMenu>().inMainMenu == true) // if Main Menu is enabled
            {
                numOfButtons = 4;

                if (buttonSelection == 1) // button 1 hover
                {
                    button1.Select();
                    currentButton = button1;
                }
                if (buttonSelection == 2) // button 2 hover
                {
                    button2.Select();
                    currentButton = button2;
                }
                if (buttonSelection == 3) // button 3 hover
                {
                    button3.Select();
                    currentButton = button3;
                }
                if (buttonSelection == 4) // button 4 hover
                {
                    button4.Select();
                    currentButton = button4;
                }
            }
            else if (GameObject.Find("Canvas").GetComponent<MainMenu>().inControlsMenu == true) // if Control Menu is enabled
            {
                numOfButtons = 1;

                controlBackButton.Select();
                currentButton = controlBackButton;
            }
            else if (GameObject.Find("Canvas").GetComponent<MainMenu>().inCreditsMenu == true) // if Credits Menu is enabled
            {
                numOfButtons = 1;

                creditsBackButton.Select();
                currentButton = creditsBackButton;
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

    }

    private IEnumerator OnUp(InputValue value) // up
    {
        if (controlMenu == true)
        {
            yield return buttonSelection--;
        }
    }

    private IEnumerator OnDown(InputValue value) // down
    {
        if (controlMenu == true)
        {
            yield return buttonSelection++;
        }
    }
}
