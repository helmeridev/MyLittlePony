using TMPro;
using UnityEngine;

public class RPS : MonoBehaviour
{
    [SerializeField] Sprite rockSprite;
    [SerializeField] Sprite paperSprite;
    [SerializeField] Sprite scissorsSprite;

    [SerializeField] TaxManager taxManager;
    RandomEventManager eventManager;
    public bool gameloop = false;
    int rpsPlayer = 0;
    int rpsGame = 0;

    public float maxTime = 5;
    [HideInInspector] public float remainingTime;



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
        eventManager.rpsTimerSlider.value = remainingTime / maxTime;
        if (remainingTime > 0 && !eventManager.rpsUI.activeSelf) 
        {
            UIManager.OpenGUI(eventManager.rpsUI);
        }

        if (remainingTime < 0)
        {
            if (rpsGame == 0)
            {
                if (rpsPlayer == 0) { rpsPlayer = (int)(Mathf.Round(Random.Range(1, 4) * 10) * 0.1f); }
                rpsGame = (int)(Mathf.Round(Random.Range(1, 4) * 10) * 0.1f);

                if(rpsGame == 1) eventManager.winResultSpriteAI.sprite = rockSprite;
                if(rpsGame == 2) eventManager.winResultSpriteAI.sprite = paperSprite;
                if(rpsGame == 3) eventManager.winResultSpriteAI.sprite = scissorsSprite;

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
        eventManager.winResultSpritePlayer.sprite = rockSprite;
        remainingTime = 0;

    }
    public void choosePaper()
    {
        rpsPlayer = 2;
        eventManager.winResultSpritePlayer.sprite = paperSprite;
        remainingTime = 0;
    }

    public void chooseScissors()
    {
        rpsPlayer = 3;
        eventManager.winResultSpritePlayer.sprite = scissorsSprite;
        remainingTime = 0;
    }

    public void win()
    {
        float moneyDifference = Mathf.Round(Random.Range(10, 15) * 10) * 0.1f;
        
        taxManager.AddMoney(moneyDifference);

        UIManager.CloseGUI(eventManager.rpsUI);
        eventManager.rpsWinUI.SetActive(true);
        eventManager.rpsWinUI.GetComponentInChildren<TMP_Text>().text = "You won and gained " + moneyDifference + " money!";
        eventManager.rpsResultFeedback.SetActive(true);
        Destroy(eventManager.robberInstance);
        remainingTime = maxTime;
        gameloop = false;
    }

    public void lose()
    {
        float moneyDifference = Mathf.Round(Random.Range(-20, -15) * 10) * 0.1f;
        
        taxManager.AddMoney(moneyDifference);

        UIManager.CloseGUI(eventManager.rpsUI);
        eventManager.rpsLoseUI.SetActive(true);
        eventManager.rpsLoseUI.GetComponentInChildren<TMP_Text>().text = "You lost! " + moneyDifference + " money!";
        eventManager.rpsResultFeedback.SetActive(true);
        Destroy(eventManager.robberInstance);
        remainingTime = maxTime;
        gameloop = false;
    }

    public void draw()
    {
        eventManager.rpsDrawUI.SetActive(true);
        eventManager.rpsDrawUI.GetComponentInChildren<TMP_Text>().text = "It's a draw! You keep your money!";
        eventManager.rpsResultFeedback.SetActive(true);
        UIManager.CloseGUI(eventManager.rpsUI);
        Destroy(eventManager.robberInstance);
        remainingTime = maxTime;
        gameloop = false;
    }
}

