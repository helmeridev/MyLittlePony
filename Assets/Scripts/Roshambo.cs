using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    bool gameloop = true;
    int rpsPlayer = 0;
    int rpsGame = 0;

    [SerializeField] float maxTime = 5;
    private float remainingTime;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RPS();
    }

    void RPS()
    {
        remainingTime -= Time.deltaTime;

        if (remainingTime < 0 ) {
            if (rpsGame == 0) {
                if (rpsPlayer == 0) { rpsPlayer = (int)(Mathf.Round(Random.Range(1, 4) * 10) * 0.1f); }
                rpsGame = (int)(Mathf.Round(Random.Range(1 , 4 ) * 10) * 0.1f);

                if (rpsGame == rpsPlayer)
                {
                    //tasapeli
                }
                if (rpsPlayer == 1 &&  rpsGame == 3) 
                {
                    //win
                }
                if (rpsPlayer == 2 && rpsGame == 1) 
                {
                    //win
                }
                if (rpsPlayer == 3 && rpsGame == 2)
                {
                    //win
                }
                else
                {
                    //lose
                }
            }
        }
    }

    public void chooseRock()
    {
        rpsPlayer = 1;

    }
    public void choosePaper()
    {
        rpsPlayer = 2;
    }

    public void chooseScissors()
    {
        rpsPlayer = 3;
    }
}
