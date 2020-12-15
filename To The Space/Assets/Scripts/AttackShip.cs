using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackShip : SpaceShips
{
    public GameObject shield;
    public GameObject misil;
    public Transform instancePoint;
    public int amountMissiles;
    public bool enemyMissileDetected;
    public Transform enemy;
    private Rigidbody2D _rb;
    private float _time;//Flotante para controlar el tiempo y saber cuadno disparar
    public float fireRate;//Flotante para comparar el tiempo de disparo
    public float rotateSpeed;
    public bool saveYou = false;
    public float contSaveYou = 3f;

    // Start is called before the first frame update
    void Start()
    {
        barFuselage = FindObjectOfType<Bars>();
        barFuel = FindObjectOfType<Bars>();
        barFuselage.SetQuantity(fuselageIntegrity);
        maxFuel = fuel;
        fuselageMaxIntegrity = fuselageIntegrity;
        barFuselage.SetMaxQuantity(fuselageMaxIntegrity);
        barFuel.SetMaxQuantity(maxFuel);
        barFuel.SetQuantity(fuel);
        currentGravity = tempGravity;
        _rb = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        IsAttacking();
        EnemyDetected();
        //Control del consumo y recarga de combustible
        #region
        if (canPressR && Input.GetKey(KeyCode.R))
        {
            IsRefueling();
        }
        transform.Translate(Vector3.down * currentGravity * Time.deltaTime);     //Con esta linea aplicamos gravedad
        if (Input.GetKey(KeyCode.W))                                             //Si se mantiene presionada W la nave se mueve hacia arriba y consume combustible
        {
            //transform.Translate(Vector3.up * speed * Time.deltaTime);
            consumingFuel = true;
            isFlying = true;
            Fly();
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            consumingFuel = false;                                               //Si se Suelta W la nave deja de consumir combustible
            isFlying = false;
            Fly();
        }
        #endregion
    }
    void FixedUpdate()
    {
        if (saveYou)
        {
            SaveYou();
        }

    }

    public void SaveYou()
    {
        contSaveYou = contSaveYou - 1 * Time.deltaTime;
        if (contSaveYou <= 0 && enemy != null)
        {
            Vector2 direction = (Vector2)enemy.position - _rb.position;
            direction.Normalize();
            float rotateAmount = Vector3.Cross(direction, transform.up).z;
            _rb.angularVelocity = -rotateAmount * rotateSpeed;
            _rb.velocity = transform.up * speed;

            float distanceThisFrame = speed * Time.deltaTime;
            transform.Translate(direction.normalized * distanceThisFrame, Space.World);
        }
    }

    public void EnemyDetected()
    {
        
    }

    public void IsAttacking()
    {

        if (amountMissiles <= 0)
        {
            saveYou = true;
        }
        _time += Time.deltaTime;
        if (_time >= fireRate && amountMissiles > 0)
        {
            Instantiate(misil, instancePoint.position, Quaternion.identity);
            _time = 0;
            amountMissiles = amountMissiles - 1;
        }
    }

    public void MissileDetected()
    {

    }

    public void Intersecting()
    {

    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Checkpoint")                                       //Si colicionamos con un checkpoint
        {
            //texto que indica que se ha alcanzado un nuevo checkpoint
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "FirstPoint")
        {
            //isFlying = true;
            //Fly();
        }
    }

    protected void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Platform")                              //Si colicionamos con la plataforma
        {
            currentGravity = tempGravity;
            canRefuel = true;
            isFlying = false;
            canPressR = true;
            consumingFuel = false;
        }

        if (collision.gameObject.tag == "Enemy")
        {
            IsDead();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            canRefuel = false;
            currentGravity = gravity;

            isFlying = true;
            Fly();                                                               //Llamamos al metodo
        }
    }
}
