using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    BossManager manager;

    [SerializeField] Vector2 damage1;
    [SerializeField] float attackTime1;
    private float remainingAttackTime;
    [SerializeField] float damage2;
    [SerializeField] float attackTime2;
    [SerializeField] GameObject damageTextPrefab;
    [SerializeField] GameObject aofAttackPrefab;
    [SerializeField] int attacksBeforeAOF = 2;
    private int attackNumberNoDamage;

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
                Attack1();
            }
            else if(remainingAttackTime <= 0 && attackNumberNoDamage >= attacksBeforeAOF) {
                Attack2();
            }
        }
    }

    void Attack1() {
        remainingAttackTime = attackTime1;
        manager.animator.Play("BossAttack1");
    }
    void Attack2() {
        attackNumberNoDamage = 0;
        remainingAttackTime = attackTime1;
        manager.animator.Play("BossAttack2");
    }

    public void SpawnDamageText(float newDamage, Transform spawnPos) {
        GameObject newDamageText = Instantiate(damageTextPrefab, spawnPos.position, damageTextPrefab.transform.rotation);
        DamageText damageText = newDamageText.GetComponent<DamageText>();
        damageText.damageAmount = newDamage;
    }

    public void DealDamage() {
        if(manager.GetPointCollider(manager.armLeft).IsColliding() ||
           manager.GetPointCollider(manager.armRight).IsColliding()) {
            float newDamage = Random.Range(damage1.x, damage1.y);
            manager.taxManager.AddMoney(-newDamage);
            attackNumberNoDamage = 0;
            SpawnDamageText(newDamage, manager.player);
        }
        else {
            attackNumberNoDamage += 1;
        }
    }
    public void SpawnAttack2() {
        Vector3 spawnPos = new Vector3(transform.position.x, transform.position.y - 2, transform.position.z);
        GameObject newAOF = Instantiate(aofAttackPrefab, spawnPos, aofAttackPrefab.transform.rotation);
        ObjectAOF objectAOF = newAOF.GetComponent<ObjectAOF>();
        objectAOF.attackDamage = damage2;
        objectAOF.attackSpeed = attackTime2;
        objectAOF.tax = manager.taxManager;
        objectAOF.bossAttack = this;
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
