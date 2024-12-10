using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovement : MonoBehaviour
{
    Rigidbody2D rb;
    BossManager manager;

    [SerializeField] float moveSpeed;
    bool isFacingRight;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        manager = GetComponent<BossManager>();
    }

    void Update() {
        if(manager.isActive) {
            if(manager.currentHealth > 0) {
                if(!manager.isAttacking) {
                    Move();
                    Flip();
                }
                else {
                    rb.velocity = new Vector2(0, rb.velocity.y);
                }
            }
            else {
                rb.velocity = new Vector2(0, rb.velocity.y);
                manager.finishDoor.SetActive(false);
            }
        }
    }
    
    //Movement
    void Move() {
        if(!manager.GetPointCollider(manager.attackCollider).IsColliding()) {
            if(IsMovingRight()) {
                rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
            }
            else {
                rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
            }

            manager.animator.Play("BossWalk");
        }
        else {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }
    bool IsMovingRight() {
        if(manager.player.position.x > transform.position.x) return true;
        else return false;
    }

    void Flip() {
        if(isFacingRight && IsMovingRight()) {
            manager.sprite.rotation = Quaternion.Euler(0, 180, 0);

            isFacingRight = false;
        }
        else if(!isFacingRight && !IsMovingRight()) {
            manager.sprite.rotation = Quaternion.Euler(0, 0, 0);

            isFacingRight = true;
        }
    }
}