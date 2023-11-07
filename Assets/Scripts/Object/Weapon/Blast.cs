using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Blast : FlyWeapon
{
    protected override void OnInit()
    {
        base.OnInit();
        rb.velocity = transform.right * speed;
        transform.DOScale(Vector3.one * 2f, 1f);
    }
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Enemy"))
        {
            if (IsCrits())
            {
                col.GetComponent<Enemy>().OnHit(damage * 2, true);
            }
            else
            {
                col.GetComponent<Enemy>().OnHit(damage);
            }
            
            OnDeSpawn();
        }
    }
}
