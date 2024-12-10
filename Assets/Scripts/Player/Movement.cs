using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody2D rb;

    [SerializeField] Transform sprite;
    [SerializeField] Animator animator;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;

    public static bool canMove = true;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    private float horizontal;
    private bool isFacingRight = true;
    [SerializeField, Space(5)] private Transform feet;
    [SerializeField] private LayerMask jumpLayer;
    [SerializeField] private float maxFeetDist;
    void Update() {
        if(!canMove) {
            horizontal = 0;
        }

        if(canMove) {
            horizontal = Input.GetAxis("Horizontal");

            if(Input.GetButton("Jump") && IsGrounded()) {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }
            if(Input.GetButtonUp("Jump") && rb.velocity.y > 0f) {
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
            }
        }
        else if(Mathf.Abs(rb.velocity.x) > 0){
            rb.velocity = Vector3.zero;
        }

        if(Mathf.Abs(horizontal) > 0) {
            animator.Play("Walking");
            AudioManager.instance.Play("PlayerWalk");
        }
        else {
            animator.Play("Idle");
            AudioManager.instance.Stop("PlayerWalk");
        }

        Flip();
    }

    void FixedUpdate() {
        if(canMove) {
            rb.velocity = new Vector2(horizontal * moveSpeed, rb.velocity.y);
        }
    }

    void Flip() {
        if(isFacingRight && horizontal < 0) {
            sprite.rotation = Quaternion.Euler(0, 180, 0);

            isFacingRight = false;
        }
        else if(!isFacingRight && horizontal > 0) {
            sprite.rotation = Quaternion.Euler(0, 0, 0);

            isFacingRight = true;
        }
    }

    private bool IsGrounded() {
        return Physics2D.OverlapCircle(feet.position, maxFeetDist, jumpLayer);
    }
}
