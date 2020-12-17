using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyBullet : MonoBehaviour
{

    GameController gameController;                                                  //Variable para modificar datos de GameController
    public Transform target;                                                        //Transform que nos servira para obtener la posicion del objetivo
    private Rigidbody2D _rb;                                                        //Usaremos este rigidboy para hacer el giro del misil
    public float speed = 0.1f;                                                      //Velocidad a la cual se movera el misil
    public float rotateSpeed = 200;                                                 //Velocidad a la cual girara el misil

    private void Start()
    {

        gameController = FindObjectOfType<GameController>();                        //Buscamos y obtenemos los componentes de GameController
        target = GameObject.FindGameObjectWithTag("Enemy").transform;               //Obtenemos la posicion del objetivo

        _rb = GetComponent<Rigidbody2D>();                                          //Obtenemos sus componentes

        if (target == null)                                                         //Si el objetivo es nullo
        {
            Destroy(this.gameObject);                                               //Destruimos al misil
        }
    }

    void FixedUpdate()
    {
        if (target == null)                                                         //Si el objetivo es nullo
        {
            Destroy(this.gameObject);                                               //Destruimos al misil
        }
        Vector2 direction = (Vector2)target.position - _rb.position;                //Obtenemos distancia
        direction.Normalize();                                                      //Normalizamos distancia
        float rotateAmount = Vector3.Cross(direction, transform.up).z;              //Obtenemos que tanto va a rotar el misil
        _rb.angularVelocity = -rotateAmount * rotateSpeed;                          //Cuantos grados por segundo rotara (lo hacemos negativo ya que de lo contrario el misil se dirige a la direccion contraria del objetivo
        _rb.velocity = transform.up * speed;                                        //Obtenemos una velocidad lineal

    }

    //Cuando detecta una colision del tipo trigger
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")                                    //Si colicionamos con la plataforma
        {
            gameController.allEnemies.Remove(collision.gameObject.GetComponent<Enemies>()); //Removemos al enemigo de la lista
            Destroy(collision.gameObject);                                          //Destruimos al objetivo
            Destroy(this.gameObject);                                               //Destruimos al misil
        }
    }
}
