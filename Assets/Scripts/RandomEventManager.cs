using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomEventManager : MonoBehaviour
{
    public enum RandomEvent {
        Robber
    }
    public RandomEvent randomEvent;
    public bool isEventActive;
    [SerializeField] float activateTreshold = 100;
    [SerializeField] Transform player;
    [SerializeField] float robberSpeed = 7f;

    void Update() {
        EventLogic();
    }

    void EventLogic() {
        
        
        switch (randomEvent) {
            case RandomEvent.Robber:
                RobberEvent();
                break;
        }
    }

    //Events
    void RobberEvent() {
        if(isEventActive) {
            Vector3 playerPos = new Vector3(player.position.x, transform.position.y, transform.position.z);

            transform.position = Vector3.MoveTowards(transform.position, playerPos, robberSpeed * Time.deltaTime);
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")) {
            Debug.Log("Robber event");
            //Call rock paper scissors
        }
    }
}
