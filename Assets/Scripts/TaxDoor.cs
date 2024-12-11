using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TaxDoor : MonoBehaviour
{
    public float cash;
    [SerializeField] Vector2 cashRange = new Vector2(1, 8);
    [SerializeField] ParticleSystem cashParticles;

    [Header("Dialogue")]
    public List<string> dialogue = new List<string>();
    public List<string> robDialogue = new List<string>();
    public string afterDialogue = "You already took my money.";
    public int diaNumber = 0;
    public bool didCollect;
    public bool isSpecial;

    [Header("Enter")]
    [SerializeField] Animator cAnimator;

    private int houseFate = -1;

    public void GUI(GameObject guiObject, TMP_Text text, TaxManager tax, GameObject dialogueUI) {
        if(Input.GetKeyDown(KeyCode.E) && !guiObject.activeInHierarchy) {
            UIManager.OpenGUI(guiObject);
            diaNumber = 0;
            DoorEnter(dialogueUI.GetComponent<Animator>());

            if(IsRobbery() && didCollect) {
                UIManager.CloseGUI(guiObject);
                diaNumber = 0;
            }
        }
        if(Input.GetKeyUp(KeyCode.Escape) && !isSpecial) {
            UIManager.CloseGUI(guiObject);
            diaNumber = 0;
        }
        if(GUI_NextInput() && guiObject.activeSelf) diaNumber += 1;
        
        if(!IsRobbery()) {
            if(diaNumber > dialogue.Count - 1) {
                UIManager.CloseGUI(guiObject);
                diaNumber = 0;
                if(!didCollect) {
                    tax.AddMoney(cash);
                    cashParticles.Play();
                }
                didCollect = true;
            }
        }
        else {
            if(diaNumber > robDialogue.Count - 1) {
                UIManager.CloseGUI(guiObject);
                diaNumber = 0;
                if(!didCollect) tax.SubMoney(cash);
                didCollect = true;
            }
        }

        if(didCollect) {
            text.text = afterDialogue;
            if(GUI_NextInput()) diaNumber = dialogue.Count;
        }
        else {
            if(!IsRobbery()) {
                text.text = dialogue[diaNumber];
            }
            else {
                text.text = robDialogue[diaNumber];
            }
        }
    }

    void Start() {
        cash = Mathf.Round(Random.Range(cashRange.x, cashRange.y + 1) * 10) * 0.1f;
        if(isSpecial) {
            houseFate = Random.Range(0, 2);
        }
    }

    void DoorEnter(Animator animator) {
        Debug.Log("Anim play");
        animator.Play("DoorEnter");
    }

    bool IsRobbery() {
        if(houseFate == 0) {
            return true;
        }
        else {
            return false;
        }
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
