using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Gambling : MonoBehaviour
{
    TaxManager tax;

    [SerializeField] Rigidbody2D wheel;
    [SerializeField] GameObject gambleUI;
    [SerializeField] TMP_InputField moneyInputField;

    [Header("Properties")]
    [SerializeField] Vector2 spinSpeed;
    [SerializeField] float stopVelocityLimit;

    [System.Serializable]
    public struct Prize {
        public int _winMultiplier;
        public float startAngle;
        public float endAngle;
    }
    public Prize[] prizes;

    [Header("Info")]
    public float wheelAngle;
    public int winMultiplier;
    public float moneyInput;
    private bool startedSpin;

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

    //Gamble UI
    public void GUI(TaxManager newTax) {
        if(wheelMode == WheelMode.idle) {
            tax = newTax;

            if(Input.GetKeyDown(KeyCode.E)) OpenGUI(gambleUI);
            if(Input.GetKeyDown(KeyCode.Escape)) CloseGUI(gambleUI);
        }
    }
    void OpenGUI(GameObject guiObject) {
        Movement.canMove = false;
        guiObject.SetActive(true);
    }
    void CloseGUI(GameObject guiObject) {
        Movement.canMove = true;
        guiObject.SetActive(false);
    }

    public void GrabInputField() {
        if(wheelMode == WheelMode.idle) {
            string input = moneyInputField.text;

            if(float.TryParse(input, out float newInput)) {
                moneyInput = newInput;
            }
            else {
                Debug.LogError("Invalid input, please enter a number");
            }
        }
    }

    //Method for wheel spin
    public void WheelSpin() {
        if(tax.money >= moneyInput && moneyInput > 0 && wheelMode == WheelMode.idle && startedSpin == false) {          
            Debug.Log("Started spin");

            CloseGUI(gambleUI);

            tax.money -= moneyInput;
            wheelMode = WheelMode.spinning;
            wheel.AddTorque(Random.Range(spinSpeed.x, spinSpeed.y), ForceMode2D.Impulse);
            startedSpin = true;
        }
    }

    //Wheel logic, which prize was hit and how much reward etc
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
                    tax.money += moneyInput * winMultiplier;
                    Debug.Log("Hit");
                    startedSpin = false;
                    wheelMode = WheelMode.idle;
                }
            }
        }
    }

    //Check if the wheel hit a specific area between angles
    bool DidHit(float angle, float newStartAngle, float newEndAngle) {
        angle = NormalizeAngle(angle);
        newStartAngle = NormalizeAngle(newStartAngle);
        newEndAngle = NormalizeAngle(newEndAngle);

        if(angle >= newStartAngle && angle < newEndAngle) {
            return true;
        }
        else return false;
    }

    //Normalize the wheel rotation angle float
    float NormalizeAngle(float angle) {
        if(angle == 360f) {
            return 360f;
        }
        else {
            return (angle + 360) % 360;
        }
    }
}
