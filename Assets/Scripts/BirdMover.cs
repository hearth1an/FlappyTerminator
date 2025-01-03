using UnityEngine;
using System;

[RequireComponent(typeof(Rigidbody2D))]
public class BirdMover : MonoBehaviour
{
    private const KeyCode Jump = KeyCode.Space;
    private const KeyCode Ability = KeyCode.E;

    [SerializeField] private float _tapForce;
    [SerializeField] private float _speed;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _maxRotationZ;
    [SerializeField] private float _minRotationZ;
    [SerializeField] private BulletSpawner _bulletSpawner;

    private Vector3 _startPosition;
    private Rigidbody2D _rigidbody2D;
    private Quaternion _maxRotation;
    private Quaternion _minRotation;

    private void Awake()
    {       
        _startPosition = transform.position;
        _rigidbody2D = GetComponent<Rigidbody2D>();

        _maxRotation = Quaternion.Euler(0, 0, _maxRotationZ);
        _minRotation = Quaternion.Euler(0, 0, _minRotationZ);

        Reset();
    }

    private void Update()
    {
        if (Input.GetKeyDown(Jump))
        {
            _rigidbody2D.velocity = new Vector2(_speed, _tapForce);
            transform.rotation = _maxRotation;
        }

        if (Input.GetKeyDown(Ability))
        {
            _bulletSpawner.ShootSingle();
        }

        transform.rotation = Quaternion.Lerp(transform.rotation, _minRotation, _rotationSpeed * Time.deltaTime);
    }

    public void Reset()
    {
        transform.position = _startPosition;
        transform.rotation = Quaternion.identity;
        _rigidbody2D.velocity = Vector2.zero;
    }
}
