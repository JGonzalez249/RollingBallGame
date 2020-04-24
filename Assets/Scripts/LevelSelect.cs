using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
    public GameObject locked;
    // Start is called before the first frame update
    private void Start()
    {
        locked = GameObject.Find("Lvl2Lock");
        if (locked == null)
        {
            Debug.Log("Cannot find 'Level Count', Did you start from the main menu?");
        }
    }

    private void Update()
    {
        if (GameObject.Find("LevelCount").GetComponent<LevelCount>().levelCount >= 1)
        {
            locked.SetActive(false);
        }
    }
    public void Level_1()
    {
        SceneManager.LoadScene("Level_1");
    }
    public void Level_2()
    {
        SceneManager.LoadScene("Level_2");
    }

    public void Back()
    {
        SceneManager.LoadScene("Main_Menu");
    }

}
