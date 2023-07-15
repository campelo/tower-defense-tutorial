using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float StartSpeed = 10f;
    private bool _isSlowing;    

    [HideInInspector]
    public float Speed
    {
        get
        {
            return _navMeshAgent.speed;
        }
        set
        {
            _navMeshAgent.speed = value;
        }
    }
    public float StartHealth = float.MaxValue;
    private float _currentHealth;
    public int Worth = 7;
    public GameObject DeathEffect;

    [Header("Unity Stuff")]
    public Canvas CanvasInfo;
    public Image HealthImage;

    private bool _isDead = false;
    private NavMeshAgent _navMeshAgent;

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }
    void Start()
    {
        Speed = StartSpeed + (WaveSpawner.WaveIndex / 10);
        StartHealth += (WaveSpawner.WaveIndex / 5);
        _currentHealth = StartHealth;
    }

    void Update()
    {
        CanvasInfo.transform.eulerAngles = new Vector3 (40, 0, 0);
        _navMeshAgent.destination = GameManager.EndpointPosition;
        Vector3 dir = GameManager.EndpointPosition - transform.position;

        if (Vector3.Distance(transform.position, GameManager.EndpointPosition) < 2)
        {
            EndPath();
            return;
        }
        TryRemoveSlowing();
    }

    private void TryRemoveSlowing()
    {
        if (!_isSlowing)
            Speed = StartSpeed;
        else
            _isSlowing = false;
    }

    private void EndPath()
    {
        PlayerStats.RemoveLive();
        Destroy(gameObject);
    }

    public void TakeDamage(float amount)
    {
        _currentHealth -= amount;
        HealthImage.fillAmount = _currentHealth / StartHealth;
        if (_currentHealth <= 0)
        {
            Die();
        }
    }

    public void Slow(float percentage)
    {
        _isSlowing = true;
        Speed = StartSpeed * (1f - percentage);
    }

    private void OnDestroy()
    {
        WaveSpawner.EnemiesAlive--;
    }

    private void Die()
    {
        if (_isDead)
            return;
        _isDead = true;
        PlayerStats.MoneyGain(Worth);

        GameObject deathEffect = Instantiate(DeathEffect, transform.position, Quaternion.identity);
        Destroy(deathEffect, 5f);

        Destroy(gameObject);
    }
}
