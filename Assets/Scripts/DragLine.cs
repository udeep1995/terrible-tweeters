using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragLine : MonoBehaviour
{
    private LineRenderer _lineRenderer;
    private Bird _bird;

    void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _bird = FindObjectOfType<Bird>();
        
        _lineRenderer.SetPosition(0, _bird.transform.position);
    }

    void Update()
    {
        if (_bird.IsDragging)
        {
            _lineRenderer.enabled = true;
            _lineRenderer.SetPosition(1, _bird.transform.position);
        } else
        {
            _lineRenderer.enabled = false;
        }
    }
}
