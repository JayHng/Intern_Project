using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] GameObject target;
    [SerializeField] float addDistanceCamera = 1.0f;
    private Vector3 targetPosition;
    public float cameraSpeedMove = 5.0f;
    [SerializeField] float minClam = -20.0f;
    [SerializeField] float maxClam = 45.0f;
    [SerializeField] bool followTarget;

    // Start is called before the first frame update
    void Start()
    {
       followTarget = true;
    }

    // Update is called once per frame 
    void Update()
    {   
       if(followTarget){
            targetPosition = new Vector2(target.transform.position.x, Mathf.Clamp(target.transform.position.y, minClam, maxClam));
            
            if(target.transform.localScale.x > 0f){
                targetPosition = new Vector3(targetPosition.x + addDistanceCamera, targetPosition.y+2, transform.position.z);      
            } else{
                targetPosition = new Vector3(targetPosition.x - addDistanceCamera, targetPosition.y+2, transform.position.z);
            }
            //make smooth transition of the camera
            transform.position = Vector3.Lerp(transform.position, targetPosition, cameraSpeedMove * Time.deltaTime);
       }
    }
}
