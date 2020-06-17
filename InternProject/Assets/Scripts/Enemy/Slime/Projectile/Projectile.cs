using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This code belongs to Bardent(Youtuber)
public class Projectile : MonoBehaviour
{
    private AttackDetails attackDetails;
    private float speed;
    private float travelDistance;
    private float xStartPos;
    [SerializeField] private float gravity;
    [SerializeField] private float damageRadius;
    private Rigidbody2D projectileRb;
    private bool isGravityOn;
    private bool hasHitGround;
    [SerializeField] private LayerMask isGround;
    [SerializeField] private LayerMask isPlayer;
    [SerializeField] private Transform damagePosition;

    private void Start() {
        projectileRb = GetComponent<Rigidbody2D>();
        projectileRb.gravityScale = 0.0f;
        projectileRb.velocity = transform.right * speed;

        isGravityOn = false;

        xStartPos = transform.position.x;
    }
    private void Update() {
        if(!hasHitGround){
            attackDetails.position = transform.position;
            if(isGravityOn){
                float angle = Mathf.Atan2(projectileRb.velocity.y, projectileRb.velocity.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            }
        }
    }
    private void FixedUpdate() {
        if(!hasHitGround){
            Collider2D damageHit = Physics2D.OverlapCircle(damagePosition.position, damageRadius,isPlayer);
            Collider2D groundHit = Physics2D.OverlapCircle(damagePosition.position, damageRadius,isGround);

            if(damageHit){
                damageHit.transform.SendMessage("Damage", attackDetails);
                Destroy(gameObject);
            }
            if(groundHit){
                hasHitGround = true;
                projectileRb.gravityScale = 0.0f;
                projectileRb.velocity = Vector2.zero;
            }
            if(Mathf.Abs(xStartPos - transform.position.x) >= travelDistance && !isGravityOn){
                isGravityOn = true;
                projectileRb.gravityScale = gravity;
            }
        }
    }
    public void FireProjectile(float speed, float travelDistance, float damage){
        this.speed=speed;
        this.travelDistance=travelDistance;
        attackDetails.damageAmount = damage;
    }
    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(damagePosition.position,damageRadius);
    }
}
