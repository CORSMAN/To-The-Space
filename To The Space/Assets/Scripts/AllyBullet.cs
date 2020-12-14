﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyBullet : MonoBehaviour
{
    public Transform target;
    private Rigidbody2D _rb;
    public float speed = 0.1f;
    public float rotateSpeed = 200;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Enemy").transform;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 direction = (Vector2)target.position - _rb.position;
        direction.Normalize();
        float rotateAmount = Vector3.Cross(direction, transform.up).z;
        _rb.angularVelocity = -rotateAmount * rotateSpeed;
        _rb.velocity = transform.up * speed;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")                              //Si colicionamos con la plataforma
        {
            Destroy(gameObject);
        }
    }
}
