using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public float rotateSpeed = 2f;//
    public float speed = 200;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.AddForce(Vector3.left * speed);
        transform.Rotate(Vector3.forward * rotateSpeed * Time.deltaTime);
        if(transform.position.x <= -10)
        {
            Destroy(this.gameObject);
        }
    }


}
