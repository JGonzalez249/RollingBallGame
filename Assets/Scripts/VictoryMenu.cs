using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class VictoryMenu : MonoBehaviour
{

    public GameObject VictoryMenuObj;
    public GameObject CreditsObj;

    public bool inVictoryMenu;
    public bool inCreditsMenu;

    private void Start()
    {
        VictoryMenuObj = GameObject.Find("VictoryMenu");
        CreditsObj = GameObject.Find("Credits");

        VictoryMenuObj.SetActive(true);
        CreditsObj.SetActive(false);

        inVictoryMenu = true;
        inCreditsMenu = false;
        inCreditsMenu = false;
    }

    public void Menu()
    {
        SceneManager.LoadScene("Main_Menu");
    }

    public void QuitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }

    public void Credits()
    {
        VictoryMenuObj.SetActive(false);
        CreditsObj.SetActive(true);

        inVictoryMenu = false;
        inCreditsMenu = true;
    }

    public void Back()
    {
        VictoryMenuObj.SetActive(true);
        CreditsObj.SetActive(false);

        inVictoryMenu = true;
        inCreditsMenu = false;
    }
}