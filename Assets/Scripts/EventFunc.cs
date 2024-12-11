using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventFunc : MonoBehaviour
{
    public void PlayOpenSound() {
        AudioManager.instance.Play("DoorOpen");
    }
}
