using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private Transform targetA, targetB;
    [SerializeField] private float speed = 1f;
    
    private Vector3 _target;
    void Start()
    {
        transform.position = targetA.position;
        _target = targetB.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _target, Time.deltaTime * speed);

        if (Vector3.Distance(transform.position, targetA.position) < 0.1f)
        {
            _target = targetB.position;
        } else if (Vector3.Distance(transform.position, targetB.position) < 0.1f)
        {
            _target = targetA.position;
        } 
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            col.transform.SetParent(transform);
        }
    }
    
    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            col.transform.SetParent(null);
        }
    }
}
