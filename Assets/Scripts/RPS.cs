using System.Collections;
using System.Collections.Generic;
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
                if (rpsPlayer == 1 && rpsGame == 3)
                {
                    win();
                }
                if (rpsPlayer == 2 && rpsGame == 1)
                {
                    win();
                }
                if (rpsPlayer == 3 && rpsGame == 2)
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

        if (taxManager != null)
        {
            taxManager.AddMoney(Mathf.Round(Random.Range(15, 20) * 10) * 0.1f);
        }

        eventManager.rpsUI.SetActive(false);
        Destroy(eventManager.robberInstance);
        remainingTime = maxTime;
        gameloop = false;
    }

    public void lose()
    {
        if (taxManager != null)
        {
            taxManager.AddMoney(Mathf.Round(Random.Range(-20, -15) * 10) * 0.1f);
        }
        eventManager.rpsUI.SetActive(false);
        Destroy(eventManager.robberInstance);
        remainingTime = maxTime;
        gameloop = false;
    }

    public void draw()
    {
        eventManager.rpsUI.SetActive(false );
        Destroy(eventManager.robberInstance);
        remainingTime = maxTime;
        gameloop = false;
    }
}

