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

    private Camera mainCamera;
    private Vector2 backgroundSize;
    private Vector2 cameraSize;

    private float minX;
    private float maxX;
    private float minY;
    private float maxY;

    private void Start()
    {
        mainCamera = Camera.main;
        if (mainCamera == null)
        {
            Debug.LogError("Main camera not found");
            return;
        }

        SpriteRenderer backgroundRenderer = GetComponent<SpriteRenderer>();
        if (backgroundRenderer != null)
        {
            backgroundSize = backgroundRenderer.bounds.size;
        }
        else
        {
            Debug.LogError("Background does not have a SpriteRenderer component");
            return;
        }

        cameraSize = new Vector2(
            mainCamera.orthographicSize * 2 * mainCamera.aspect, // Width
            mainCamera.orthographicSize * 2 // Height
        );

        CalculateCameraBounds();

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
        // Determine the current movement axis (horizontal or vertical)
        bool isMovingHorizontally = Mathf.Abs(currentDirection.x) > Mathf.Abs(currentDirection.y);

        if (isMovingHorizontally)
        {
            // If currently moving horizontally, switch to vertical (up or down)
            int verticalChoice = Random.Range(0, 2);
            currentDirection = (verticalChoice == 0) ? Vector2.up : Vector2.down;
        }
        else
        {
            // If currently moving vertically, switch to horizontal (left or right)
            int horizontalChoice = Random.Range(0, 2);
            currentDirection = (horizontalChoice == 0) ? Vector2.left : Vector2.right;
        }
    }

    void CalculateCameraBounds()
    {
        minX = -backgroundSize.x / 2 + cameraSize.x / 2;
        maxX = backgroundSize.x / 2 - cameraSize.x / 2;
        minY = -backgroundSize.y / 2 + cameraSize.y / 2;
        maxY = backgroundSize.y / 2 - cameraSize.y / 2;
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
