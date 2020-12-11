using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spacecraft : SpaceShips
{
    public float speed;                                                 //Flotante para controlar la velocidad
    public float gravity;                                               //flotante para controlar la gravedad
    
    void Start()
    {
        
    }
    
    void Update()
    {
        if (transform.position.y > -3.83f)                              //Si superamos -3.83 en el eje y, significa que estamos en el aire
        {
            IsFlying();                                                 //Llamamos al metodo
        }
        transform.Translate(Vector3.down * gravity * Time.deltaTime);   //Con esta linea aplicamos gravedad
        if (Input.GetKey(KeyCode.W))                                    //Si se presiona W la nave se mueve hacia arriba
        {
            transform.Translate(Vector3.up * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A))                                    //Si se presiona A la nave se mueve hacia la izquierda
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime); 
        }
        if (Input.GetKey(KeyCode.D))                                    //Si se presiona D la nave se mueve hacia la derecha
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
    }


    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Checkpoint")                              //Si colicionamos con un checkpoint
        {
            //texto que indica que se ha alcanzado un nuevo checkpoint
        }
    }

    protected void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Checkpoint")                       //Si colicionamos con un checkpoint
        {
            canRefuel = true;
            IsRefueling();
        }
        if(collision.gameObject.tag == "Enemy")
        {
            IsDead();
        }
    }
}
