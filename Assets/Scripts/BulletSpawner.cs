using System.Collections;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    [SerializeField] private Bullet _prefab;
    [SerializeField] private Transform _spawnPoint;

    private float _shootInterval = 2f; // Интервал между выстрелами
    private bool _isShooting = false;  // Флаг для контроля состояния стрельбы

    public void StartShooting(Vector3 direction)
    {
        if (!_isShooting) // Проверка, чтобы избежать дублирования корутин
        {
            _isShooting = true;
            StartCoroutine(ShootCoroutine(direction));
        }
    }

    public IEnumerator ShootCoroutine(Vector3 direction)
    {
        while (_isShooting) // Бесконечный цикл для постоянной стрельбы
        {
            Shoot(direction); // Стреляем пулей
            yield return new WaitForSeconds(_shootInterval); // Ждём указанное время
        }
    }

    public void StopShooting()
    {
        _isShooting = false; // Останавливаем стрельбу
    }

    private void Shoot(Vector3 direction)
    {
        Bullet bullet = Instantiate(_prefab, _spawnPoint.position, Quaternion.identity);
        bullet.GetDirection(direction);
    }
}
