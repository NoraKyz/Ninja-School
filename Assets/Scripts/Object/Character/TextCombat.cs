using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextCombat : MonoBehaviour
{
    [SerializeField] private Text textCombat;
    [SerializeField] private Image critIcon;
    
    public void OnInit(float damage, bool isCritical)
    {
        if (isCritical)
        {
            critIcon.gameObject.SetActive(true);
            textCombat.transform.localScale = Vector3.one * 1.3f;
        }
        
        textCombat.text = damage.ToString();
        Invoke(nameof(Despawn), 1f);
    }

    public void Despawn()
    {
        Destroy(gameObject);
    }
}
