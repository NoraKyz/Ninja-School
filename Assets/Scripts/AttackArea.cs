using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player") || col.CompareTag("Enemy"))
        {
            Debug.Log(col.tag);
            col.GetComponent<Character>().OnHit(30f);
        }
    }
}
