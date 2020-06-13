using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This code belongs to Bardent (Youtuber)
[CreateAssetMenu(fileName = "newMeleeAttackStateData", menuName = "Data/State Data/Melee Attack State")]
public class D_MeleeAttack : ScriptableObject
{
    public float attackRadius = 0.5f;
    public float attackDamage = 10.0f;
    public LayerMask isPlayer;
}
