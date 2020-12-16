using System.Collections;
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
        target = GameObject.FindGameObjectWithTag("Enemy").transform;

        _rb = GetComponent<Rigidbody2D>();

        if (target == null)
        {
            Debug.LogError("target null");


            Destroy(this.gameObject);
        }
        if(_rb == null)
        {
            Debug.LogError("rb null");
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (target == null)
        {
            Destroy(gameObject);
        }
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
            // collision.gameObject.SetActive(false);

            GameController.gc.allEnemies.Remove(collision.gameObject.GetComponent<Enemies>());
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
