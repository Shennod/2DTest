using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    [SerializeField] private List<Transform> _points = new List<Transform>();

    public List<Transform> Points { get => _points; private set => _points = value; }

    [ContextMenu("AssignWaypoints")]
    private void AssignWaypoints()
    {
        Points.Clear();
        foreach (Transform child in transform)
        {
            Points.Add(child);
        }
    }
}
