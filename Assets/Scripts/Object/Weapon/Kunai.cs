using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kunai : FlyWeapon
{
    [SerializeField] private GameObject hitVFX;
    protected override void OnInit()
    {
        base.OnInit();
        rb.velocity = transform.right * speed;
    }
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Enemy"))
        {
            if (IsCrits())
            {
                col.GetComponent<Enemy>().OnHit(damage * 2);
            }
            else
            {
                col.GetComponent<Enemy>().OnHit(damage);
            }

            var cacheTransform = transform;
            Instantiate(hitVFX, cacheTransform.position, cacheTransform.rotation);
            OnDeSpawn();
        }
    }
}
