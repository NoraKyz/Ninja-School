using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kunai : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed;
    [SerializeField] private float damage;

    private void Start()
    {
        OnInit();
    }

    public void OnInit()
    {
        rb.velocity = transform.right * speed;
        
        Invoke(nameof(OnDeSpawn), 4f);
    }
    public void OnDeSpawn()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Enemy"))
        {
            col.GetComponent<Enemy>().OnHit(damage);
            OnDeSpawn();
        }
    }
}
