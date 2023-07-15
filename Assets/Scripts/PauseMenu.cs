using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject PauseUI;
    private float _initialTimeScale;
    public SceneFader SceneFader;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
            Toggle();
    }

    private void Toggle()
    {
        PauseUI.SetActive(!PauseUI.activeSelf);
        if (PauseUI.activeSelf)
        {
            _initialTimeScale = Time.timeScale;
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = _initialTimeScale;
        }
    }

    public void Pause()
    {
        Toggle();
    }

    public void Continue()
    {
        Toggle();
    }

    public void Retry()
    {
        _initialTimeScale = 1;
        Toggle();
        SceneFader.FadeTo(SceneManager.GetActiveScene().name);
    }

    public void Menu()
    {
        SceneFader.FadeTo(nameof(MainMenu));
    }
}
