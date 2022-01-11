using System.Collections.Generic;
using UnityEngine;

public class LevelPathes : MonoBehaviour
{
    [SerializeField] private List<Path> _path = new List<Path>();

    public Path GetFirstPath()
    {
        return _path[0];
    }

    [ContextMenu("AssignPathsInLevel")]
    private void AssignPathsInLevel()
    {
        _path.Clear();
        foreach (Transform child in transform)
        {
            _path.Add(child.GetComponent<Path>());
        }
    }
}
