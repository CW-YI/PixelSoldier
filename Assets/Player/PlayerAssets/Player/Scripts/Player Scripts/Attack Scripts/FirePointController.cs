using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePointController : MonoBehaviour
{
    private Transform playerTransform;
    private SpriteRenderer playerSpriteRenderer;
    private bool facingRight = true;

    private void Awake()
    {
        // Get references to the required components
        playerTransform = transform.parent;
        playerSpriteRenderer = playerTransform.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        // Check the player's movement direction
        float moveInput = Input.GetAxisRaw("Horizontal");

        // Update the fire point position based on the sprite's flip
        if (moveInput > 0 && !facingRight)
        {
            FlipFirePoint();
        }
        else if (moveInput < 0 && facingRight)
        {
            FlipFirePoint();
        }
    }

    private void FlipFirePoint()
    {
        // Toggle the facing direction
        facingRight = !facingRight;

        // Calculate the new position of the fire point
        Vector3 localPosition = transform.localPosition;
        localPosition.x *= -1;
        transform.localPosition = localPosition;
        transform.Rotate(0f, 180f, 0f);
    }
}