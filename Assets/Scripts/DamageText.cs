using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageText : MonoBehaviour
{
    public float moveSpeed = 5;
    public float lifeTime = 1;
    public float damageAmount;
    public TMP_Text damageText;

    void Start() {
        damageText.text = "-" + Mathf.Abs(damageAmount).ToString("0.0");
    }

    void Update() {
        transform.position += new Vector3(0, moveSpeed, 0) * Time.deltaTime;

        lifeTime -= Time.deltaTime;
        if(lifeTime <= 0) {
            Destroy(gameObject);
        }
    }
}
