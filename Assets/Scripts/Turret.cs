using System;
using System.Collections;
using UnityEngine;

public class Turret : MonoBehaviour, IInteractable
{
    [SerializeField] private Transform _turret;
    [SerializeField] private BulletSpawner _bulletSpawner;

    private WaitForSeconds _rate;
    private float _checkRate = 0.01f;
    private float _viewRadius = 10f;
    private Vector2 _currentDirection;

    public event Action<Turret> TurretDestroyed;

    private void Awake()
    {       
        _rate = new WaitForSeconds(_checkRate);
    }

    public void Shoot()
    {
        StartCoroutine(CheckPlayerNear());
    }

    private IEnumerator CheckPlayerNear()
    {
        while (enabled)
        {
            if (IsPlayerInSight())
            {                
                _bulletSpawner.StartShooting(_currentDirection);
            }

            yield return _rate;
        }
    }

    public void InitDestroy()
    {
        TurretDestroyed?.Invoke(this);
        gameObject.SetActive(false);
    }

    private bool IsPlayerInSight()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, _viewRadius);

        foreach (var hit in hits)
        {
            if (hit.TryGetComponent<BirdCollisionHandler>(out var player))
            {
                Vector2 direction = (player.transform.position - _turret.position).normalized;

                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                _turret.rotation = Quaternion.Euler(0, 0, angle);
                _currentDirection = direction;

                return true;
            }
        }

        return false;
    }
}
