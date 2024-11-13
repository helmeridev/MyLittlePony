using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointCollider : MonoBehaviour
{
    bool isColliding;

    void OnTriggerStay2D(Collider2D other) {
        isColliding = true;
    }
    void OnTriggerExit2D(Collider2D other) {
        isColliding = false;
    }

    public bool IsColliding() {
        if(isColliding) return true;
        else return false;
    }
}
