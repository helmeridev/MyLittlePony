using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CutsceneManager : MonoBehaviour
{
    [SerializeField] GameObject player;
    private TaxManager tax;

    [SerializeField] PlayableDirector director;

    void Start() {
        tax = player.GetComponent<TaxManager>();

        PlayCutscene();
    }

    void PlayCutscene() {
        director.Play();
    }

    void Update() {
        if(director.state != PlayState.Playing) {
            LevelManager.SetLevel(0);
        }
    }
}
