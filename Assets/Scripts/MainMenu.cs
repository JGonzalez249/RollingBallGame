using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    public GameObject MainMenuObj;
    public GameObject ControlsObj;
    public GameObject CreditsObj;

    public bool inMainMenu;
    public bool inControlsMenu;
    public bool inCreditsMenu;

    private void Start()
    {
        MainMenuObj = GameObject.Find("MainMenu");
        ControlsObj = GameObject.Find("Controls");
        CreditsObj = GameObject.Find("Credits");

        MainMenuObj.SetActive(true);
        ControlsObj.SetActive(false);
        CreditsObj.SetActive(false);

        inMainMenu = true;
        inControlsMenu = false;
        inCreditsMenu = false;
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }

    public void Menu()
    {
        SceneManager.LoadScene("Main_Menu");
    }

    public void Controls()
    {
        MainMenuObj.SetActive(false);
        ControlsObj.SetActive(true);
        CreditsObj.SetActive(false);

        inMainMenu = false;
        inControlsMenu = true;
        inCreditsMenu = false;
    }

    public void Credits()
    {
        MainMenuObj.SetActive(false);
        ControlsObj.SetActive(false);
        CreditsObj.SetActive(true);

        inMainMenu = false;
        inControlsMenu = false;
        inCreditsMenu = true;
    }

    public void Back()
    {
        MainMenuObj.SetActive(true);
        ControlsObj.SetActive(false);
        CreditsObj.SetActive(false);

        inMainMenu = true;
        inControlsMenu = false;
        inCreditsMenu = false;
    }
}
