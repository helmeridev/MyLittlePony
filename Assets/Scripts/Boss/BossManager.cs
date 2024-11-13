using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    [Header("Body references")]
    public Transform sprite;
    public GameObject head;
    public GameObject body;
    public GameObject armLeft;
    public GameObject armRight;
    public GameObject legLeft;
    public GameObject legRight;

    [Header("Other references")]
    public Transform player;
    public GameObject attackCollider;

    [HideInInspector] public bool isAttacking;

    public PointCollider GetPointCollider(GameObject colliderObject) {
        if(colliderObject.GetComponent<PointCollider>()) return colliderObject.GetComponent<PointCollider>();
        else {
            Debug.LogError("Cannot get PointCollider from " + colliderObject.name + " because it does not have one!");
            return null;
        }
    }
}
