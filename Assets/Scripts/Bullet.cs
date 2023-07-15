using System;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform _target;
    public float Speed = 50f;
    public float Damage = .5f;
    public GameObject ImpactEffect;
    public float ExplosionRadius = 0f;

    public void Seek(Transform target)
    {
        _target = target;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (_target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = _target.position - transform.position;
        float distanceThisFrame = Speed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
        transform.LookAt(_target);
    }

    private void HitTarget()
    {
        GameObject effectInstance = Instantiate(ImpactEffect, transform.position, transform.rotation);
        Destroy(effectInstance, 5f);
        if (ExplosionRadius > 0f)
            Explode();
        else
            DoDamage(_target);
        Destroy(gameObject);
    }

    private void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, ExplosionRadius);
        foreach(Collider collider in colliders)
        {
            if (collider.tag == "Enemy")
            {
                DoDamage(collider.transform);
            }
        }
    }

    private void DoDamage(Transform target)
    {
        Enemy enemy = target.GetComponent<Enemy>();
        if (enemy == null)
            return;
        enemy.TakeDamage(Damage);
    }
}
