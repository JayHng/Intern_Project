using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This code belongs to Sebastian Lague
public class MovingPlatController : RaycastController
{
    public LayerMask passengerMask;
    public Vector3 move;

    List<PassengerMovement> passengerMovement;
    Dictionary<Transform, Controller2D> passengerDictionary = new Dictionary<Transform, Controller2D>();

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateRaycastOrigin();

        Vector3 velocity = move * Time.deltaTime;

        CalculatePassengerMovement(velocity);

        MovePassengers(true);
        transform.Translate(velocity);
        MovePassengers(false);
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

}
