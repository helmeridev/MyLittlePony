using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Gambling : MonoBehaviour
{
    [SerializeField] Rigidbody2D wheel;
    [SerializeField] float spinSpeed = 5;
    [SerializeField] float stopVelocityLimit;
    public float wheelAngle;
    public int winMultiplier;
    private int wheelLogicLoopNumber;
    private bool startedSpin;
    public string debug;

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
        wheelAngle = wheel.transform.eulerAngles.z;

        WheelLogic();
        WheelSpin();
    }

    public void WheelSpin() {
        if(Input.GetKeyDown(KeyCode.G) && wheelMode == WheelMode.idle) {
            wheelMode = WheelMode.spinning;
        }

        if(wheelMode == WheelMode.spinning && startedSpin == false) {
            Debug.Log("Started spin");
            wheel.AddTorque(spinSpeed, ForceMode2D.Impulse);
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
                debug = NormalizeAngle(wheelAngle) + " " + NormalizeAngle(prize.startAngle) + " " + NormalizeAngle(prize.endAngle);
                if(DidHit(wheelAngle, prize.startAngle, prize.endAngle)) {
                    winMultiplier = prize._winMultiplier;
                    Debug.Log("Hit");
                    startedSpin = false;
                    wheelMode = WheelMode.idle;
                }

                Debug.Log(NormalizeAngle(prize.endAngle));
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
        return (angle + 360) % 360;
    }
}
