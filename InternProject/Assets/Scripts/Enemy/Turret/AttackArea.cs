using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    public TurretAI turret;
    public bool isLeft = false;


    private void Awake()
    {
        turret = gameObject.GetComponentInParent<TurretAI>();
    }

    void OnTriggerStay2D(Collider2D other) {
        if(other.CompareTag("Player")){
            if(isLeft){
                turret.TurretAttack(false);
            }else{
                turret.TurretAttack(true);
            }
        }
    }
}
