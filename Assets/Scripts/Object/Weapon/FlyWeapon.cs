using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyWeapon : MonoBehaviour
{
    [SerializeField] protected FlyWeaponData flyWeaponData;
    
    [Header("=========================================")]
    [SerializeField] protected  Rigidbody2D rb;
    
    protected float speed;
    protected float damage;
    protected float critsRate;
    protected float thrustForce;
    protected float lifeTime;
    
    void Start()
    {
        OnInit();
    }

    protected virtual void OnInit()
    {
        InitProperties();
        
        Invoke(nameof(OnDeSpawn), lifeTime);
    }

    protected virtual void InitProperties()
    {
        speed = flyWeaponData.speed;
        damage = flyWeaponData.damage;
        critsRate = flyWeaponData.critRate;
        thrustForce = flyWeaponData.thrustForce;
        lifeTime = flyWeaponData.lifeTime;
    }

    protected virtual void OnDeSpawn()
    {
        Destroy(gameObject);
    }
    
    protected bool IsCrits()
    {
        int crits = Random.Range(0, 100);
        
        return crits <= critsRate;
    }
}
