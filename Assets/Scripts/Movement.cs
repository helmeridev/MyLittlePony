using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody2D rb;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    private float horizontal;
    [SerializeField, Space(5)] private Transform feet;
    [SerializeField] private LayerMask jumpLayer;
    [SerializeField] private float maxFeetDist;
    void Update() {
        horizontal = Input.GetAxis("Horizontal");

        if(Input.GetButton("Jump") && IsGrounded()) {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
        if(Input.GetButtonUp("Jump") && rb.velocity.y > 0f) {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }
    }

    void FixedUpdate() {
        rb.velocity = new Vector2(horizontal * moveSpeed, rb.velocity.y);
    }

    private bool IsGrounded() {
        return Physics2D.OverlapCircle(feet.position, maxFeetDist, jumpLayer);
    }
}
