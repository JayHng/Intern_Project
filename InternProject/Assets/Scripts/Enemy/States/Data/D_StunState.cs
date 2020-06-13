using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This code belongs to Bardent (Youtuber)
[CreateAssetMenu(fileName = "newStunStateData", menuName = "Data/State Data/Stun State")]
public class D_StunState : ScriptableObject
{
    public float stunTime = 3.0f;
    public float stunKnockbackTime = 0.2f;
    public Vector2 stunKnockbackAngle;
    public float stunKnockbackSpeed = 20f;
}
