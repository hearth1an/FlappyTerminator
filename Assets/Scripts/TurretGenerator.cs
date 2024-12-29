using System.Collections;
using UnityEngine;

public class TurretGenerator : MonoBehaviour
{
    [SerializeField] private float _delay;
    [SerializeField] private float _lowerBound;
    [SerializeField] private float _upperBound;
    [SerializeField] private ObjectPool _pool;

    private void Start()
    {
        StartCoroutine(GenerateTurrets());
    }

    private IEnumerator GenerateTurrets()
    {
        var wait = new WaitForSeconds(_delay);

        while (enabled) 
        {
            Spawn();
            yield return wait;
        }
    }

    private void Spawn()
    {
        float spawnPositionY = Random.Range(_upperBound, _lowerBound);
        Vector3 spawnPoint = new Vector3(transform.position.x, spawnPositionY, transform.position.z);

        var turret = _pool.GetObject();

        turret.gameObject.SetActive(true);
        turret.transform.position = spawnPoint;
    }
}
