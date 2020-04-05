using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControlPauseMenu : MonoBehaviour
{
    public Button currentButton;
    //Pause Menu
    public Button button1;
    public Button button2;
    public Button button3;

    //Controls Menu
    public Button controlBackButton;

    public bool controlPauseMenu;

    public int numOfButtons;
    public int buttonSelection;

    void Start()
    {
        controlPauseMenu = false;
        buttonSelection = 1;
    }
    public void Update()
    {
        if (controlPauseMenu == true)
        {
            if (GameObject.Find("Canvas").GetComponent<PauseMenu>().inPauseMenu == true) // if Pause Menu is enabled
            {
                numOfButtons = 3;

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
            }
            else if (GameObject.Find("Canvas").GetComponent<PauseMenu>().inControlsMenu == true) // if Control Menu is enabled
            {
                numOfButtons = 1;

                controlBackButton.Select();
                currentButton = controlBackButton;
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
        if (controlPauseMenu == true)
        {
            yield return buttonSelection--;
        }
    }

    private IEnumerator OnDown(InputValue value) // down
    {
        if (controlPauseMenu == true)
        {
            yield return buttonSelection++;
        }
    }
}
