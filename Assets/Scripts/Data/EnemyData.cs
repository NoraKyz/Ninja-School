using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newEnemyData", menuName = "Data/Enemy Data/Base Data")]
public class EnemyData : ScriptableObject
{
    [Header("Base Properties")]
    public int health;
    
    [Header("Patrol State")]
    public float speed;
    
    [Header("Attack State")]
    public float attackRange;
    
    [Header("Throw State")]
    public float throwRange;
}
