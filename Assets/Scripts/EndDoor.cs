using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndDoor : MonoBehaviour
{
    public enum TriggerEvent {
        NextLevel,
        Boss,
        MainMenu,
        FinalCutscene
    }
    public TriggerEvent triggerEvent;

    [SerializeField] int planktonIndex;
    [SerializeField] int finalCutsceneIndex;

    void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player")) {
            if(triggerEvent == TriggerEvent.NextLevel) LevelManager.NextLevel();
            else if(triggerEvent == TriggerEvent.Boss) LevelManager.SetLevel(planktonIndex);
            else if(triggerEvent == TriggerEvent.MainMenu) LevelManager.SetLevel(0);
            else if(triggerEvent == TriggerEvent.FinalCutscene) LevelManager.SetLevel(finalCutsceneIndex);
        }
    }
}
