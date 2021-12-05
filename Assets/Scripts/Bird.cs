using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    [SerializeField] float _launchForce = 500;
    [SerializeField] float _maxDragPosition = 2;

    public SpriteRenderer _spriteRenderer;
    public Rigidbody2D _rigidBody;
    Vector2 _startPosition;
    

    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        _startPosition = _rigidBody.position;
        _rigidBody.isKinematic = true;
    }

    void OnMouseDown()
    {
        _spriteRenderer.color = Color.red;
    }

    void OnMouseUp()
    {
        _rigidBody.isKinematic = false;
        Vector2 currentPosition = _rigidBody.position;
        Vector2 direction = _startPosition - currentPosition;
        direction.Normalize();
        _rigidBody.AddForce(direction * _launchForce);
        _spriteRenderer.color = Color.white;
    }

    private void OnMouseDrag()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 desiredPosition = mousePosition;
       
        float distance = Vector2.Distance(desiredPosition, _startPosition);
        if (distance > _maxDragPosition)
        {
            Vector2 direction = desiredPosition - _startPosition;
            direction.Normalize();
            desiredPosition = _startPosition + (direction * _maxDragPosition);
        }
        if (desiredPosition.x > _startPosition.x)
            desiredPosition.x = _startPosition.x;
        _rigidBody.position = desiredPosition;
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnCollisionEnter2D(Collision2D collision2D)
    {
        StartCoroutine(ResetAfterDelay());
    }

    IEnumerator ResetAfterDelay()
    {
        yield return new WaitForSeconds(3);
        _rigidBody.position = _startPosition;
        _rigidBody.isKinematic = true;
        _rigidBody.velocity = Vector2.zero;
    }
}