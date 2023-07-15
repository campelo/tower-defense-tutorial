using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    public static int EnemiesAlive = 0;

    public Wave[] Waves;
    public float TimeBetweenWaves = 10f;
    [Range(.1f, 10f)]
    public float TimeBetweenEnemies = 1.5f;
    public Transform SpawnPoint;
    public TextMeshProUGUI WaveCountdownText;
    public TextMeshProUGUI WaveCount;
    public int MaxWaves = 100;

    [HideInInspector]
    public static int WaveIndex;

    private float _countDown;

    void Awake()
    {
        ResetVariables();   
    }

    void Start()
    {
        ResetVariables();
    }

    void Update()
    {
        if (EnemiesAlive > 0)
            return;
        
        if (_countDown <= 0f)
        {
            StartCoroutine(SpawnWave());
            _countDown = TimeBetweenWaves;
            WaveCount.text = $"Wave: {WaveIndex+1}/{MaxWaves}";
            return;
        }
        
        _countDown -= Time.deltaTime;
        
        var count = Mathf.Ceil(_countDown);
        
        WaveCountdownText.enabled = count <= 5 && count > 0;
        if (WaveCountdownText.enabled)
            WaveCountdownText.text = count.ToString();
    }

    private void ResetVariables()
    {
        _countDown = TimeBetweenWaves;
        WaveIndex = 0;
        WaveCountdownText.enabled = false;
        WaveCount.text = $"Wave: {WaveIndex + 1}/{MaxWaves}";
    }

    IEnumerator SpawnWave()
    {
        PlayerStats.Rounds++;
        int waveMultiplier = (WaveIndex / Waves.Length) + 1;
        Wave wave = Waves[WaveIndex % Waves.Length];

        TimeBetweenEnemies *= 0.95f;
        TimeBetweenEnemies = Math.Clamp(TimeBetweenEnemies, .1f, float.MaxValue);
        for (int i = 0; i < (wave.Count * waveMultiplier); i++)
        {
            SpawnEnemy(wave.Enemy);
            yield return new WaitForSeconds(1 / (wave.Rate * waveMultiplier));
        }
        WaveIndex++;
        if(WaveIndex == (MaxWaves - 1))
        {
            Debug.Log("LEVEL WON!");
            this.enabled = false;
        }
    }

    private void SpawnEnemy(GameObject enemy)
    {
        Instantiate(enemy, SpawnPoint.position, SpawnPoint.rotation);
        EnemiesAlive++;
    }
}
