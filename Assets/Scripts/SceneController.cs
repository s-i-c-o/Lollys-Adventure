using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    public static SceneController Instance { get; private set; }

    public int currentLevel { get; set; }
    public string[] levelNames = { "MainMenu", "Level1", "Level2", "Level3", "Level4", "Level5", "GameOver", "TheEnd" };
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
