using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class RPS : MonoBehaviour
{
    [SerializeField] TaxManager taxManager;
    RandomEventManager eventManager;
    public bool gameloop = false;
    int rpsPlayer = 0;
    int rpsGame = 0;

    [SerializeField] float maxTime = 5;
    private float remainingTime;



    // Start is called before the first frame update
    void Start()
    {
        eventManager = GetComponent<RandomEventManager>();
        remainingTime = maxTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameloop)
        {
            RPSgame();
        }
        
    }

    void RPSgame()
    {
        remainingTime -= Time.deltaTime;
        if (remainingTime > 0) 
        {
            eventManager.rpsUI.SetActive(true);
        }

        if (remainingTime < 0)
        {
            if (rpsGame == 0)
            {
                if (rpsPlayer == 0) { rpsPlayer = (int)(Mathf.Round(Random.Range(1, 4) * 10) * 0.1f); }
                rpsGame = (int)(Mathf.Round(Random.Range(1, 4) * 10) * 0.1f);

                if (rpsGame == rpsPlayer)
                {
                    draw();
                }
                else if (rpsPlayer == 1 && rpsGame == 3)
                {
                    win();
                }
                else if (rpsPlayer == 2 && rpsGame == 1)
                {
                    win();
                }
                else if (rpsPlayer == 3 && rpsGame == 2)
                {
                    win();
                }
                else
                {
                    lose();
                }
            }
        }
    }

    public void chooseRock()
    {
        rpsPlayer = 1;
        remainingTime = 0;

    }
    public void choosePaper()
    {
        rpsPlayer = 2;
        remainingTime = 0;
    }

    public void chooseScissors()
    {
        rpsPlayer = 3;
        remainingTime = 0;
    }

    public void win()
    {
        float moneyDifference = Mathf.Round(Random.Range(10, 15) * 10) * 0.1f;
        
        taxManager.AddMoney(moneyDifference);

        eventManager.rpsUI.SetActive(false);
        eventManager.rpsWinUI.SetActive(true);
        eventManager.rpsWinUI.GetComponentInChildren<TMP_Text>().text = "You gained " + moneyDifference + " money!";
        Destroy(eventManager.robberInstance);
        remainingTime = maxTime;
        gameloop = false;
    }

    public void lose()
    {
        float moneyDifference = Mathf.Round(Random.Range(-20, -15) * 10) * 0.1f;
        
        taxManager.AddMoney(moneyDifference);

        eventManager.rpsUI.SetActive(false);
        eventManager.rpsLoseUI.SetActive(true);
        eventManager.rpsLoseUI.GetComponentInChildren<TMP_Text>().text = "You lost " + moneyDifference + " money!";
        Destroy(eventManager.robberInstance);
        remainingTime = maxTime;
        gameloop = false;
    }

    public void draw()
    {
        eventManager.rpsDrawUI.SetActive(true);
        eventManager.rpsDrawUI.GetComponentInChildren<TMP_Text>().text = "You keep your money!";
        eventManager.rpsUI.SetActive(false);
        Destroy(eventManager.robberInstance);
        remainingTime = maxTime;
        gameloop = false;
    }
}

