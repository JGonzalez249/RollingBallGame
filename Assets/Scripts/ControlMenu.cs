using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class ControlMenu : MonoBehaviour
{
    public Button currentButton;

    private GameObject Indicator;

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
        Indicator = GameObject.Find("Indicator");
        if (Indicator == null)
        {
            Debug.Log("Can't find Indicator");
        }
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
                    Indicator.transform.position = new Vector3(button1.transform.position.x - 150, button1.transform.position.y, 0);
                }
                if (buttonSelection == 2) // button 2 hover
                {
                    button2.Select();
                    currentButton = button2;
                    Indicator.transform.position = new Vector3(button2.transform.position.x - 150, button2.transform.position.y, 0);
                }
                if (buttonSelection == 3) // button 3 hover
                {
                    button3.Select();
                    currentButton = button3;
                    Indicator.transform.position = new Vector3(button3.transform.position.x - 150, button3.transform.position.y, 0);
                }
                if (buttonSelection == 4) // button 4 hover
                {
                    button4.Select();
                    currentButton = button4;
                    Indicator.transform.position = new Vector3(button4.transform.position.x - 150, button4.transform.position.y, 0);
                }
            }
            else if (GameObject.Find("Canvas").GetComponent<MainMenu>().inControlsMenu == true) // if Control Menu is enabled
            {
                numOfButtons = 1;

                controlBackButton.Select();
                currentButton = controlBackButton;
                Indicator.transform.position = new Vector3(controlBackButton.transform.position.x - 100, controlBackButton.transform.position.y, 0);
            }
            else if (GameObject.Find("Canvas").GetComponent<MainMenu>().inCreditsMenu == true) // if Credits Menu is enabled
            {
                numOfButtons = 1;

                creditsBackButton.Select();
                currentButton = creditsBackButton;
                Indicator.transform.position = new Vector3(creditsBackButton.transform.position.x - 150, creditsBackButton.transform.position.y, 0);
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
