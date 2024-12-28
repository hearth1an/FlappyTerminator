using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void GetDirection(Vector3 direction)
    {
        _rigidbody.velocity = new Vector2(direction.x, direction.y).normalized;
    }
}