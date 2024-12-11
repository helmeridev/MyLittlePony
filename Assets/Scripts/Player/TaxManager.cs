using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class TaxManager : MonoBehaviour
{
    TaxDoor taxDoor;
    Gambling gambling;
    public static float staticMoney;
    public float money;

    [SerializeField] GameObject dialogueUI;
    [SerializeField] TMP_Text dialogueText;
    [SerializeField] Image ownerImage;

    [SerializeField] TMP_Text moneyText;

    void Start() {
        //FOR DEVELOPMENT PURPOSES
        // staticMoney = 999;
        
        
        money = staticMoney;
    }

    void Update()
    {
        if(staticMoney != money) staticMoney = money;

        if (taxDoor)
        {
            taxDoor.GUI(dialogueUI, dialogueText, this, dialogueUI, ownerImage);
        }
        if (gambling)
        {
            gambling.GUI(this);
        }

        moneyText.text = money.ToString("0.00");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<TaxDoor>()) taxDoor = other.GetComponent<TaxDoor>();
        if (other.GetComponent<Gambling>()) gambling = other.GetComponent<Gambling>();
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<TaxDoor>())
        {
            UIManager.CloseGUI(dialogueUI);
            taxDoor = null;
        }
        if (other.GetComponent<Gambling>())
        {
            gambling = null;
        }
    }

    public void AddMoney(float amount)
    {
        money += amount;
        AudioManager.instance.Play("KaChing");
        if(money < 0) money = 0;
        moneyText.text = money.ToString("0.00");
    }
    public void AddMoneyNoSFX(float amount)
    {
        money += amount;
        if(money < 0) money = 0;
        moneyText.text = money.ToString("0.00");
    }
    public void SubMoney(float amount)
    {
        money -= amount;
        if(money < 0) money = 0;
        moneyText.text = money.ToString("0.00");
    }
}
