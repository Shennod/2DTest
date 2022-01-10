using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    [SerializeField] private List<Transform> _points = new List<Transform>();

    public List<Transform> Points { get => _points; set => _points = value; }

    [ContextMenu("GetWaypoints")]
    private void GetWaypoints()
    {
        Points.Clear();
        foreach (Transform child in transform)
        {
            Points.Add(child);
        }
    }
}
