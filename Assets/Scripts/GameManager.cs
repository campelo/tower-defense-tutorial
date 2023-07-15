using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static bool GameIsOver;
    public GameObject GameOverUI;
    public TextMeshProUGUI TimeScaleText;
    private int _timeScaleFactor = 0;
    private int _timeScaleQuantity = 3;
    public GameObject Endpoint;
    [HideInInspector]
    public static Vector3 EndpointPosition;

    void Awake()
    {
        if (IsMobile)
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }

    void Start()
    {
        EndpointPosition = Endpoint.transform.position;
        SceneManager.sceneLoaded += OnSceneLoaded;
        GameIsOver = false;
        _timeScaleFactor = 0;
        SetTimeScaleText();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        ResetTimeScale();
    }

    private void ResetTimeScale()
    {
        _timeScaleFactor = 0;
        SetTimeScale();
    }

    public void ToggleTimeScale()
    {
        _timeScaleFactor = ++_timeScaleFactor % _timeScaleQuantity;
        SetTimeScale();
    }

    public void SetTimeScale()
    {
        SetTimeScaleText();
        Time.timeScale = Factor;
    }

    private void SetTimeScaleText()
    {
        TimeScaleText.text = $"{Factor}X";
    }

    private int Factor => Convert.ToInt32(Math.Pow(2, _timeScaleFactor));

    // Update is called once per frame
    void Update()
    {
        if (GameIsOver)
            return;
        if (PlayerStats.Lives <= 0)
        {
            EndGame();
            return;
        }
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void EndGame()
    {
        GameIsOver = true;
        GameOverUI.SetActive(true);
    }

    public static bool IsMobile => SystemInfo.deviceType == DeviceType.Handheld;

    /// <summary>
    /// To inform if has mouse, keyboard interactions...
    /// </summary>
    public static bool IsOtherDevice => !IsMobile;
}
