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

    void Update() {
        if(Input.GetMouseButtonDown(0) && canAttack) {
            Attack();
        }
    }

    void Attack() {
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
