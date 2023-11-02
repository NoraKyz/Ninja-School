using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;

    public static UIManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<UIManager>();
            }

            return instance;
        }
    }

    private void Awake()
    {
        instance = this;
    }

    [SerializeField] private Text textCoin;

    public void SetCoin(int coin)
    {
        textCoin.text = coin.ToString();
    }
}
