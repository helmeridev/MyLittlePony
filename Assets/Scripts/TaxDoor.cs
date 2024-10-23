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
    private int diaNumber = 0;
    private bool didCollect;

    public void GUI(GameObject guiObject, TMP_Text text, TaxManager tax) {
        if(Input.GetKeyDown(KeyCode.E)) OpenGUI(guiObject);
        if(Input.GetKeyDown(KeyCode.Escape)) CloseGUI(guiObject);
        if(GUI_NextInput()) diaNumber += 1;
        
        if(diaNumber > dialogue.Count - 1) {
            CloseGUI(guiObject);
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
    void OpenGUI(GameObject guiObject) {
        Movement.canMove = false;
        diaNumber = 0;
        guiObject.SetActive(true);
    }
    void CloseGUI(GameObject guiObject) {
        Movement.canMove = true;
        guiObject.SetActive(false);
        diaNumber = 0;
    }

    void Start() {
        cash = Mathf.Round(Random.Range(cashRange.x, cashRange.y + 1) * 10) * 0.1f;
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
