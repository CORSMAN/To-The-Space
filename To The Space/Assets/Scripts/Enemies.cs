using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemies : MonoBehaviour
{
    //Variables para guardar datos de los enemigos al cargar y guardar partidas
    //public bool isDead;
    //public float enemyPositionX;
    //public float enemyPositionY;

    public Transform player;
    public Transform instancePoint;//Transform que nos servira para instanciar la bala
    private Rigidbody2D _rb;
    public Text myName;
    public Text myNum;
    public float fireRate;//Flotante para controlar el tiempo de disparo
    public float amountMissiles;
    public EnemiesSO enemySO;
    public static Enemies enemies;
    private float _time = 0;//Flotante para controlar el tiempo y saber cuando disparar


    public Transform target1;
    public Transform target2;
    public Transform currentTarget;
    // Start is called before the first frame update
    void Start()
    {

        target1 = GameObject.FindGameObjectWithTag("Player").transform;
        target2 = GameObject.FindGameObjectWithTag("Ally").transform;
        enemySO.bullet.GetComponent<Bullet>().currentTarget = target2;
        _rb = this.GetComponent<Rigidbody2D>();
        myName.text = enemySO.enemyName;
        myNum.text = enemySO.enemyNum;
        GetComponent<SpriteRenderer>().color = enemySO.enemyColor;
    }

    // Update is called once per frame
    void Update()
    {
        //enemyPositionX = transform.position.x;
        //enemyPositionY = transform.position.y;
        
            Follow();
            Shooting();
        if (target2 == null)
        {
            currentTarget = target1;
        }
    }

    //Metodo para comenzar a seguir y ver al jugador
    void Follow()
    {
        if (player != null)
        {
            if (Vector2.Distance(transform.position, player.position) > enemySO.stopDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.position, enemySO.speed * Time.deltaTime);
            }
            if (Vector2.Distance(transform.position, player.position) < enemySO.stopDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.position, -enemySO.speed * Time.deltaTime);
            }
            Vector3 direction = player.position - transform.position;//Obtenemos la distancia
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;//Obtenemos el angulo entre Y y X y lo convertimos en grados 
            _rb.rotation = angle;//Le damos la rotacion

        }
    }

    void Shooting()
    {
        _time += Time.deltaTime;
        if(_time >= fireRate  && amountMissiles > 0)
        {
            Instantiate(enemySO.bullet, instancePoint.position, transform.rotation);
            _time = 0;
            amountMissiles = amountMissiles - 1;    
        }
    }
}
