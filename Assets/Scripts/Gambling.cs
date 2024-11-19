using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Gambling : MonoBehaviour
{
    TaxManager tax;

    [Header("References")]
    [SerializeField] Rigidbody2D wheel;
    public GameObject gambleUI;
    public TMP_InputField moneyInputField;
    [SerializeField] List<GameObject> historyObjects = new List<GameObject>();
    [SerializeField] List<GameObject> historyList = new List<GameObject>();
    [SerializeField] List<Transform> historyPos = new List<Transform>();

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
        wheelAngle = wheel.transform.eulerAngles.z;

        WheelLogic();
    }

    //Gamble UI
    public void GUI(TaxManager newTax) {
        if(wheelMode == WheelMode.idle) {
            tax = newTax;

            if(Input.GetKeyDown(KeyCode.E)) UIManager.OpenGUI(gambleUI);
            if(Input.GetKeyUp(KeyCode.Escape)) UIManager.CloseGUI(gambleUI);
        }
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

    void UpdateHistory(float prizeAmount) {
        for(int i = historyList.Count - 1; i >= 0; i--) {
            if(historyList[i] != null) {
                if(i == historyList.Count - 1) {
                    Destroy(historyList[i]);
                    historyList[i] = null;
                }

                if(i < historyList.Count - 1) {
                    historyList[i + 1] = historyList[i];
                    historyList[i + 1].transform.position = historyPos[i + 1].position;

                    if(i == 0) {
                        SetHistory(prizeAmount, i);
                    }
                }
            }
            else {
                if(i == 0) {
                    SetHistory(prizeAmount, i);
                }
            }
        }
    }
    void SetHistory(float prizeAmount, int loopNumber) {
        if(prizeAmount == 0) {
            historyList[loopNumber] = Instantiate(historyObjects[0], historyPos[0].position, historyObjects[0].transform.rotation, gambleUI.transform);
        }
        else if(prizeAmount == 2) {
            historyList[loopNumber] = Instantiate(historyObjects[1], historyPos[0].position, historyObjects[1].transform.rotation, gambleUI.transform);
        }
        else if(prizeAmount == 3) {
            historyList[loopNumber] = Instantiate(historyObjects[2], historyPos[0].position, historyObjects[2].transform.rotation, gambleUI.transform);
        }
        else if(prizeAmount == 7) {
            historyList[loopNumber] = Instantiate(historyObjects[3], historyPos[0].position, historyObjects[3].transform.rotation, gambleUI.transform);
        }
    }

    //Method for wheel spin
    public void WheelSpin() {
        if(tax.money >= moneyInput && moneyInput > 0 && wheelMode == WheelMode.idle && startedSpin == false) {          
            Debug.Log("Started spin");

            UIManager.CloseGUI(gambleUI);

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
                    UpdateHistory(winMultiplier);
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
