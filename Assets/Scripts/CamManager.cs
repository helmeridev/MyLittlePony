using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamManager : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private float parallaxIntensity;


    void FixedUpdate() {
        Vector3 targetPos = new Vector3(player.transform.position.x, transform.position.y, -10);
        transform.position = Vector3.Lerp(transform.position, targetPos, parallaxIntensity * Time.deltaTime);

        CamVertical();
    }

    void CamVertical() {
        if(player.transform.position.y > 0.2f) {
            transform.position = new Vector3(transform.position.x, player.transform.position.y, -10);
        }
    }
}
