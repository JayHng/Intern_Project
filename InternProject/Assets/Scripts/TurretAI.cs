  using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretAI : MonoBehaviour
{
    [SerializeField]private int turretHP = 100;
    public float playerDistance;
    [SerializeField]private float awakeRange = 5;
    [SerializeField]private float bulletSpeed = 10;
    public float shootInterval;
    public float bulletTimer;
    
    public bool isAwake;
    public bool isLookRight = true;

    public GameObject bullet;
    public Player target;
    public Animator anim;
    public Transform shootPointL, shootPointR;


    void Awake(){
        anim = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("Awake", isAwake);
        anim.SetBool("isLookRight", isLookRight);

        RangeCheck();

        if(target.transform.position.x > this.transform.position.x){                          
            isLookRight = true;
        }
        if(target.transform.position.x < this.transform.position.x){
            isLookRight = false;
        }
        if(turretHP < 0){
            Destroy(gameObject);
        }
    }
    void RangeCheck()
    {
        playerDistance = Vector2.Distance(this.transform.position, target.transform.position);
 
        if (playerDistance < awakeRange)
            isAwake = true;
 
        if (playerDistance > awakeRange)
            isAwake = false;
    }


    //I don't know why this function doesnt work. I try it on other 2D game and it works, but not this one. I try Debug.log to check but it doesnt work.
    public void TurretAttack(bool attackRight)
    {
        bulletTimer += Time.deltaTime;
        
        if(bulletTimer >= shootInterval){
            Vector2 dir = target.transform.position - this.transform.position;
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
