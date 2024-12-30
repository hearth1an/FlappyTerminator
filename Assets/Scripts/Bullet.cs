using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class Bullet : MonoBehaviour, IInteractable
{
    private Rigidbody2D _rigidbody;
    private float _speed = 5f;
    private ObjectPool<Bullet> _pool;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void SetPool(ObjectPool<Bullet> pool)
    {       
        _pool = pool;
    }

    public void SetDirection(Vector2 direction)
    {  
        _rigidbody.AddForce(direction * _speed, ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<BirdCollisionHandler>(out BirdCollisionHandler player))
        {
            _pool.Release(this);
        }

        if (collision.gameObject.TryGetComponent<Turret>(out Turret turret))
        {
            _pool.Release(this);
            turret.gameObject.SetActive(false);
        }
    }
}
