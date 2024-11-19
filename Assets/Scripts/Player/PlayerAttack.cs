using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    BossManager bossManager;

    [SerializeField] float damage;
    bool canAttack;
    Transform bossPos;
    [SerializeField] GameObject damageTextPrefab;
    [SerializeField] float attackCooldown = 1;
    private float remainingCooldown;

    void Update() {
        if(remainingCooldown > 0) {
            remainingCooldown -= Time.deltaTime;
        }

        if(Input.GetMouseButtonDown(0) && canAttack && remainingCooldown <= 0) {
            Attack();
        }
    }

    void Attack() {
        remainingCooldown = attackCooldown;
        bossManager.currentHealth -= damage;
        SpawnDamageText(damage, bossPos);
    }

    void SpawnDamageText(float newDamage, Transform spawnPos) {
        GameObject newDamageText = Instantiate(damageTextPrefab, spawnPos.position, damageTextPrefab.transform.rotation);
        DamageText damageText = newDamageText.GetComponent<DamageText>();
        damageText.damageAmount = newDamage;
    }

    void OnTriggerStay2D(Collider2D other) {
        if(other.gameObject.CompareTag("Boss")) {
            if(bossManager == null) {
                bossManager = other.gameObject.GetComponent<BossManager>();
                bossPos = other.gameObject.transform;
            }

            if(bossManager.currentHealth > 0) {
                canAttack = true;
            }
            else {
                canAttack = false;
            }
        }
    }
    void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.CompareTag("Boss")) {
            canAttack = false;
        }
    }
}
