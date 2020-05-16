using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    public TurretAI turret;
    public bool isLeft = false;


    void Awake()
    {
        turret = gameObject.GetComponentInParent<TurretAI>();
    }

    private void OnTriggerStay2D(Collider2D other) {
        if(other.CompareTag("Player")){
            if(isLeft){
                turret.Attack(false);
            }else{
                turret.Attack(true);
            }
        }
    }
}
