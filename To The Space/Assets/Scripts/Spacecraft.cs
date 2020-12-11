using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spacecraft : SpaceShips
{
    public float speed;                                                 //Flotante para controlar la velocidad
    public float gravity;                                               //Flotante paara aplicar gravedad al momento de volar
    float currentGravity;                                               //Flotante para controlar la gravedad actual
    float tempGravity = 0;                                              //Flotante para convertir la gravedad 0
    Vector2 currentCheckpoint;
    void Start()
    {
        currentGravity = gravity;
    }
    
    void Update()
    {
        transform.Translate(Vector3.down * currentGravity * Time.deltaTime);   //Con esta linea aplicamos gravedad
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
            currentCheckpoint = collision.transform.position;
            Debug.Log("Llegue al primer checkpoint");
            //texto que indica que se ha alcanzado un nuevo checkpoint
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "FirstPoint")
        {
            currentCheckpoint = collision.transform.position;
            Debug.Log("Sali del primer punto");
            isFlying = true;
            Fly();
        }
    }

    protected void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Platform")                       //Si colicionamos con la plataforma
        {
            Debug.Log("Estoy en la primer plataforma");
            currentGravity = tempGravity;
            canRefuel = true;
            isFlying = false;
            if (Input.GetKey(KeyCode.R))                                  //Si se presiona W la nave se mueve hacia arriba
            {
                Debug.Log("Recargando combustible");
                IsRefueling();
            }
        }
        if(collision.gameObject.tag == "Enemy")
        {
            IsDead();
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Platform")
        {
            canRefuel = false;
            currentGravity = gravity;

            isFlying = true;
            Fly();                                                       //Llamamos al metodo
        }
    }
}
