using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxController : MonoBehaviour
{
    [SerializeField] Transform cam;
    [System.Serializable]
    public struct Background {
        [Range(0.01f, 0.05f)]
        public float speed;
        public Material mat;
    }
    public Background[] backgrounds;

    void Start() {
        int backCount = transform.childCount;

        for(int i = 0; i < backCount; i++) {
            backgrounds[i].mat = transform.GetChild(i).gameObject.GetComponent<Renderer>().material;
        }
    }

    void LateUpdate() {
        transform.position = new Vector3(cam.position.x, transform.position.y, transform.position.z);

        float distance = cam.position.x;

        for(int i = 0; i < backgrounds.Length; i++) {
            backgrounds[i].mat.SetTextureOffset("_MainTex", new Vector2(distance, 0) * backgrounds[i].speed);
        }
    }
}