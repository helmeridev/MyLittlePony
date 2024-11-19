using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    BossManager manager;

    [SerializeField] Vector2 damage;
    [SerializeField] float attackTime;
    private float remainingAttackTime;
    [SerializeField] GameObject damageTextPrefab;

    void Start() {
        manager = GetComponent<BossManager>();
    }

    void Update() {
        if(remainingAttackTime > 0) remainingAttackTime -= Time.deltaTime;

        AttackLogic();
        ReturnIsAttacking();
    }

    void AttackLogic() {
        if(manager.currentHealth > 0) {
            if(CanAttack())  {
                Attack();
            }
        }
    }

    void Attack() {
        remainingAttackTime = attackTime;
        manager.animator.Play("BossAttack1");
    }

    void SpawnDamageText(float newDamage, Transform spawnPos) {
        GameObject newDamageText = Instantiate(damageTextPrefab, spawnPos.position, damageTextPrefab.transform.rotation);
        DamageText damageText = newDamageText.GetComponent<DamageText>();
        damageText.damageAmount = newDamage;
    }

    public void DealDamage() {
        if(manager.GetPointCollider(manager.armLeft).IsColliding() ||
           manager.GetPointCollider(manager.armRight).IsColliding()) {
            float newDamage = Random.Range(damage.x, damage.y);
            manager.taxManager.money -= newDamage;
            SpawnDamageText(newDamage, manager.player);
        }
    }

    bool CanAttack() {
        if(remainingAttackTime <= 0 &&
           manager.GetPointCollider(manager.attackCollider).IsColliding()
           ) {
            return true;
           }
        else return false;
    }

    void ReturnIsAttacking() {
        if(remainingAttackTime <= 0) manager.isAttacking = false;
        else manager.isAttacking = true;
    }
}
