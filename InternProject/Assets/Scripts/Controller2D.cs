using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller2D : MonoBehaviour
{
    const float skinWidth = 0.015f;
    public int horizontalRayCount = 4;
    public int verticalRayCount = 4;

    float horizontalRaySpacing;
    float verticalRaySpacing;

    BoxCollider2D boxcollider;


    RaycastOrigins raycastOrigins;
    public LayerMask collisionMask;

    // Start is called before the first frame update
    void Start()
    {
        boxcollider = GetComponent<BoxCollider2D>();
        CalculateRaySpacing();

    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Move(Vector3 velocity){    
        UpdateRaycastOrigin();
        if(velocity.x != 0){
        HorizontalCollision(ref velocity);
        }
        if(velocity.y != 0){
        VerticalCollision(ref velocity);
        }
        transform.Translate (velocity);
    }

    void HorizontalCollision(ref Vector3 velocity){
        float dirX= Mathf.Sign(velocity.x);
        float rayLength = Mathf.Abs(velocity.x) * skinWidth;

        for(int i=0; i < horizontalRayCount; i++){
            Vector2  rayOrigin = (dirX == -1)? raycastOrigins.bottomLeft : raycastOrigins.bottomRight;
            rayOrigin += Vector2.up * (horizontalRaySpacing * i);
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * dirX, rayLength, collisionMask); 

            Debug.DrawRay(rayOrigin, Vector2.right * dirX * rayLength, Color.cyan);
            if(hit){
                velocity.x = (hit.distance - skinWidth) * dirX;
                rayLength = hit.distance;
            }
        }
    }

    void VerticalCollision(ref Vector3 velocity){
        float dirY= Mathf.Sign(velocity.y);
        float rayLength = Mathf.Abs(velocity.y) + skinWidth;

        for(int i=0; i < verticalRayCount; i++){
            Vector2  rayOrigin = (dirY == -1)? raycastOrigins.bottomLeft : raycastOrigins.topLeft;
            rayOrigin += Vector2.right * (verticalRaySpacing * i + velocity.x);
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up * dirY, rayLength,collisionMask); 

            Debug.DrawRay(rayOrigin, Vector2.up * dirY * rayLength, Color.cyan);
            if(hit){
                velocity.y = (hit.distance - skinWidth) * dirY;
                rayLength = hit.distance;
            }
        }
    }
    void UpdateRaycastOrigin(){
        Bounds bounds = boxcollider.bounds;
        bounds.Expand(skinWidth *-2);

        raycastOrigins.bottomLeft = new Vector2(bounds.min.x,bounds.min.y);
        raycastOrigins.bottomRight = new Vector2(bounds.max.x,bounds.max.y);
        raycastOrigins.topLeft = new Vector2(bounds.min.x,bounds.min.y);
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
}
