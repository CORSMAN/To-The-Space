using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spacecraft : SpaceShips
{
    public float speed;                                                          //Flotante para controlar la velocidad
    public float gravity;                                                        //Flotante paara aplicar gravedad al momento de volar
    float currentGravity;                                                        //Flotante para controlar la gravedad actual
    float tempGravity = 0;                                                       //Flotante para convertir la gravedad 0

    void Start()
    {
        currentGravity = tempGravity;
    }
    
    void Update()
    {
        if(canPressR && Input.GetKey(KeyCode.R))
        {
            IsRefueling();
        }
        transform.Translate(Vector3.down * currentGravity * Time.deltaTime);     //Con esta linea aplicamos gravedad
        if (Input.GetKey(KeyCode.W))                                             //Si se mantiene presionada W la nave se mueve hacia arriba y consume combustible
        {
            transform.Translate(Vector3.up * speed * Time.deltaTime);
            consumingFuel = true;
            isFlying = true;
            Fly();
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            Debug.Log("deje de presionar w");
            consumingFuel = false;                                               //Si se Suelta W la nave deja de consumir combustible
            isFlying = false;
            Fly();
        }

        if (Input.GetKey(KeyCode.A))                                             //Si se mantiene presionada A la nave se mueve hacia la izquierda
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime); 
        }
        if (Input.GetKey(KeyCode.D))                                             //Si se mantiene presionada D la nave se mueve hacia la derecha
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
    }


    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Checkpoint")                                       //Si colicionamos con un checkpoint
        {
            Debug.Log("Llegue al primer checkpoint");
            //texto que indica que se ha alcanzado un nuevo checkpoint
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "FirstPoint")
        {
            Debug.Log("Sali del primer punto");
            //isFlying = true;
            //Fly();
        }
    }

    protected void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Platform")                              //Si colicionamos con la plataforma
        {
            Debug.Log("Estoy en la primer plataforma");
            currentGravity = tempGravity;
            canRefuel = true;
            isFlying = false;
            canPressR = true;
            consumingFuel = false;
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
            Debug.Log("Sali de la primera plataforma");
            canRefuel = false;
            currentGravity = gravity;

            isFlying = true;
            Fly();                                                               //Llamamos al metodo
        }
    }
}
