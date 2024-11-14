using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    BossManager bossManager;

    [SerializeField] float damage;
    bool canAttack;

    void Update() {
        if(Input.GetMouseButtonDown(0) && canAttack) {
            Attack();
        }
    }

    void Attack() {
        bossManager.currentHealth -= damage;
    }

    void OnTriggerStay2D(Collider2D other) {
        if(other.gameObject.CompareTag("Boss")) {
            if(bossManager == null) bossManager = other.gameObject.GetComponent<BossManager>();

            canAttack = true;
        }
    }
    void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.CompareTag("Boss")) {
            canAttack = false;
        }
    }
}
