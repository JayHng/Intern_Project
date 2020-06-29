using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleObjController : MonoBehaviour
{
    [SerializeField] private float maxHP;
    private float currentHP;
    private Player player;
    private GameObject aliveGO, brokenTopGO, brokenBotGO;
    private Rigidbody2D rbAlive, rbBrokenTop, rbBrokenBot;
    private Animator aliveAnim;

    private void Start() {
        currentHP =  maxHP;

        player = GameObject.Find("Player").GetComponent<Player>();
        aliveGO = transform.Find("Vase").gameObject;
        brokenTopGO = transform.Find("Broken Top").gameObject;
        brokenBotGO = transform.Find("Broken Bottom").gameObject;

        aliveAnim = aliveGO.GetComponent<Animator>();
        rbAlive = aliveGO.GetComponent<Rigidbody2D>();
        rbBrokenTop = brokenTopGO.GetComponent<Rigidbody2D>();
        rbBrokenBot = brokenBotGO.GetComponent<Rigidbody2D>();

        aliveGO.SetActive(true);
        brokenTopGO.SetActive(false);
        brokenBotGO.SetActive(false);
    }

    private void Damage(float amount){
        currentHP -= amount;
    }
}
