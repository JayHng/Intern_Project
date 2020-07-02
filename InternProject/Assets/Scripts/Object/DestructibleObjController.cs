using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleObjController : MonoBehaviour
{
    [SerializeField] private float maxHP, knockbackSpeedX, knockbackSpeedY, knockbackDuration;
    [SerializeField] private bool applyKnockback;
    [SerializeField] private GameObject hitParticle;
    [SerializeField] private float currentHP, knockbackStart;
    [SerializeField] private GameObject brokenVase, coin;
    [SerializeField] private float timeDelay;
    private Controller2D controller;
    private bool playerOnLeft, knockback;

    private int playerFacingDirection;
    private GameObject aliveGO;
    private Rigidbody2D rbAlive;
    private Animator aliveAnim;
    private Player player;

    private GameManager gm; 

    private void Start() {
        currentHP =  maxHP;

        controller = GetComponent<Controller2D>();
        aliveGO = transform.Find("Vase").gameObject;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();


        aliveAnim = aliveGO.GetComponent<Animator>();
        rbAlive = aliveGO.GetComponent<Rigidbody2D>();
    }

    private void Update() {
        CheckKnockback();

        aliveAnim.SetBool("playerOnLeft", playerOnLeft);
        aliveAnim.SetTrigger("Damage");
    }
    private void Damage(AttackDetails attackDetails){
        currentHP -= attackDetails.damageAmount;

        Instantiate(hitParticle, aliveAnim.transform.position, Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f)));

        if(player.transform.position.x < aliveAnim.transform.position.x){
            playerOnLeft = true;
            playerFacingDirection = 1;
        }else{
            playerOnLeft = false;
            playerFacingDirection = -1;
        }

        if(applyKnockback && currentHP > 0.0f){
            Knockback();
            Debug.Log("Knockback");
        }
        if(currentHP <= 0.0f){
            aliveGO.GetComponent<Animation>().Play("redflash");
            Die();
            Debug.Log("Die");
        }
        Debug.Log(playerFacingDirection);
    }

    private void Knockback(){
        knockback = true;
        knockbackStart = Time.time;
        rbAlive.velocity = new Vector2(knockbackSpeedX * playerFacingDirection, knockbackSpeedY);
    }
    public void CheckKnockback(){
        if(Time.time >= knockbackStart + knockbackDuration && knockback){
            knockback = false;
            rbAlive.velocity = new Vector2(0.0f, rbAlive.velocity.y);
        }
    }
    private void Die(){
        Instantiate(coin, aliveGO.transform.position, coin.transform.rotation);
        gm.points += 5;
        StartCoroutine("CoinPartical");
    }

    IEnumerator CoinPartical(){                                    
        yield return new WaitForSeconds(timeDelay);
        Instantiate(brokenVase, aliveGO.transform.position, brokenVase.transform.rotation);      
        aliveGO.SetActive(false);
        yield return 0;
    }
}
