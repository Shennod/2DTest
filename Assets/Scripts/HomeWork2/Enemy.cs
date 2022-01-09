using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Path _path;
    [SerializeField] private float _speed;

    private int _currentPoint;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        Transform target = _path.Points[_currentPoint];
        Vector2 direction = (target.position - transform.position).normalized;
        _spriteRenderer.flipX = direction.x > 0;
        transform.position = Vector3.MoveTowards(transform.position, target.position, _speed * Time.deltaTime);

        if(transform.position == target.position)
        {
            _currentPoint++;

            if(_currentPoint >= _path.Points.Count)
            {
                _currentPoint = 0;
            }
        }            
    }

    internal void Init(Path path)
    {
        _path = path;
    }
}
