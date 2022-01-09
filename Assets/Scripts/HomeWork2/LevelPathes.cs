using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPathes : MonoBehaviour
{
    [SerializeField] private List<Path> _path = new List<Path>();

    public Path GetPath0()
    {
        return _path[0];
    }

    [ContextMenu("GetPathes")]
    private void GetPathes()
    {
        _path.Clear();
        foreach (Transform child in transform)
        {
            _path.Add(child.GetComponent<Path>());
        }
    }
}
