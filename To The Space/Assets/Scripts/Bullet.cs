using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    
    private Rigidbody2D _rb;                                                                                 //Rigidbody que nos servira para poder rotar el misil
    public float speed = 0.1f;                                                                               //Velocidad a la que se movera                    
    public float rotateSpeed = 200;                                                                          //Velocidad a la que rotara
    public Transform currentTarget;                                                                          //Posicion del target actual

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();                                                                   //Obtenemos los componentes
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        
        if(currentTarget != null)                                                                           //Si el objetivo actual no es nulo
        {
            Vector2 direction = (Vector2) currentTarget.position - _rb.position;                            //Obtenemos la direccion
            float rotateAmount = Vector3.Cross(direction, transform.up).z;                                  //Obtenemos que tanto va a rotar el misil
            _rb.angularVelocity = -rotateAmount * rotateSpeed;                                              //Cuantos grados por segundo rotara (lo hacemos negativo ya que de lo contrario el misil se dirige a la direccion contraria del objetivo

            float distanceThisFrame = speed * Time.deltaTime;                                               //Incrementamos la velocidad a cada segundo
            transform.Translate(direction.normalized * distanceThisFrame, Space.World);                     //Hacemos que se mueva hacia la direccion indicada a la velocidad de distanceThisFrame
        }
        else
        {
            Destroy(this.gameObject);                                                                       //Si es nulo, lo destruimos
        }

    }

    //Cuando detecta una colision de tipo trigger
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")                                                           //Si coliciona con player, se destrulle 
        {
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "Ally")                                                             //Si colicionamos con el aliado, se destrulle 
        {
            Destroy(gameObject);
        }
    }
}
