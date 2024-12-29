using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class BulletSpawner : MonoBehaviour
{
    [SerializeField] private Bullet _prefab;
    [SerializeField] private Transform _spawnPoint;

    public ObjectPool<Bullet> _pool;
    private bool _isShooting = false;
    private float _shootInterval = 1f;

    private void Awake()
    {
        _pool = new ObjectPool<Bullet>(
            createFunc: CreateBullet,
            actionOnGet: ActivateBullet,
            actionOnRelease: DeactivateBullet,
            actionOnDestroy: DestroyBullet,
            defaultCapacity: 10,
            maxSize: 20
        );
    }

    private Bullet CreateBullet()
    {
        Bullet bullet = Instantiate(_prefab);
        bullet.SetPool(_pool);
        bullet.gameObject.SetActive(false);
        return bullet;
    }

    private void ActivateBullet(Bullet bullet)
    {
        bullet.transform.position = _spawnPoint.position;
        bullet.transform.rotation = _spawnPoint.rotation;

        Vector3 direction = _spawnPoint.right;

        bullet.SetDirection(direction);

        bullet.gameObject.SetActive(true);
    }

    private void DeactivateBullet(Bullet bullet)
    {
        bullet.gameObject.SetActive(false);
    }

    private void DestroyBullet(Bullet bullet)
    {
        Destroy(bullet.gameObject);
    }

    public void StartShooting(Vector2 direction)
    {        
        if (!_isShooting)
            StartCoroutine(ShootCoroutine(direction));
    }

    private IEnumerator ShootCoroutine(Vector2 direction)
    {
        WaitForSeconds wait = new WaitForSeconds(_shootInterval);

        _isShooting = true;

        while (_isShooting)
        {
            Bullet bullet = _pool.Get();        

            yield return wait;
        }

        _isShooting = false;
    }
}
