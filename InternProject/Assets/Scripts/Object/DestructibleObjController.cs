using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleObjController : MonoBehaviour
{
    [SerializeField] private float maxHP, knockbackSpeedX, knockbackSpeedY, knockbackDuration, knockbackDeathSpeedY, knockbackDeathSpeedX, deathTorque;
    [SerializeField] private bool applyKnockback;
    [SerializeField] private GameObject hitParticle;
    [SerializeField] private float currentHP, knockbackStart;
    private Controller2D controller;
    private bool playerOnLeft, knockback;

    private int playerFacingDirection;
    private GameObject aliveGO, brokenTopGO, brokenBotGO;
    private Rigidbody2D rbAlive, rbBrokenTop, rbBrokenBot;
    private Animator aliveAnim;

    private void Start() {
        currentHP =  maxHP;

        controller = GetComponent<Controller2D>();
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

    private void Update() {
        CheckKnockback();

        if(currentHP <= 0.0f){
            Die();
            Debug.Log("Die");
        }
        if(applyKnockback && currentHP > 0.0f){
            Knockback();
            Debug.Log("Knockback");
        }
    }
    private void Damage(AttackDetails attackDetails){
        currentHP -= attackDetails.damageAmount;

        Instantiate(hitParticle, aliveAnim.transform.position, Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f)));

        if(!controller.faceright){
            playerOnLeft = true;
            playerFacingDirection = 1;
            Debug.Log("Left");

        }else{
            playerOnLeft = false;
            playerFacingDirection = -1;
            Debug.Log("Right");
        }
        aliveAnim.SetBool("playerOnLeft", playerOnLeft);
        aliveAnim.SetTrigger("Damage");
    }

    private void Knockback(){
        Debug.Log("Knockback");
        knockback = true;
        knockbackStart = Time.time;
        rbAlive.velocity = new Vector2(knockbackSpeedX * playerFacingDirection, knockbackSpeedY);
    }
    public void CheckKnockback(){
        if(Time.time >= knockbackStart + knockbackDuration && knockback){
            //Debug.Log("Check Knockback");
            knockback = false;
            rbAlive.velocity = new Vector2(0.0f, rbAlive.velocity.y);
        }
    }
    private void Die(){
        aliveGO.SetActive(false);
        brokenTopGO.SetActive(true);
        brokenBotGO.SetActive(true);

        brokenTopGO.transform.position = aliveGO.transform.position;
        brokenBotGO.transform.position = aliveGO.transform.position;

        Debug.Log("knock");
        rbBrokenBot.velocity = new Vector2(knockbackSpeedX * playerFacingDirection, knockbackSpeedY);
        rbBrokenTop.velocity = new Vector2(knockbackDeathSpeedX * playerFacingDirection, knockbackDeathSpeedY);
        rbBrokenTop.AddTorque(deathTorque * -playerFacingDirection, ForceMode2D.Impulse);
    }
}
