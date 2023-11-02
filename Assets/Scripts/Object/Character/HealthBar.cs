using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image imageFill;

    private float hp, maxHp;
    private void Update()
    {
        imageFill.fillAmount = Mathf.Lerp(imageFill.fillAmount, hp / maxHp, Time.deltaTime * 5f);
    }

    private void LateUpdate()
    {
        transform.rotation = Quaternion.identity;
    }

    public void OnInit(float maxHp)
    {
        this.maxHp = maxHp;
        hp = maxHp;
    }
    
    public void SetNewHp(float hp)
    {
        this.hp = hp;
    }
}
