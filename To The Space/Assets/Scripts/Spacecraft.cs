using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Spacecraft : SpaceShips
{
    GameController gameController;                                                  //Variable para modificar datos de GameController
    Camera cam;                                                                     //Creamos esta linea para poder interactuar con la camara
    Bars bars;                                                                      //Variable para modificar datos del script bars                                                             
    Bars bars2;                                                                     //Variable para modificar datos del script bars   
    public bool enoughFuel = true;                                                  //Bool para saber cuando se le ha acabado el combustible
    public float gameOverCont = 3;                                                  //Contador para esperar y mostrar el mensaje de game over
    public bool gameOver = false;                                                   //Bool para controlar cuando se le muestra el mensaje de game over
    public bool win = false;                                                        //Bool para saber cuando el jugador ha ganado
    public GameObject loseFuel;                                                     //GameObject que nos servira para enseñar el mensaje cuando se perdio por falta de combustible
    public GameObject thisGameObject;                                               //GameObject para activar y desactivar al jugador(SpaceCRaft)

    void Start()
    {
        #region Obtenemos y Asignamos  valroes
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
        #endregion
    }

    void Update()
    {
        if(fuel <= 1)                                                               //Si combustible es menor a 1, lo igualamos a cero para evitar un problema de decimales y hacemos a enough fuel falso
        {
            fuel = 0;
            enoughFuel = false;
        }
        if(fuselageIntegrity <= 0)                                                  //Si la integrida (HP) llega a cero, le decimos a game controller que game over es verdadero y desactivamos SpaceCraft
        {
            gameController.gameOver = true;
            thisGameObject.SetActive(false);
        }

        cam.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 1.8f, -10);         //Codigo para que la camara siga al jugador

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -7.31f, 7.66f), 
            (transform.position.y), transform.position.z);                                                              //Limitamos la capacidad de movimiento lateral del jugador

        //Movimiento del jugador y recarga de combustible
        #region
        if (canPressR && Input.GetKey(KeyCode.R))                                                                       //Si puede presiona R y la presiona significa que esta recargando combustible
        {
            IsRefueling();
        }

        
       

        if (enoughFuel)                                                                                                 //Si tiene combustible puede apretar estas teclas
        {
            if (Input.GetKey(KeyCode.W))                                                                                //Si se mantiene presionada W la nave se mueve hacia arriba y consume combustible
            {

                if (gameController.currentDistance.position.y > gameController.tempDistance.position.y)                 //Si la distancia actual es mayor que la distancia temporal, entonces podemos seguir puntuando y a la distancia temporal le psamos el valor de la distancia actual
                {

                    gameController.scoring = true;
                    gameController.tempDistance.position = gameController.currentDistance.position;
                }
                transform.Translate(Vector3.up * speed * Time.deltaTime);
                consumingFuel = true;
                isFlying = true;
                Fly();                                                                                                  //Llamamos al metodo
            }
            if (Input.GetKeyUp(KeyCode.W))                                                                              //Si el jugador suelta W, deja de consumir combustible y comienza a caer
            {
                consumingFuel = false;
                isFlying = false;

                Falling();                                                                                              //Llamamos al metodo
                Fly();                                                                                                  //Llamamos al metodo
            }

        }

        if (!gameOver)                                                                                                  //Si ha perdido ya no puede presionar estas teclas
        {

            if (Input.GetKey(KeyCode.A))                                                                                //Si se mantiene presionada A la nave se mueve hacia la izquierda
            {
                transform.Translate(Vector3.left * speed * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.D))                                                                                //Si se mantiene presionada D la nave se mueve hacia la derecha
            {
                transform.Translate(Vector3.right * speed * Time.deltaTime);
            }

            transform.Translate(Vector3.down * currentGravity * Time.deltaTime);                                        //Con esta linea aplicamos gravedad
        }

        if (!enoughFuel)                                                                                                //Si no tiene combustible iniciamos el contador para mostrar el mensaje de game over y cuando llega a cero volvemos tru la variable gameOver
        {
            gameOverCont -= Time.deltaTime;
            if(gameOverCont <= 0)
            {
                gameOver = true;
            }
        }
        if (gameOver && !win)                                                                                           //Si gameOver es verdadero y win es falso, enseñamos el mensaje que ha perdido por falta de combustible y deja de puntuar
        {
            loseFuel.gameObject.SetActive(true);
            gameController.scoring = false;
        }
        #endregion
    }


    #region Metodo para saver cuando el jugador esta callendo y asi dejar de puntuar
    protected void Falling()
    {
        gameController.scoring = false;
        if (gameController.tempDistance.position.y >= gameController.initialDistance.position.y)                         //Actualizamos la distancia que debe superar para comenzar a puntuar de nuevo
        {
            gameController.initialDistance.position = gameController.tempDistance.position;
        }
    }
    #endregion

    //Cuando detecta una colission del tipo trigger
    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "EnemyBullet")                                                                             //Si colisiona con una bala enemiga, recibe 50 de daño
        {
            TakingDamage(50);                                                           
        }
        if (collision.tag == "Asteroid")                                                                                //Si colisiona con un asteriode, recibe 50 de daño
        {
            TakingDamage(50);
        }
    }

    //Cuando detecta una colision 
    protected void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Platform")                                                                     //Si colicionamos con la plataforma hacemos un guardado de partida
        {
            gameController.SaveBySerialization();                                                                       //Hacemos un guardado de partida
            currentGravity = tempGravity;                                                                               //Actualizamos la gravedad
            canRefuel = true;                                                                                           //Puede recargar combustible    
            isFlying = false;                                                                                           //Ya no esta volando    
            canPressR = true;                                                                                           //Puede presionar R   
            consumingFuel = false;                                                                                      //Deja de consumir combustible    
        }

        if (collision.gameObject.tag == "Base")                                                                         //Si colisiona con la base
        {
            gameController.SaveBySerialization();                                                                       //Guardamos la partida
            currentGravity = tempGravity;                                                                               //Actualizamos la gravedad
            canRefuel = true;                                                                                           //Puede recargar combustible
            isFlying = false;                                                                                           //Ya no esta volando    
            canPressR = true;                                                                                           //Puede presionar R   
            consumingFuel = false;                                                                                      //Deja de consumir combustible  
        }
        if (collision.gameObject.tag == "Enemy")                                                                        //Si colisiona con el enemigo
        {
            collision.gameObject.SetActive(false);                                                                      //Desactivamos al enemigo
            IsDead();                                                                                                   //Lammamos al metodo
        }
    }


    //Cuando deja de detectar una colision
    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Platform")                                                                      //Ha dejado la plataforma
        {
            canRefuel = false;                                                                                           //No puuede recargar combustible
            currentGravity = gravity;                                                                                    //Actualizamos la gravedad

            isFlying = true;                                                                                             //Esta volando
            Fly();                                                                                                       //Llamamos al metodo
        }
        if (collision.gameObject.tag == "Base")                                                                          //Actualizamos gravedad
        {
            currentGravity = gravity;
        }
    }
}
