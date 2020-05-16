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
    public Transform targetPosition;
    public Animator anim;
    public Transform shootPointL, shootPointR;


    void Awake(){
        anim = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("Awake", isAwake);
        anim.SetBool("isLookRight", isLookRight);

        if(targetPosition.transform.position.x > this.transform.position.x){
            isLookRight = true;
        }
        if(targetPosition.transform.position.x < this.transform.position.x){
            isLookRight = false;
        }
        if(turretHP < 0){
            Destroy(gameObject);
        }
    }
}
