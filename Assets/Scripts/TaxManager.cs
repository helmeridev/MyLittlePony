using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TaxManager : MonoBehaviour
{
    TaxDoor taxDoor;
    Gambling gambling;
    public float money;

    [SerializeField] GameObject dialogueUI;
    [SerializeField] TMP_Text dialogueText;

    [SerializeField] TMP_Text moneyText;

    void Update() {
        if(taxDoor) {
            taxDoor.GUI(dialogueUI, dialogueText, this);
        }
        if(gambling) {
            gambling.GUI(this);
        }

        moneyText.text = money.ToString("0.00");
    }

    void OnTriggerEnter2D(Collider2D other) {
        if(taxDoor = other.GetComponent<TaxDoor>()) {}
        if(gambling = other.GetComponent<Gambling>()) {}
    }
    void OnTriggerExit2D(Collider2D other) {
        if(other.GetComponent<TaxDoor>()) {
            taxDoor = null;
        }
        if(other.GetComponent<Gambling>()) {
            gambling = null;
        }
    }
}
