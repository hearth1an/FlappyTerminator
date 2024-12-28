using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] private GameObject _turret;
    [SerializeField] private BulletSpawner _bulletSpawner;

    private WaitForSeconds _rate;
    private float _checkRate = 0.1f;

    private float _viewRadius = 5f;
    public bool IsPlayerNear { get; private set; } = false;

    private void Awake()
    {        
        _rate = new WaitForSeconds(_checkRate);
    }
    private void Update()
    {
        StartCoroutine(nameof(CheckPlayerNear));
    }

    private bool IsPlayerInSight()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, _viewRadius);

        foreach (var hit in hits)
        {
            if (hit.TryGetComponent<BirdCollisionHandler>(out var player))
            {
                Vector3 direction = (player.transform.position - _turret.transform.position).normalized;

                // Рассчитываем угол в градусах (по оси Z)
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

                // Применяем угол к повороту башни
                _turret.transform.rotation = Quaternion.Euler(0, 0, angle);
                
                _bulletSpawner.StartCoroutine(_bulletSpawner.ShootCoroutine(player.transform.position));

                return true;
            }
        }

        return false;
    }

    private IEnumerator CheckPlayerNear()
    {
        while (enabled)
        {
            IsPlayerNear = IsPlayerInSight();

            yield return _rate;
        }
    }
}
