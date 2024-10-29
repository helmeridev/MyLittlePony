using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TaxDoor : MonoBehaviour
{
    public float cash;
    [SerializeField] Vector2 cashRange = new Vector2(1, 8);

    [Header("Dialogue")]
    public List<string> dialogue = new List<string>();
    public int diaNumber = 0;
    public bool didCollect;

    [Header("Enter")]
    [SerializeField] Animator cAnimator;

    public void GUI(GameObject guiObject, TMP_Text text, TaxManager tax, GameObject dialogueUI) {
        if(Input.GetKeyDown(KeyCode.E) && !guiObject.activeInHierarchy) {
            UIManager.OpenGUI(guiObject);
            diaNumber = 0;
            DoorEnter(dialogueUI.GetComponent<Animator>());
        }
        if(Input.GetKeyUp(KeyCode.Escape)) {
            UIManager.CloseGUI(guiObject);
            diaNumber = 0;
        }
        if(GUI_NextInput() && guiObject.activeSelf) diaNumber += 1;
        
        if(diaNumber > dialogue.Count - 1) {
            UIManager.CloseGUI(guiObject);
            diaNumber = 0;
            if(!didCollect) tax.money += cash;
            didCollect = true;
        }

        if(didCollect) {
            text.text = "You already took my money.";
            if(GUI_NextInput()) diaNumber = dialogue.Count;
        }
        else {
            text.text = dialogue[diaNumber];
        }
    }

    void Start() {
        cash = Mathf.Round(Random.Range(cashRange.x, cashRange.y + 1) * 10) * 0.1f;
    }

    void DoorEnter(Animator animator) {
        Debug.Log("Anim play");
        animator.Play("DoorEnter");
    }

    bool GUI_NextInput() {
        if( Input.GetKeyDown(KeyCode.Return) ||
            Input.GetKeyDown(KeyCode.Space) || 
            Input.GetMouseButtonDown(0)) {
            return true;
        }
        else return false;
    }
}
