using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Enemy _enemy;
    [SerializeField] private Transform _spawners;
    [SerializeField] private float _coolDown;
    [SerializeField] private LevelPathes _levelPathes;

    private Transform[] _points;

    private void Start()
    {
        _points = new Transform[_spawners.childCount];

        for (int i = 0; i < _spawners.childCount; i++)
        {
            _points[i] = _spawners.GetChild(i);
        }

        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        var waitForSeconds = new WaitForSeconds(_coolDown);

        for (int i = 0; i < _points.Length; i++)
        {
            var spawnedEnemy = Instantiate(_enemy, _points[i].position, Quaternion.identity);
            spawnedEnemy.Init(_levelPathes.GetFirstPath()); 

            yield return waitForSeconds;
        }
    }
}
