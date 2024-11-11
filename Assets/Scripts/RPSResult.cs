using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPSResult : MonoBehaviour
{
    [SerializeField] float disableTime;
    float remainingTime;

    void Awake() {
        remainingTime = disableTime;
    }

    void Update() {
        if(remainingTime <= 0) {
            gameObject.SetActive(false);
        }
        else {
            remainingTime -= Time.deltaTime;
        }
    }
}