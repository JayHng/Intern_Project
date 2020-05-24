using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This code belongs to Sebastian Lague(Youtuber)
public class MovingPlatController : RaycastController
{
    public LayerMask passengerMask;

    public Vector3[] localWaypoints;
    Vector3[] globalWaypoints;

    public float speed;
    public bool cyclic;
    public float waitTime;
    int fromWaypointIndex;
    float percentBetweenWaypoints;
    float nextMoveTime;

    List<PassengerMovement> passengerMovement;
    Dictionary<Transform, Controller2D> passengerDictionary = new Dictionary<Transform, Controller2D>();

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();

        if (localWaypoints != null && localWaypoints.Length > 0)
        {
            globalWaypoints = new Vector3[localWaypoints.Length];
            for (int i = 0; i < localWaypoints.Length; i++)
            {
                globalWaypoints[i] = localWaypoints[i] + transform.position;
            } 
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateRaycastOrigin();

        Vector3 velocity = CalculatePlatformMovement();

        CalculatePassengerMovement(velocity);

        MovePassengers(true);
        transform.Translate(velocity);
        MovePassengers(false);
    }

    Vector3 CalculatePlatformMovement(){
        if(Time.time <nextMoveTime){
            return Vector3.zero;
        }

        fromWaypointIndex %= globalWaypoints.Length;
        int toWaypointIndex = (fromWaypointIndex +1) % globalWaypoints.Length;
        float distanceBetweenWaypoints = Vector3.Distance(globalWaypoints[fromWaypointIndex], globalWaypoints[toWaypointIndex]);
        percentBetweenWaypoints += Time.deltaTime * speed/distanceBetweenWaypoints;

        Vector3 newPos = Vector3.Lerp(globalWaypoints[fromWaypointIndex], globalWaypoints[toWaypointIndex], percentBetweenWaypoints);

        if(percentBetweenWaypoints >= 1){
            percentBetweenWaypoints =0;
            fromWaypointIndex++;
            if(!cyclic){
                if(fromWaypointIndex >= globalWaypoints.Length-1){
                    fromWaypointIndex =0;
                    System.Array.Reverse(globalWaypoints);
                }
            }
            nextMoveTime = Time.time;
        }
        return newPos - transform.position;
    }
    void MovePassengers(bool beforeMovePlatform){
        foreach(PassengerMovement passenger in passengerMovement){
            if(!passengerDictionary.ContainsKey(passenger.transform)){
                passengerDictionary.Add(passenger.transform, passenger.transform.GetComponent<Controller2D>());
            }

            if(passenger.moveBeforePlatform == beforeMovePlatform){
                passengerDictionary[passenger.transform].Move(passenger.velocity, passenger.standingOnPlatform);
            }
        }
    }

    void CalculatePassengerMovement(Vector3 velocity){
        HashSet<Transform> movedPassengers = new HashSet<Transform>();
        passengerMovement = new List<PassengerMovement>();

        float dirX = Mathf.Sign(velocity.x);
        float dirY = Mathf.Sign(velocity.y);

        //Vertically moving platform
        if(velocity.y != 0){
            float rayLength = Mathf.Abs(velocity.y) + skinWidth;

            for(int i=0; i < verticalRayCount; i++){
                Vector2  rayOrigin = (dirY == -1)?raycastOrigins.bottomLeft:raycastOrigins.topLeft;
                rayOrigin += Vector2.right * (verticalRaySpacing * i);
                RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up * dirY, rayLength, passengerMask);

                if(hit){
                    if(!movedPassengers.Contains(hit.transform)){
                        movedPassengers.Add(hit.transform);
                        float pushX = (dirY == 1)?velocity.x:0;
                        float pushY = velocity.y - (hit.distance - skinWidth) * dirY;
    
                        passengerMovement.Add(new PassengerMovement(hit.transform, new Vector3(pushX,pushY), dirY == 1, true));
                    }
                }
            }
        }

        //Horizontally moving platform
        if(velocity.x != 0){
            float rayLength = Mathf.Abs(velocity.x) + skinWidth;

            for(int i=0; i < horizontalRayCount; i++)
            {
                Vector2  rayOrigin = (dirX == -1)?raycastOrigins.bottomLeft:raycastOrigins.bottomRight;
                rayOrigin += Vector2.up * (horizontalRaySpacing * i);
                RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * dirX, rayLength, passengerMask);

                if(hit){
                    if(!movedPassengers.Contains(hit.transform)){
                        movedPassengers.Add(hit.transform);
                        float pushX = velocity.x - (hit.distance - skinWidth) * dirX;
                        float pushY = -skinWidth;

                        passengerMovement.Add(new PassengerMovement(hit.transform, new Vector3(pushX,pushY), false, true));
                    }
                }
            }
        }

        //Passenger on top of a horizontally or a downward moving platform
        if(dirY == -1 || velocity.y == 0 && velocity.x != 0){
            float rayLength = skinWidth * 2;

            for(int i=0; i < verticalRayCount; i++){
                Vector2  rayOrigin = raycastOrigins.topLeft + Vector2.right * (verticalRaySpacing * i);
                RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up, rayLength, passengerMask);

                if(hit){
                    if(!movedPassengers.Contains(hit.transform)){
                        movedPassengers.Add(hit.transform);
                        float pushX = velocity.x;
                        float pushY = velocity.y;
    
                        passengerMovement.Add(new PassengerMovement(hit.transform, new Vector3(pushX,pushY), true, false));
                    }
                }
            }
        }

    }
    struct PassengerMovement{
        public Transform transform;
        public Vector3 velocity;
        public bool standingOnPlatform;
        public bool moveBeforePlatform;

        public PassengerMovement(Transform _transform, Vector3 _velocity, bool _standingOnPlatform, bool _moveBeforePlatform){
            transform = _transform;
            velocity = _velocity;
            standingOnPlatform = _standingOnPlatform;
            moveBeforePlatform = _moveBeforePlatform;
        }
    }

    void OnDrawGizmos() {
        if(localWaypoints != null){
            Gizmos.color = Color.red;
            float size = .3f;

            for(int i=0; i<localWaypoints.Length; i++){
                Vector3 globalWaypointPos = (Application.isPlaying)?globalWaypoints[i] : localWaypoints[i] + transform.position;
                Gizmos.DrawLine(globalWaypointPos - Vector3.up * size, globalWaypointPos  + Vector3.up *size);
                Gizmos.DrawLine(globalWaypointPos - Vector3.left * size, globalWaypointPos  + Vector3.left *size);
            }
        }
    }

}
