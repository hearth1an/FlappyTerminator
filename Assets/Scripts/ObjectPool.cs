using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private Transform _container;
    [SerializeField] private Turret _prefab;

    private Queue<Turret> _pool;

    public IEnumerable<Turret> PooledObjects => _pool;

    private void Awake()
    {
        _pool = new Queue<Turret>();
    }

    public Turret GetObject()
    {
        if (_pool.Count == 0)
        {
            var pipe = Instantiate(_prefab);
            pipe.transform.parent = _container;

            return pipe;
        }

        return _pool.Dequeue();
    }

    public void PutObject(Turret Turret)
    {
        _pool.Enqueue(Turret);
        Turret.gameObject.SetActive(false);
    }

    public void Reset()
    {
        _pool.Clear();
    }
}
