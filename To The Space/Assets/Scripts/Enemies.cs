using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemies : MonoBehaviour
{
    public Transform instancePoint;                                                 //Transform que nos servira para instanciar la bala
    private Rigidbody2D _rb;                                                        //Rigidbody que nos servira para la rotacion 
    public Text myName;                                                             //Nombre    
    public Text myNum;                                                              //Numero
    public float fireRate;                                                          //Flotante para controlar el tiempo de disparo
    public float amountMissiles;                                                    //Cantidad de misiles        
    public EnemiesSO enemySO;                                                       //ScriptableObject
    private float _time = 0;                                                        //Flotante para controlar el tiempo y saber cuando disparar


    public Transform target1;                                                       //Primer objetivo                      
    public Transform target2;                                                       //Segundo objetivo
    public Transform currentTarget;                                                 //Objetivo actual

    void Start()
    {
        #region Obtenemos y Asignamos  valroes
        target1 = GameObject.FindGameObjectWithTag("Player").transform;
        target2 = GameObject.FindGameObjectWithTag("Ally").transform;
        enemySO.bullet.GetComponent<Bullet>().currentTarget = target2;
        _rb = this.GetComponent<Rigidbody2D>();
        myName.text = enemySO.enemyName;
        myNum.text = enemySO.enemyNum;
        GetComponent<SpriteRenderer>().color = enemySO.enemyColor;
        currentTarget = target2;
        #endregion
    }

    void Update()
    {
        Follow();                                                                   //Llamamos al metodo
        Shooting();                                                                 //Llamamos al metodo
        if (target2 == null)                                                        //Si el segundo objetivo es nulo
        {
            currentTarget = target1;                                                //El objetivo actual pasa a ser el objetivo 1
        }
    }

    #region Metodo para comenzar a seguir y ver al jugador
    void Follow()
    {   
        if (currentTarget != null)                                                         //Si el player es diferente de nulo
        {
            if (Vector2.Distance(transform.position, currentTarget.position) > enemySO.stopDistance)                                    //Obtenemos la distancia y si esta es mayor a la distancia para detenrse dada por enemySO      
            {
                transform.position = Vector2.MoveTowards(transform.position, currentTarget.position, enemySO.speed * Time.deltaTime);   //Le decimos que se mueva de la velocidad actual a la del player a la velocidad dada por enemySO
            }
            if (Vector2.Distance(transform.position, currentTarget.position) < enemySO.stopDistance)                                    //Obtenemos la distancia y si esta es menor a la distancia para detenrse dada por enemySO 
            {
                transform.position = Vector2.MoveTowards(transform.position, currentTarget.position, -enemySO.speed * Time.deltaTime);  //Le decimos que retroceda a la velocidad dada por enemySO
            }
            Vector3 direction = currentTarget.position - transform.position;                                                            //Obtenemos la distancia
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;                                                 //Obtenemos el angulo entre Y y X y lo convertimos en grados 
            _rb.rotation = angle;                                                                                                //Le damos la rotacion

        }
    }
    #endregion

    #region Metodo que se encarga de hacer los disparos
    void Shooting()
    {
        _time += Time.deltaTime;                                                                                                   //incrementamos el tiempo
        if(_time >= fireRate  && amountMissiles > 0)                                                                               //Si el tiempo es mayor que la cadencia de tiro y aun le quedan misiles 
        {
            Instantiate(enemySO.bullet, instancePoint.position, transform.rotation);                                               //Instancia el misil
            _time = 0;                                                                                                             //Reiniciamos el tiempo
            amountMissiles = amountMissiles - 1;                                                                                   //Dosminuimos la cantidad de misiles
        }
    }
    #endregion
}
