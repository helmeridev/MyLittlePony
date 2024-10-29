using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndDoor : MonoBehaviour
{
    public enum TriggerEvent {
        NextLevel,
        Plankton
    }
    public TriggerEvent triggerEvent;

    [SerializeField] int planktonIndex;

    void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player")) {
            if(triggerEvent == TriggerEvent.NextLevel) LevelManager.NextLevel();
            else if(triggerEvent == TriggerEvent.Plankton) LevelManager.SetLevel(planktonIndex);
        }
    }
}
