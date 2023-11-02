using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingClouds : MonoBehaviour
{
    private float speed;
    [SerializeField] private float leftLimitX;
    [SerializeField] private float rightLimitX;
    private void Start()
    {
        speed = Random.Range(0.5f, 2f);
    }

    private void Update()
    {
        transform.position += Vector3.right * speed * Time.deltaTime;
        
        if(transform.position.x > rightLimitX)
        {
            speed = Random.Range(0.5f, 2f);
            transform.position = new Vector3(leftLimitX, Random.Range(-3f, 8f), 0);
        }
    }
}
