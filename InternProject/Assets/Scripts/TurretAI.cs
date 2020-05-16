using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretAI : MonoBehaviour
{
    public int turretHP = 100;
    public float playerDistance;
    public float awakeRange;
    public float shootInterval;
    public float bulletSpeed = 5;
    public float bulletTimer;
    
    public bool isAwake = false;
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



    public void Attack(bool attackright)
    {
        bulletTimer += Time.deltaTime;
 
        if (bulletTimer >= shootInterval)
        {                     
            Vector2 direction = target.transform.position - this.transform.position;
            direction.Normalize();
 
            if (attackright)
            {
                GameObject bulletclone;
                bulletclone = Instantiate(bullet, shootPointR.transform.position, shootPointR.transform.rotation) as GameObject;
                bulletclone.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;
 
                bulletTimer = 0;
            }
 
            if (!attackright)
            {
                GameObject bulletclone;
                bulletclone = Instantiate(bullet, shootPointL.transform.position, shootPointL.transform.rotation) as GameObject;
                bulletclone.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;
 
                bulletTimer = 0;
            }
        }
    }

    public void Damage(int dam){
        turretHP -= dam;
    }
}
