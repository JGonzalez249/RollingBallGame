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

    private GameObject Indicator;

    public Scene currentScene;
    public string sceneName;

    public bool controlMenu;

    public int numOfButtons;
    public int buttonSelection;

    void Start()
    {
        Indicator = GameObject.Find("Indicator");
        if (Indicator == null)
        {
            Debug.Log("Can't find Indicator");
        }
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
            Indicator.transform.position = new Vector3(Lvl1.transform.position.x / 1.5f, Lvl1.transform.position.y, 0);
        }
        if (buttonSelection == 2 && GameObject.Find("LevelCount").GetComponent<LevelCount>().levelCount < 1) // button 2 hover - Back
        {
            Back.Select();
            currentButton = Back;
            Indicator.transform.position = new Vector3(Back.transform.position.x / 1.5f, Back.transform.position.y, 0);
        }
        if (buttonSelection == 2 && GameObject.Find("LevelCount").GetComponent<LevelCount>().levelCount >= 1) // button 2 hover - lvl 2 with lvl 2 active
        {
            Lvl2.Select();
            currentButton = Lvl2;
            Indicator.transform.position = new Vector3(Lvl2.transform.position.x / 1.5f, Lvl2.transform.position.y, 0);
        }
        if (buttonSelection == 3 && GameObject.Find("LevelCount").GetComponent<LevelCount>().levelCount >= 1) // button 3 hover - Back with lvl 2 active
        {
            Back.Select();
            currentButton = Back;
            Indicator.transform.position = new Vector3(Back.transform.position.x / 1.5f, Back.transform.position.y, 0);
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
