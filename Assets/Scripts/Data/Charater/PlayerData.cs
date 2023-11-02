using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newPlayerData", menuName = "Data/Player Data/Base Data")]
public class PlayerData : ScriptableObject
{
    [Header("Base Properties")]
    public int health;
    
    [Header("Move State")]
    public float speed;
    
    [Header("Jump State")]
    public float jumpForce;
    
    [Header("Check Variables")]
    [Header("Ground Check")]
    public float castDistance;
    public Vector2 groundBoxSize;
    public LayerMask groundLayer;
}
