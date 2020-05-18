using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Thiscode belongs to Bardent
public class PlayerCombatController : MonoBehaviour
{
    [SerializeField]private bool combatEnabled, inputEntered;
    [SerializeField]private float inputTimer, attack1Radius, attack1Damage;
    [SerializeField]private Transform attack1HitBoxPos;
    [SerializeField]private LayerMask isDamageable;
    private float lastInputTime=Mathf.NegativeInfinity;
    public float[] attackDetails = new float[2];

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckCombatInput();
    }
    private void CheckCombatInput(){
        if(Input.GetMouseButtonDown(0)){
            if(combatEnabled){
                //inputEntered = true;
                lastInputTime = Time.time;
            }
        }
    }
    
    private void CheckAttackHitBox(){
        Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(attack1HitBoxPos.position, attack1Radius, isDamageable);
        attackDetails[0] = attack1Damage;
        attackDetails[1] = transform.position.x;

        foreach(Collider2D collider in detectedObjects){
            collider.transform.parent.SendMessage("Damage", attack1Damage);
        }
    }

    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(attack1HitBoxPos.position, attack1Radius);
    }
}
