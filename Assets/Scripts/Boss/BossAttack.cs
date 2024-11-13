using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    BossManager manager;

    [SerializeField] float attackTime;
    private float remainingAttackTime;

    void Start() {
        manager = GetComponent<BossManager>();
    }

    void Update() {
        if(remainingAttackTime > 0) remainingAttackTime -= Time.deltaTime;

        AttackLogic();
        ReturnIsAttacking();
    }

    void AttackLogic() {
        if(CanAttack())  {
            Attack();
        }
    }

    void Attack() {
        remainingAttackTime = attackTime;
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
