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
    private float _time;//Flotante para controlar el tiempo de disparo

    // Start is called before the first frame update
    void Start()
    {
        maxFuel = fuel;
        fuselageMaxIntegrity = fuselageIntegrity;
        barFuselage.SetQuantity(fuselageIntegrity);
        barFuselage.SetMaxQuantity(fuselageMaxIntegrity);
        barFuel.SetMaxQuantity(maxFuel);
        barFuel.SetQuantity(fuel);
        currentGravity = tempGravity;
    }

    // Update is called once per frame
    void Update()
    {
        IsAttacking();
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
            Debug.Log("deje de presionar w");
            consumingFuel = false;                                               //Si se Suelta W la nave deja de consumir combustible
            isFlying = false;
            Fly();
        }
        #endregion
    }



    public void EnemyDetected()
    {

    }

    public void IsAttacking()
    {
        _time += Time.deltaTime;
        if (_time >= 2)
        {
            Instantiate(misil, instancePoint.position, Quaternion.identity);
            _time = 0;

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

        if (collision.gameObject.tag == "Enemy")
        {
            IsDead();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            Debug.Log("Sali de la primera plataforma");
            canRefuel = false;
            currentGravity = gravity;

            isFlying = true;
            Fly();                                                               //Llamamos al metodo
        }
    }
}
