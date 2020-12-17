using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public float rotateSpeed = 2f;                                                  //Velocidad de rotacion
    public float speed = 200;                                                       //Velocidad a la que se movera
    Rigidbody2D rb;                                                                 //Rigidbody para poder interactuar con las fuerzas

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();                                           //Obtenemos los componentes
    }

    // Update is called once per frame
    void Update()
    {
        rb.AddForce(Vector3.left * speed);                                          //Agregamos una fuerza a la izquierda multiplicada por la velocidad de movimiento
        transform.Rotate(Vector3.forward * rotateSpeed * Time.deltaTime);           //Le decimos que comience a rotar

        if(transform.position.x <= -10)                                             //Si la posicion en x es menor a 10
        {
            Destroy(this.gameObject);                                               //Destruimos el objeto
        }
    }

    //Cuando detecta una colision con cualquiera de estos tags, se detrulle a si mismo 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")          
        {
            Destroy(this.gameObject);
        }
        if (collision.tag == "Ally")
        {
            Destroy(this.gameObject);
        }
    }

}
