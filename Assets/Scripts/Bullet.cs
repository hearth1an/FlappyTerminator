using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class Bullet : MonoBehaviour
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<BirdCollisionHandler>(out BirdCollisionHandler player))
        {
            _pool.Release(this);
        }
        else
        {
            _pool.Release(this);
        }
    }
}
