using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Music : MonoBehaviour
{
    public Scene currentScene;
    public Scene previousScene;

    public AudioSource MusicSource;
    public AudioClip MenuMusic;
    public AudioClip Lvl1Music;
    public AudioClip Lvl2Music;
    public AudioClip VictoryMusic;

    private bool playmain;
    private static Music Instance;

    void Awake()
    {
        playmain = true;
        DontDestroyOnLoad(this);

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Object.Destroy(this.gameObject);
        }
    }
    private void Update()
    {
        currentScene = SceneManager.GetActiveScene(); // set currentscene
        string currentSceneName = currentScene.name;
        string previousSceneName = previousScene.name;

        if (currentScene.name != previousScene.name) // perform once when a scene change happens
        {
            if (currentSceneName == "Main_Menu" && playmain == true) // main menu and level select - doesn't repeat song if go from Level select to Main Menu
            {
                MusicSource.Stop();
                MusicSource.clip = MenuMusic;
                MusicSource.loop = true;
                MusicSource.pitch = 1;
                MusicSource.Play();
                previousScene = currentScene;
                playmain = false;
            }
            if (currentSceneName == "LevelSelect") // Level Select - don't play different music - DOESN'T WORK
            {
                previousScene = currentScene;
            }
                if (currentSceneName == "Level_1") // Level 1
            {
                MusicSource.Stop();
                MusicSource.clip = Lvl1Music;
                MusicSource.loop = true;
                MusicSource.pitch = 0.8f;
                MusicSource.Play();
                previousScene = currentScene;
                playmain = true;
            }
            if (currentSceneName == "Level_2") // Level 2
            {
                MusicSource.Stop();
                MusicSource.clip = Lvl2Music;
                MusicSource.loop = true;
                MusicSource.pitch = 0.8f;
                MusicSource.Play();
                previousScene = currentScene;
                playmain = true;
            }
            if (currentSceneName == "Victory_Menu") // VictoryMenu
            {
                MusicSource.Stop();
                MusicSource.clip = VictoryMusic;
                MusicSource.loop = true;
                MusicSource.pitch = 0.8f;
                MusicSource.Play();
                previousScene = currentScene;
                playmain = true;
            }
        }
    }
}
