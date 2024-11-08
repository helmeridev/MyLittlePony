using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomEventManager : MonoBehaviour
{
    public enum RandomEvent {
        Robber
    }
    public RandomEvent randomEvent;
    [SerializeField] GameObject robberPrefab;
    [SerializeField] Transform player;
    [SerializeField] Vector2 activateTresholds;
    private float activateTreshold = 50;
    private float currentDistance;
    bool isEventReady;
    bool didSpawn;

    void Start() {
        activateTreshold = Random.Range(activateTresholds.x, activateTresholds.y);
    }

    void Update() {
        EventLogic();
    }

    void EventLogic() {
        currentDistance = Vector3.Distance(new Vector3(0, player.position.y, player.position.z), player.position);

        if(currentDistance > activateTreshold) {
            isEventReady = true;
        }
        
        if(isEventReady && !didSpawn) {
            switch (randomEvent) {
                case RandomEvent.Robber:
                    Vector3 spawnPos = new Vector3(player.position.x - 30, -3.5f, player.position.z);
                    GameObject newPrefab = Instantiate(robberPrefab, spawnPos, robberPrefab.transform.rotation);
                    RandomEventObject eventObject = newPrefab.GetComponent<RandomEventObject>();
                    eventObject.randomEvent = RandomEventObject.RandomEvent.Robber;
                    eventObject.player = player;
                    break;
                default:
                    break;
            }

            isEventReady = false;
            didSpawn = true;
        }
    }
}
