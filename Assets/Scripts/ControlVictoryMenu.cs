using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControlVictoryMenu : MonoBehaviour
{
    public Button currentButton;

    private GameObject Indicator;

    //Pause Menu
    public Button button1;
    public Button button2;
    public Button button3;

    //Credits Menu
    public Button creditsBackButton;

    public bool controlVictoryMenu;

    public int numOfButtons;
    public int buttonSelection;

    void Start()
    {
        Indicator = GameObject.Find("Indicator");
        if (Indicator == null)
        {
            Debug.Log("Can't find Indicator");
        }
        controlVictoryMenu = true;
        buttonSelection = 1;
    }
    public void Update()
    {
        if (controlVictoryMenu == true)
        {
            if (GameObject.Find("Canvas").GetComponent<VictoryMenu>().inVictoryMenu == true) // if Pause Menu is enabled
            {
                numOfButtons = 3;

                if (buttonSelection == 1) // button 1 hover
                {
                    button1.Select();
                    currentButton = button1;
                    Indicator.transform.position = new Vector3(button1.transform.position.x / 1.5f, button1.transform.position.y, 0);
                }
                if (buttonSelection == 2) // button 2 hover
                {
                    button2.Select();
                    currentButton = button2;
                    Indicator.transform.position = new Vector3(button2.transform.position.x / 1.5f, button2.transform.position.y, 0);
                }
                if (buttonSelection == 3) // button 3 hover
                {
                    button3.Select();
                    currentButton = button3;
                    Indicator.transform.position = new Vector3(button3.transform.position.x / 1.5f, button3.transform.position.y, 0);
                }
            }
            else if (GameObject.Find("Canvas").GetComponent<VictoryMenu>().inCreditsMenu == true) // if Control Menu is enabled
            {
                numOfButtons = 1;

                creditsBackButton.Select();
                currentButton = creditsBackButton;
                Indicator.transform.position = new Vector3(creditsBackButton.transform.position.x / 1.5f, creditsBackButton.transform.position.y, 0);
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
        if (controlVictoryMenu == true)
        {
            yield return buttonSelection--;
        }
    }

    private IEnumerator OnDown(InputValue value) // down
    {
        if (controlVictoryMenu == true)
        {
            yield return buttonSelection++;
        }
    }
}
