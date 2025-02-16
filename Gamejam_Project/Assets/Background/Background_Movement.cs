using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Background_Movement : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float moveInterval = 3f;

    private Vector2 currentDirection;
    private float timer;

    public float minX = -13f;
    public float maxX = 13f;
    public float minY = -15f;
    public float maxY = 15f;

    private void Start()
    {
        ChangeDirection();
        timer = moveInterval;
    }

    void Update()
    {
        transform.Translate(currentDirection * moveSpeed * Time.deltaTime);

        ClampPosition();

        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            ChangeDirection();
            timer = moveInterval;
        }
    }

    void ChangeDirection()
    {
        int directionChoice = Random.Range(0, 2);
        if (directionChoice == 0)
        {
            currentDirection = Vector2.down;
        }
        else
        {
            currentDirection = Vector2.left; 
        }
    }

    void ClampPosition()
    {
        Vector2 position = transform.position;
        position.x = Mathf.Clamp(position.x, minX, maxX);
        position.y = Mathf.Clamp(position.y, minY, maxY);
        transform.position = position;

        if (position.x <= minX || position.x >= maxX)
        {
            currentDirection.x *= -1;
        }
        if (position.y <= minY || position.y >= maxY)
        {
            currentDirection.y *= -1;
        }
    }
}
