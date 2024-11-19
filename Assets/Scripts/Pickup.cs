using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    TaxManager tax;
    [SerializeField] float amount;

    public enum Type {
        money
    }
    public Type type;

    void OnTriggerEnter2D(Collider2D other) {
        if(tax = other.GetComponent<TaxManager>()) {
            switch(type) {
                case Type.money:
                    tax.AddMoney(amount);
                    Destroy(gameObject);
                    break;
                default:
                    Destroy(gameObject);
                    break;
            }
        }
    }
}
