using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This code belongs to Bardent(Youtuber)
[CreateAssetMenu(fileName = "newEntityData", menuName = "Data/Entity Data/Base Data")]
public class D_Entity : ScriptableObject
{
    public float wallCheckDistance = 0.2f;
    public float ledgeCheckDistance = 0.4f;
    public float groundCheckRadius = 0.3f;


    public float minArgoDistance = 3.0f;
    public float maxArgoDistance = 4.0f;


    public float maxHP = 50.0f;
    public float damageHopSpeed = 3.0f;

    public float stunResistance = 3.0f;
    public float stunRecoveryTime = 2.0f;


    public float closeRangeActionDistance = 1.0f;

    public GameObject hitParticle;

    public LayerMask isGround;
    public LayerMask isPlayer;

}
