using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//This code belongs to Sebastian Lague
[RequireComponent (typeof(BoxCollider2D))]
public class Controller2D : MonoBehaviour
{
    const float skinWidth = 0.015f;
    public int horizontalRayCount = 4;
    public int verticalRayCount = 4;

    float maxClimbAngle = 80;
    float maxDescendAngle = 75;
    float horizontalRaySpacing;

    float verticalRaySpacing;

    BoxCollider2D boxcollider;

    RaycastOrigins raycastOrigins;
    public LayerMask collisionMask;
    public CollisionInfo objectCol;

    public LayerMask collisionDoor;
    public int levelToLoad;
    public int currentLevel;
    AsyncOperation async;

    public GameManager gm;

    // Start is called before the first frame update
    void Start()
    {
        currentLevel = 2;
        boxcollider = GetComponent<BoxCollider2D>();
        CalculateRaySpacing();
    }

    public void Move(Vector3 velocity)
    {    
        UpdateRaycastOrigin();
        objectCol.Reset();
        objectCol.velocityOld = velocity;

        if(velocity.y < 0){
            DescendSlope(ref velocity);
        }
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
                    if(objectCol.isDescending){
                        objectCol.isDescending = false;
                        velocity = objectCol.velocityOld;
                    }
                    ClimbSlope(ref velocity, slopeAngle);    
                }
                if(!objectCol.isClimbing || slopeAngle > maxClimbAngle){
                    velocity.x = (hit.distance - skinWidth) * dirX;
                    rayLength = hit.distance;    
                
                    objectCol.left = dirX == -1;
                    objectCol.right = dirX == 1;
                }

            }
        }
    }

    private void Update()
    {
        if(currentLevel < 4)
        {
            if (async != null)
                if (async.isDone) currentLevel = levelToLoad;
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
        if(objectCol.isClimbing){
            float dirX =Mathf.Sign(velocity.x);
            rayLength = Mathf.Abs(velocity.x) + skinWidth;
			Vector2 rayOrigin = ((dirX == -1)?raycastOrigins.bottomLeft:raycastOrigins.bottomRight) + Vector2.up * velocity.y;
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * dirX, rayLength, collisionMask);
            if(hit){
                float slopeAngle = Vector2.Angle(hit.normal,Vector2.up);
                if(slopeAngle != objectCol.slopeAngle){
                    velocity.x = (hit.distance - skinWidth) * dirX;
                    objectCol.slopeAngle = slopeAngle;
                }
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
    void DescendSlope(ref Vector3 velocity){
        float dirX = Mathf.Sign(velocity.x);
        Vector2 rayOrigin = (dirX == -1)?raycastOrigins.bottomRight:raycastOrigins.bottomLeft;
        RaycastHit2D hit = Physics2D.Raycast(rayOrigin, -Vector2.up, Mathf.Infinity,collisionMask);

        if(hit){
            float slopeAngle = Vector2.Angle(hit.normal, Vector2.up);
            if(slopeAngle != 0 && slopeAngle <= maxDescendAngle){
                if(Mathf.Sign(hit.normal.x) == dirX){
                    if(hit.distance - skinWidth <= Mathf.Tan(slopeAngle * Mathf.Deg2Rad) * Mathf.Abs(velocity.x)){
                        float moveDistance = Mathf.Abs(velocity.x);
                        float descendVelocityY = Mathf.Sin (slopeAngle * Mathf.Deg2Rad) * moveDistance;
						velocity.x = Mathf.Cos (slopeAngle * Mathf.Deg2Rad) * moveDistance * Mathf.Sign (velocity.x);
						velocity.y -= descendVelocityY;

						objectCol.slopeAngle = slopeAngle;
						objectCol.isDescending = true;
						objectCol.below = true;
                    }
                }
            }
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
        public bool isDescending;
        public float slopeAngle, slopeAngleOld;
        public Vector3 velocityOld;
        public void Reset(){
            above = below = false;
            left = right = false;
            isClimbing = false;
            isDescending = false;

            slopeAngleOld = slopeAngle;
            slopeAngle = 0;
        }
    }

}
