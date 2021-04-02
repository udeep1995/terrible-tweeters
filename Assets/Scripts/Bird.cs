using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    private Vector2 _startPos;
    private Rigidbody2D _birdRb;
    public float forceMultiplier = 500;
    public float maxDragDistance = 5;

    public bool IsDragging { get; private set; }

    private void Awake()
    {
        _birdRb = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        _startPos = _birdRb.position;
        _birdRb.isKinematic = true;
    }

    private void OnMouseDrag()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 desiredPos = mousePos;

        float distance = Vector2.Distance(desiredPos, _startPos);
        if(distance > maxDragDistance)
        {
            Vector2 direction = desiredPos - _startPos;
            direction.Normalize();
            desiredPos = _startPos + direction * maxDragDistance;
        }

        if (desiredPos.x > _startPos.x)
            desiredPos.x = _startPos.x;

        _birdRb.position = desiredPos;
    }

    private void OnMouseUp()
    {
        var currentPos = _birdRb.position;
        var direction = _startPos - currentPos;
        direction.Normalize();
        _birdRb.isKinematic = false;
        _birdRb.AddForce(direction * forceMultiplier);
        IsDragging = false;

        GameController.reduceChanceEvent.Invoke();
    }

    private void OnMouseDown()
    {
        IsDragging = true;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        StartCoroutine(ResetAfterDelay());
    }

    private IEnumerator ResetAfterDelay()
    {
        yield return new WaitForSeconds(3f);
     
        if(LevelController.instance.chances>0)
        {
           _birdRb.position = _startPos;
           _birdRb.isKinematic = true;
           _birdRb.velocity = Vector2.zero;
        } else
        {
            GameController.gameOverEvent.Invoke();
        }
    }
}
