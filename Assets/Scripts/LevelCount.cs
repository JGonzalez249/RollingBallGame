using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCount : MonoBehaviour
{
    public int levelCount;

    private static LevelCount Instance;

    void Awake()
    {
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
}
