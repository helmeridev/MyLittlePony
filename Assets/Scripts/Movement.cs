using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody2D rb;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;

    public static bool canMove = true;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    private float horizontal;
    [SerializeField, Space(5)] private Transform feet;
    [SerializeField] private LayerMask jumpLayer;
    [SerializeField] private float maxFeetDist;
    void Update() {
        if(canMove) {
            horizontal = Input.GetAxis("Horizontal");

            if(Input.GetButton("Jump") && IsGrounded()) {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }
            if(Input.GetButtonUp("Jump") && rb.velocity.y > 0f) {
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
            }
        }
        else {
            rb.velocity = Vector3.zero;
        }
    }

    void FixedUpdate() {
        if(canMove) {
            rb.velocity = new Vector2(horizontal * moveSpeed, rb.velocity.y);
        }
        else {
            rb.velocity = Vector3.zero;
        }
    }

    private bool IsGrounded() {
        return Physics2D.OverlapCircle(feet.position, maxFeetDist, jumpLayer);
    }
}
