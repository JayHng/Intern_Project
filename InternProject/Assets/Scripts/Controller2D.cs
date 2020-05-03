using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(BoxCollider2D))]
public class Controller2D : MonoBehaviour
{
    const float skinWidth = 0.015f;
    public int horizontalRayCount = 4;
    public int verticalRayCount = 4;

    float maxClimbAngle = 80;
    float horizontalRaySpacing;
    float verticalRaySpacing;

    BoxCollider2D boxcollider;

    RaycastOrigins raycastOrigins;
    public LayerMask collisionMask;
    public CollisionInfo objectCol;

    // Start is called before the first frame update
    void Start()
    {
        boxcollider = GetComponent<BoxCollider2D>();
        CalculateRaySpacing();

    }

    public void Move(Vector3 velocity)
    {    
        UpdateRaycastOrigin();
        objectCol.Reset();

        if(velocity.x != 0){
            HorizontalCollision(ref velocity);
        }
        if(velocity.y != 0){
            VerticalCollision(ref velocity);
        }
        transform.Translate(velocity);
    }

    void HorizontalCollision(ref Vector3 velocity)
    {
        float dirX= Mathf.Sign(velocity.x);
        float rayLength = Mathf.Abs(velocity.x) + skinWidth;

        for(int i=0; i < horizontalRayCount; i++)
        {
            Vector2  rayOrigin = (dirX == -1)?raycastOrigins.bottomLeft:raycastOrigins.bottomRight;
            rayOrigin += Vector2.up * (horizontalRaySpacing * i);
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * dirX, rayLength, collisionMask); 

            Debug.DrawRay(rayOrigin, Vector2.right * dirX * rayLength,Color.cyan);
            if(hit)
            {

                float slopeAngle = Vector2.Angle(hit.normal, Vector2.up);

                if(i == 0 && slopeAngle <= maxClimbAngle){
                    //float distanceToSlopeStart =0;
                    // if(slopeAngle != objectCol.slopeAngleOld){
                    //     distanceToSlopeStart = hit.distance-skinWidth;
                    //     velocity.x -= distanceToSlopeStart + dirX;
                    // }
                    ClimbSlope(ref velocity, slopeAngle);    
                    //velocity.x += distanceToSlopeStart * dirX;       
                }
                if(!objectCol.isClimbing || slopeAngle > maxClimbAngle){
                    velocity.x = (hit.distance - skinWidth) * dirX;
                    rayLength = hit.distance;    
                    // if(objectCol.isClimbing){
                    //     velocity.y = Mathf.Tan(objectCol.slopeAngle * Mathf.Deg2Rad) * Mathf.Abs(velocity.x)
                    // }
                    objectCol.left = dirX == -1;
                    objectCol.right = dirX == 1;
                }  
            }
        }
    }

    void VerticalCollision(ref Vector3 velocity){
        float dirY= Mathf.Sign(velocity.y);
        float rayLength = Mathf.Abs(velocity.y) + skinWidth;

        for(int i=0; i < verticalRayCount; i++){
            Vector2  rayOrigin = (dirY == -1)?raycastOrigins.bottomLeft:raycastOrigins.topLeft;
            rayOrigin += Vector2.right * (verticalRaySpacing * i + velocity.x);
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up * dirY, rayLength, collisionMask); 

            Debug.DrawRay(rayOrigin, Vector2.up * dirY * rayLength,Color.cyan);
            if(hit){
                velocity.y = (hit.distance - skinWidth) * dirY;
                rayLength = hit.distance;

                if(objectCol.isClimbing){
                    velocity.x = velocity.y / Mathf.Tan(objectCol.slopeAngle * Mathf.Deg2Rad) * Mathf.Sign(velocity.x);
                }

                objectCol.below = dirY == -1;
                objectCol.above = dirY == 1;
    
            }
        }
    }
    
    void ClimbSlope(ref Vector3 velocity, float slopeAngle){
         float moveDistance = Mathf.Abs(velocity.x);
         float climbVelocityY=Mathf.Sin(slopeAngle * Mathf.Deg2Rad)* moveDistance;
         if(velocity.y <= climbVelocityY){
            velocity.y = climbVelocityY;
            velocity.x = Mathf.Cos(slopeAngle * Mathf.Deg2Rad) * moveDistance * Mathf.Sign(velocity.x);
            objectCol.below = true;
            objectCol.isClimbing=true;
            objectCol.slopeAngle=slopeAngle;
         }
    }
    void UpdateRaycastOrigin(){
        Bounds bounds = boxcollider.bounds;
        bounds.Expand(skinWidth * -2);

        raycastOrigins.bottomLeft = new Vector2(bounds.min.x,bounds.min.y);
        raycastOrigins.bottomRight = new Vector2(bounds.max.x,bounds.min.y);
        raycastOrigins.topLeft = new Vector2(bounds.min.x,bounds.max.y);
        raycastOrigins.topRight = new Vector2(bounds.max.x,bounds.max.y);        
    }

    void CalculateRaySpacing(){
        Bounds bounds = boxcollider.bounds;
        bounds.Expand(skinWidth * -2);

        horizontalRayCount = Mathf.Clamp(horizontalRayCount, 2, int.MaxValue);
        verticalRayCount = Mathf.Clamp(verticalRayCount, 2, int.MaxValue);

        horizontalRaySpacing = bounds.size.y / (horizontalRayCount - 1);
        verticalRaySpacing = bounds.size.x / (verticalRayCount - 1);        
    }

    struct RaycastOrigins{
        public Vector2 topLeft, topRight;
        public Vector2 bottomLeft, bottomRight;
    }

    public struct CollisionInfo{
        public bool above,below;
        public bool left,right;
        public bool isClimbing;
        public float slopeAngle, slopeAngleOld;
        public void Reset(){
            above = below = false;
            left = right = false;
            isClimbing = false;

            slopeAngleOld = slopeAngle;
            slopeAngle = 0;
        }
    }
}
