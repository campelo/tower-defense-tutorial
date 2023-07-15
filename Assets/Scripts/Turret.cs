using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class Turret : MonoBehaviour
{
    private Transform _target;
    private Enemy _targetEnemy;

    [Header("General")]
    public float Range = 15f;

    [Header("Use bullets (default)")]
    public GameObject BulletPrefab;
    public float FireRatePerMinute = 60f;
    private float _fireCountdown = 0f;

    [Header("Use laser")]
    public bool UseLaser = false;
    public LineRenderer LineRenderer;
    public ParticleSystem ImpactEffect;
    public Light ImpactLight;
    public float DamageOverSecond;
    public float SlowAmount = .25f;

    [Header("Unit Setup Fields")]
    public string enemyTag = "Enemy";
    public Transform PartToRotate;
    public float TurnSpeed = 10f;
    public Transform FirePoint;    

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating(nameof(UpdateTarget), 0f, 0.5f);
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= Range)
        {
            _target = nearestEnemy.transform;
            _targetEnemy = nearestEnemy.GetComponent<Enemy>();
        }
        else
        {
            _target = null;
            _targetEnemy = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_target == null)
        {
            if(UseLaser && LineRenderer.enabled)
            {
                ImpactEffect.Stop();
                LineRenderer.enabled = false;
                ImpactLight.enabled = false;
            }

            return;
        }
        if (_targetEnemy == null)
            return;
        LockOnTarget();
        if (UseLaser)
            Laser();
        else
            Shoot();
    }

    private void Laser()
    {
        _targetEnemy.TakeDamage(DamageOverSecond * Time.deltaTime);
        _targetEnemy.Slow(SlowAmount);
        if (!LineRenderer.enabled)
        {
            LineRenderer.enabled = true;
            ImpactEffect.Play();
            ImpactLight.enabled = true;
        }
        LineRenderer.SetPosition(0, FirePoint.position);
        LineRenderer.SetPosition(1, _target.position);

        Vector3 dir = FirePoint.position - _target.position;
        ImpactEffect.transform.rotation = Quaternion.LookRotation(dir);

        ImpactEffect.transform.position = _target.position + dir.normalized;
    }

    private void LockOnTarget()
    {
        Vector3 dir = _target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(PartToRotate.rotation, lookRotation, Time.deltaTime * TurnSpeed).eulerAngles;
        PartToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    private void Shoot()
    {
        if (_fireCountdown <= 0f)
        {
            DoShoot();
            _fireCountdown = 60f / FireRatePerMinute;
        }
        _fireCountdown -= Time.deltaTime;
    }

    private void DoShoot()
    {
        GameObject bulletGO = Instantiate(BulletPrefab, FirePoint.position, FirePoint.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();
        if (bullet != null)
            bullet.Seek(_target);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, Range);
    }
}
