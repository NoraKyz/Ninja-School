using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextCombat : MonoBehaviour
{
    [SerializeField] private Text textCombat;
    
    public void OnInit(float damage)
    {
        textCombat.text = damage.ToString();
        Invoke(nameof(Despawn), 1f);
    }

    public void Despawn()
    {
        Destroy(gameObject);
    }
}
