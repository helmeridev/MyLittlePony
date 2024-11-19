using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.UI;

public class BossManager : MonoBehaviour
{
    [Header("Body references")]
    public Transform sprite;
    public GameObject head;
    public GameObject body;
    public GameObject armLeft;
    public GameObject armRight;
    public GameObject legLeft;
    public GameObject legRight;

    [Header("Other references")]
    public Transform player;
    [HideInInspector] public TaxManager taxManager;
    public GameObject attackCollider;
    [HideInInspector] public Animator animator;
    public GameObject finishDoor;
    [SerializeField] GameObject bossBar;
    [SerializeField] Slider bossBarSlider;
    [SerializeField] ParticleSystem leaveParticles;

    [Header("Properties")]
    public float maxHealth;
    [HideInInspector] public float currentHealth;
    [SerializeField] float leaveSpeed;
    bool isdeathanim;

    [HideInInspector] public bool isAttacking;
    private float bossBarAfter0Timer = 2;

    public PointCollider GetPointCollider(GameObject colliderObject) {
        if(colliderObject.GetComponent<PointCollider>()) return colliderObject.GetComponent<PointCollider>();
        else {
            Debug.LogError("Cannot get PointCollider from " + colliderObject.name + " because it does not have one!");
            return null;
        }
    }

    void Start() {
        taxManager = player.gameObject.GetComponent<TaxManager>();
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
    }

    void Update() {
        BossLeave();
        BossDeath();
        BossBarUpdate();
    }

    void BossBarUpdate() {
        if(currentHealth <= 0) {
            bossBarAfter0Timer -= Time.deltaTime;

            if(bossBarAfter0Timer <= 0) {
                bossBar.SetActive(false);
            }
        }

        bossBarSlider.value = currentHealth / maxHealth;
    }

    void BossLeave() {
        if(taxManager.money <= 0) {
            if(!leaveParticles.isPlaying) {
                bossBar.SetActive(false);leaveParticles.Play();
            }

            if(transform.position.y > 40) {
                currentHealth = 0;
            }
            else {
                transform.position += new Vector3(0, leaveSpeed, 0) * Time.deltaTime;
            }
        }
    }

    void BossDeath()
    {
        if (currentHealth <= 0 && !isdeathanim) {
            animator.Play("BossDeathAnim");
            isdeathanim = true;
        }
    }

}
