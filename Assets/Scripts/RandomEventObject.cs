using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RandomEventObject : MonoBehaviour
{
    public RandomEventManager eventManager;
    public RPS rps;
    public enum RandomEvent {
        Robber
    }
    public RandomEvent randomEvent;
    public Transform player;
    [SerializeField] float robberSpeed = 7f;

    void Update() {
        RobberEvent();
    }

    //Events
    void RobberEvent() {
        if(randomEvent == RandomEvent.Robber) {
            Vector3 playerPos = new Vector3(player.position.x, transform.position.y, transform.position.z);

            transform.position = Vector3.MoveTowards(transform.position, playerPos, robberSpeed * Time.deltaTime);
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")) {
            Debug.Log("Robber event");
            rps.gameloop = true;
        }
    }
}
