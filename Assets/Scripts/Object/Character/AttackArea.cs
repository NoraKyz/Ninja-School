using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    [SerializeField] private Transform target;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag(target.tag))
        {
            col.GetComponent<Character>().OnHit(30f);
        }
    }
}
