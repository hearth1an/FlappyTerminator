using UnityEngine;

public class ObjectRemover : MonoBehaviour
{
    [SerializeField] private ObjectPool _pool;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Turret turret))
        {
            _pool.PutObject(turret);
        }
    }
}
