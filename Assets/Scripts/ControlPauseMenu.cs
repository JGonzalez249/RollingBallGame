using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControlPauseMenu : MonoBehaviour
{
    public Button currentButton;

    public GameObject Indicator;

    //Pause Menu
    public Button button1;
    public Button button2;
    public Button button3;

    //Controls Menu
    public Button controlBackButton;

    public bool controlPauseMenu;

    public int numOfButtons;
    public int buttonSelection;

    void Awake()
    {
        Indicator = GameObject.Find("Indicator");
        if (Indicator == null)
        {
            Debug.Log("Can't find Indicator");
        }
        //Indicator.transform.position = new Vector3(button1.transform.position.x - 150, button1.transform.position.y, 0); // put indicator on first button to begin
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
                    Indicator.transform.position = new Vector3(button1.transform.position.x / 2.5f, button1.transform.position.y, 0);
                }
                if (buttonSelection == 2) // button 2 hover
                {
                    button2.Select();
                    currentButton = button2;
                    Indicator.transform.position = new Vector3(button2.transform.position.x / 2.5f, button2.transform.position.y, 0);
                }
                if (buttonSelection == 3) // button 3 hover
                {
                    button3.Select();
                    currentButton = button3;
                    Indicator.transform.position = new Vector3(button3.transform.position.x / 2.5f, button3.transform.position.y, 0);
                }
            }
            else if (GameObject.Find("Canvas").GetComponent<PauseMenu>().inControlsMenu == true) // if Control Menu is enabled
            {
                numOfButtons = 1;

                controlBackButton.Select();
                currentButton = controlBackButton;
                Indicator.transform.position = new Vector3(controlBackButton.transform.position.x / 1.3f, controlBackButton.transform.position.y, 0);
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
