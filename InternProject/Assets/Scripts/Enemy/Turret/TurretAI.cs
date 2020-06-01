using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretAI : MonoBehaviour
{
    [SerializeField]private int turretHP = 100;
    [SerializeField]private float awakeRange = 10f;
    [SerializeField]private float bulletSpeed = 100f;
    public float shootInterval;
    public float bulletTimer;
    
    public bool isAwake;
    public bool isLookRight = true;

    public GameObject bullet;
    public Player player;
    public Animator anim;
    public Transform shootPointL, shootPointR;

    public float distanceToPlayer;
    void Awake(){
        anim = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        isAwake = false;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>(); //Async to MainScene
    }

    // Update is called once per frame
    void Update()
    {

        RangeCheck();
        anim.SetBool("Awake", isAwake);
        anim.SetBool("isLookRight", isLookRight);

        if(player.transform.position.x > this.transform.position.x){                          
            isLookRight = true;
        }
        if(player.transform.position.x < this.transform.position.x){
            isLookRight = false;
        }

        if(turretHP < 0){
            GameObject.FindGameObjectWithTag("Turret").GetComponent<TurretAI>().enabled = false; 
        }
    }

    void RangeCheck()
    {
        //Distance from turret to player 
        distanceToPlayer = Vector2.Distance(this.transform.position, player.transform.position);

        if(distanceToPlayer > awakeRange) isAwake = false; else isAwake = true;
    }

    public void TurretAttack(bool attackRight)
    {
        bulletTimer += Time.deltaTime;
        
        if(bulletTimer >= shootInterval){
            Vector2 dir = player.transform.position - this.transform.position;
            dir.Normalize();

            if(attackRight){
                GameObject bulletClone;
                bulletClone = Instantiate(bullet, shootPointR.transform.position, shootPointR.transform.rotation);
                bulletClone.GetComponent<Rigidbody2D>().velocity = dir * bulletSpeed;
                bulletTimer=0;
            }
            if(!attackRight){
                GameObject bulletClone;
                bulletClone = Instantiate(bullet, shootPointL.transform.position, shootPointL.transform.rotation);
                bulletClone.GetComponent<Rigidbody2D>().velocity = dir * bulletSpeed;
                bulletTimer=0;
            }
        }
    }

    public void Damage(int dam){
        turretHP -= dam;
    }
}
