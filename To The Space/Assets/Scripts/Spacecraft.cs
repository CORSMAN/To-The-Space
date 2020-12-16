using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Spacecraft : SpaceShips
{
    GameController gameController;
    Camera cam;
    public static Spacecraft sc;
    Bars bars;
    Bars bars2;
    public bool enoughFuel = true;
    public float gameOverCont = 3;
    public bool gameOver = false;

    void Start()
    {
        gameController = FindObjectOfType<GameController>();
        cam = Camera.main;
        currentGravity = gravity;
        bars = barFuselage.GetComponent<Bars>();
        bars2 = barFuel.GetComponent<Bars>();
        maxFuel = fuel;
        fuselageMaxIntegrity = fuselageIntegrity;
        bars.SetQuantity(fuselageIntegrity);
        bars.SetMaxQuantity(fuselageMaxIntegrity);
        bars2.SetMaxQuantity(maxFuel);
        bars2.SetQuantity(fuel);
    }
    
    void Update()
    {
        if(fuel <= 1)
        {
            fuel = 0;
            enoughFuel = false;
        }
        if(fuselageIntegrity <= 0)
        {
            IsDead();
            GameController.gc.gameOver = true;
        }

        cam.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, -10);//Codigo para que la camara siga al jugador

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -7.31f, 7.66f), 
            (transform.position.y), transform.position.z);//Limitamos la capacidad de movimiento lateral del jugador

        //Movimiento del jugador y recarga de combustible
        #region
        if (canPressR && Input.GetKey(KeyCode.R))
        {
            IsRefueling();
        }

        
       

        if (enoughFuel)
        {
            if (Input.GetKey(KeyCode.W))                                             //Si se mantiene presionada W la nave se mueve hacia arriba y consume combustible
            {

                if (gameController.currentDistance.position.y > gameController.tempDistance.position.y)
                {

                    gameController.scoring = true;
                    gameController.tempDistance.position = gameController.currentDistance.position;
                }
                transform.Translate(Vector3.up * speed * Time.deltaTime);
                consumingFuel = true;
                isFlying = true;
                Fly();
            }
            if (Input.GetKeyUp(KeyCode.W))
            {
                consumingFuel = false;                                               //Si se Suelta W la nave deja de consumir combustible
                isFlying = false;

                Falling();
                Fly();
            }

        }

        if (!gameOver)
        {

            if (Input.GetKey(KeyCode.A))                                             //Si se mantiene presionada A la nave se mueve hacia la izquierda
            {
                transform.Translate(Vector3.left * speed * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.D))                                             //Si se mantiene presionada D la nave se mueve hacia la derecha
            {
                transform.Translate(Vector3.right * speed * Time.deltaTime);
            }

            transform.Translate(Vector3.down * currentGravity * Time.deltaTime);     //Con esta linea aplicamos gravedad
        }

        if (!enoughFuel)
        {
            gameOverCont -= Time.deltaTime;
            if(gameOverCont <= 0)
            {
                gameOver = true;
            }
        }
        #endregion
    }

    

    protected void Falling()
    {
        gameController.scoring = false;
        if (gameController.tempDistance.position.y >= gameController.initialDistance.position.y)
        {
            gameController.initialDistance.position = gameController.tempDistance.position;
        }
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Checkpoint")                                       //Si colicionamos con un checkpoint
        {
            
            //texto que indica que se ha alcanzado un nuevo checkpoint
        }
        if (collision.tag == "EnemyBullet")
        {
            TakingDamage(50);
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        
    }

    protected void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Platform")                              //Si colicionamos con la plataforma
        {
            GameController.gc.SaveBySerialization();
            currentGravity = tempGravity;
            canRefuel = true;
            isFlying = false;
            canPressR = true;
            consumingFuel = false;
        }

        if (collision.gameObject.tag == "Base")                              //Si colicionamos con la plataforma
        {
            Debug.Log("Estoy en la base");
            GameController.gc.SaveBySerialization();
            currentGravity = tempGravity;
            currentGravity = tempGravity;
            canRefuel = true;
            isFlying = false;
            canPressR = true;
            consumingFuel = false;
        }
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.SetActive(false);
            IsDead();
        }

        if (collision.gameObject.tag == "EnemyBullet")
        {
            //Spacecraft.sc.TakingDamage(50);
        }
    }



    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Platform")
        {

            canRefuel = false;
            currentGravity = gravity;

            isFlying = true;
            Fly();                                                               //Llamamos al metodo
        }
        if (collision.gameObject.tag == "Base")
        {
            currentGravity = gravity;
        }
    }
}
