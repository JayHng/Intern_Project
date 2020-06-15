using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This code belongs to Bardent(Youtuber)
[CreateAssetMenu(fileName = "newRangeAttackStateData", menuName = "Data/State Data/Range Attack State")]
public class D_RangeAttackState : ScriptableObject
{
    public GameObject projectile;
    public float projectileDamage=10.0f;
    public float projectileSpeed=12.0f;
    public float projectileTravelDistance=7.0f;
}
