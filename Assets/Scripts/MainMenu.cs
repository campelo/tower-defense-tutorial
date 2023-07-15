using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string LevelToLoad = "Level001";
    public SceneFader SceneFader;

    private void Awake()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }

    public void Play()
    {
        SceneFader.FadeTo(LevelToLoad);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
