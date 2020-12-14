using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour
{
    public Transform player;
    public Transform instancePoint;//Transofrm que nos servira para instanciar la bala
    public GameObject bullet;//Gameobject para la bala
    [SerializeField] private float _stopDistance;//Distancia ala que se detendra
    [SerializeField] protected float speed = 10;
    private Rigidbody2D _rb;
    private float _time;//Flotante para controlar el tiempo de disparo
    // Start is called before the first frame update
    void Start()
    {
        _rb = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Follow();
        Shooting();
    }

    //Metodo para comenzar a seguir y ver al jugador
    void Follow()
    {
        if (player != null)
        {
            if (Vector2.Distance(transform.position, player.position) > _stopDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            }
            if (Vector2.Distance(transform.position, player.position) < _stopDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.position, -speed * Time.deltaTime);
            }
            Vector3 direction = player.position - transform.position;//Obtenemos la distancia
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;//Obtenemos el angulo entre Y y X y lo convertimos en grados 
            _rb.rotation = angle;//Le damos la rotacion

        }
    }

    void Shooting()
    {
        _time += Time.deltaTime;
        if(_time >= 2)
        {
            Instantiate(bullet, instancePoint.position, Quaternion.identity);
            _time = 0;
                
        }
    }
}
