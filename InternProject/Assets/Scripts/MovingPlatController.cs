using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatController : RaycastController
{
    public Vector3 move;
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 velocity = move * Time.deltaTime;

    }
}
