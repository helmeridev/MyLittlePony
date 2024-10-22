using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TaxManager : MonoBehaviour
{
    TaxDoor taxDoor;
    public float money;

    [SerializeField] GameObject dialogueUI;
    [SerializeField] TMP_Text dialogueText;

    [SerializeField] TMP_Text moneyText;

    void Update() {
        if(taxDoor) {
            taxDoor.GUI(dialogueUI, dialogueText, this);
        }

        moneyText.text = money.ToString();
    }

    void OnTriggerEnter2D(Collider2D other) {
        if(taxDoor = other.GetComponent<TaxDoor>()) {}
    }
    void OnTriggerExit2D(Collider2D other) {
        if(other.GetComponent<TaxDoor>()) {
            taxDoor = null;
        }
    }
}
