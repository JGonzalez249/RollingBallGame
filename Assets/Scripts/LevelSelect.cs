using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
    // Start is called before the first frame update
    public void Level_1()
    {
        SceneManager.LoadScene("Level_1");
    }
    public void Level_2()
    {
        SceneManager.LoadScene("Level_2");
    }
    public void Level_3()
    {
        SceneManager.LoadScene("Level_3");
    }
    public void Level_4()
    {
        SceneManager.LoadScene("Level_4");
    }
}
