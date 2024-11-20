using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectAOF : MonoBehaviour
{
    [SerializeField] float scaleSpeed = 2;
    [SerializeField] float scaleTime = 5;
    public float attackSpeed;
    private float remainingCooldown;
    public float attackDamage;
    public TaxManager tax;
    public BossAttack bossAttack;
    private bool isColliding;
    private Transform collisionTransform;

    void Start() {
        remainingCooldown = attackSpeed;
    }

    void Update() {
        transform.localScale += new Vector3(scaleSpeed, scaleSpeed, 0) * Time.deltaTime;

        scaleTime -= Time.deltaTime;
        if(scaleTime <= 0) {
            Destroy(gameObject);
        }

        //Damage player
        if(remainingCooldown > 0) {
            remainingCooldown -= Time.deltaTime;
        }

        if(isColliding && remainingCooldown <= 0) {
            tax.AddMoney(-attackDamage);
            bossAttack.SpawnDamageText(-attackDamage, collisionTransform);
            remainingCooldown = attackSpeed;
        }
    }

    void OnTriggerStay2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player")) {
            isColliding = true;
            collisionTransform = other.transform;
        }
    }
    void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player")) {
            isColliding = false;
        }
    }
}
