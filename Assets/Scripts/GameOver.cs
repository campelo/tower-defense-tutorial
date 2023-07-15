using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public TextMeshProUGUI RoundsText;
    public SceneFader SceneFader;

    void OnEnable()
    {
        RoundsText.text = PlayerStats.Rounds.ToString();
        Time.timeScale = 0f;
    }

    public void Retry()
    {
        SceneFader.FadeTo(SceneManager.GetActiveScene().name);
    }

    public void Menu()
    {
        SceneFader.FadeTo(nameof(MainMenu));
    }
}
