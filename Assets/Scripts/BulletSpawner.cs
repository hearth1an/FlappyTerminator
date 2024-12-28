using System.Collections;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    [SerializeField] private Bullet _prefab;
    [SerializeField] private Transform _spawnPoint;

    private float _shootInterval = 2f; // �������� ����� ����������
    private bool _isShooting = false;  // ���� ��� �������� ��������� ��������

    public void StartShooting(Vector3 direction)
    {
        if (!_isShooting) // ��������, ����� �������� ������������ �������
        {
            _isShooting = true;
            StartCoroutine(ShootCoroutine(direction));
        }
    }

    public IEnumerator ShootCoroutine(Vector3 direction)
    {
        while (_isShooting) // ����������� ���� ��� ���������� ��������
        {
            Shoot(direction); // �������� �����
            yield return new WaitForSeconds(_shootInterval); // ��� ��������� �����
        }
    }

    public void StopShooting()
    {
        _isShooting = false; // ������������� ��������
    }

    private void Shoot(Vector3 direction)
    {
        Bullet bullet = Instantiate(_prefab, _spawnPoint.position, Quaternion.identity);
        bullet.GetDirection(direction);
    }
}
