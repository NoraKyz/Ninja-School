using System;
using UnityEngine;

public class SavePoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            Debug.Log("SavePoint");
            col.GetComponent<Player>().SavePoint();
        }
    }
}