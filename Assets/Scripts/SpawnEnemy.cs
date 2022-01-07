using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField] private GameObject _enemy;
    [SerializeField] private Transform _spawners;
    [SerializeField] private float _coolDown;

    private Transform[] _points;

    private void Start()
    {
        _points = new Transform[_spawners.childCount];

        for (int i = 0; i < _spawners.childCount; i++)
        {
            _points[i] = _spawners.GetChild(i);
        }

        StartCoroutine(Create());
    }

    private IEnumerator Create()
    {
        var waitForSeconds = new WaitForSeconds(_coolDown);

        for (int i = 0; i < _points.Length; i++)
        {
            Instantiate(_enemy, _points[i].position, Quaternion.identity);

            yield return waitForSeconds;
        }
    }
}
