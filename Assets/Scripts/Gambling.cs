using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Gambling : MonoBehaviour
{
    TaxManager tax;

    [SerializeField] Rigidbody2D wheel;
    [SerializeField] Vector2 spinSpeed;
    [SerializeField] float stopVelocityLimit;
    public float wheelAngle;
    public int winMultiplier;
    private bool startedSpin;

    [System.Serializable]
    public struct Prize {
        public int _winMultiplier;
        public float startAngle;
        public float endAngle;
    }
    public Prize[] prizes;

    public enum WheelMode {
        idle, 
        spinning, 
        reward,
    }
    public WheelMode wheelMode;


    void Update() {
        if(!UIManager.isPaused) {
            wheelAngle = wheel.transform.eulerAngles.z;

            WheelLogic();
        }
    }

    public void WheelSpin(TaxManager newTax) {
        tax = newTax;

        if(Input.GetKeyDown(KeyCode.E) && wheelMode == WheelMode.idle) {
            if(tax.money >= 5) {
                tax.money -= 5;
                wheelMode = WheelMode.spinning;
            }
        }

        if(wheelMode == WheelMode.spinning && startedSpin == false) {
            Debug.Log("Started spin");
            wheel.AddTorque(Random.Range(spinSpeed.x, spinSpeed.y), ForceMode2D.Impulse);
            startedSpin = true;
        }
    }

    void WheelLogic() {
        foreach(Prize prize in prizes) {
            if(wheelMode == WheelMode.spinning) {
                if(wheel.angularVelocity < stopVelocityLimit && startedSpin == true) {
                    wheel.angularVelocity = 0;
                    wheelMode = WheelMode.reward;
                }
            }
            else if(wheelMode == WheelMode.reward) {
                if(DidHit(wheelAngle, prize.startAngle, prize.endAngle)) {
                    winMultiplier = prize._winMultiplier;
                    tax.money += 5 * winMultiplier;
                    Debug.Log("Hit");
                    startedSpin = false;
                    wheelMode = WheelMode.idle;
                }
            }
        }
    }

    bool DidHit(float angle, float newStartAngle, float newEndAngle) {
        angle = NormalizeAngle(angle);
        newStartAngle = NormalizeAngle(newStartAngle);
        newEndAngle = NormalizeAngle(newEndAngle);

        if(angle >= newStartAngle && angle < newEndAngle) {
            return true;
        }
        else return false;
    }

    float NormalizeAngle(float angle) {
        if(angle == 360f) {
            return 360f;
        }
        else {
            return (angle + 360) % 360;
        }
    }
}
