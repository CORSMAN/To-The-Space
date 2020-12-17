using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackShip : SpaceShips
{
    GameController gameController;                                                  //Variable para modificar datos de GameController
    public GameObject misil;                                                        //GameObject para el misil
    public Transform instancePoint;                                                 //Posicion en la cual instanciamos al misil
    public int amountMissiles;                                                      //La cantidad de misiles    
    public Transform enemy;                                                         //Transform para obtener la posicion del enemigo
    private Rigidbody2D _rb;                                                        //Rigidbody que nos servira para poder rotar a la nave
    private float _time;                                                            //Flotante para controlar el tiempo y saber cuadno disparar
    public float fireRate;                                                          //Flotante para comparar el tiempo de disparo
    public float rotateSpeed;                                                       //Velocidad de rotacion
    public bool saveYou = false;                                                    //Bool para controlar cuando la nave se convertira en kamikaze para salvar al jugador
    public bool isAttacking = true;                                                 //Bool para saber cuando la nave esta atacando
    public float contSaveYou = 3f;                                                  //Flotante que servira para que depues de pasar 3 segundos saveYou se vuelva verdadero
    Bars bars;                                                                      //Variable para modificar datos del script bars                                                             
    Bars bars2;                                                                     //Variable para modificar datos del script bars   

    // Start is called before the first frame update
    void Start()
    {
        #region Obtenemos y Asignamos  valroes
        gameController = FindObjectOfType<GameController>();
        enemy = GameObject.FindGameObjectWithTag("Enemy").transform;
        bars = barFuselage.GetComponent<Bars>();
        bars2 = barFuel.GetComponent<Bars>();
        bars.SetQuantity(fuselageIntegrity);
        maxFuel = fuel;
        fuselageMaxIntegrity = fuselageIntegrity;
        bars.SetMaxQuantity(fuselageMaxIntegrity);
        bars2.SetMaxQuantity(maxFuel);
        bars2.SetQuantity(fuel);
        currentGravity = tempGravity;
        _rb = this.GetComponent<Rigidbody2D>();
        #endregion
    }
    
    void Update()
    {
        if (fuselageIntegrity <= 0)                                                  //Si la integridad(HP) llega a cero, llamamos al metodo IsDead
        {
            IsDead();
        }

        if (isAttacking)                                                             //si esta atacando, llamamos al metodo IsAttacking
        {
            IsAttacking();
        }

        
        if (canPressR && Input.GetKey(KeyCode.R))                                   //Si puede presiona R y la presiona significa que esta recargando combustible
        {
            IsRefueling();
        }
        transform.Translate(Vector3.down * currentGravity * Time.deltaTime);        //Con esta linea aplicamos gravedad
    }

    void FixedUpdate()
    {
        if (saveYou)                                                                //Si saveYou es verdadero, mandamos a llamar al metodo
        {
            SaveYou();
        }

    }

    #region Metodo para que la nave se vuelva kamikaze
    public void SaveYou()
    {
        if (saveYou)                                                                //Si saveYou es verdadero, deja de atacar
        {
            isAttacking = false;
        }

        contSaveYou = contSaveYou - 1 * Time.deltaTime;                             //Contadore para que se vuelva Kamikaze

        if (contSaveYou <= 0 && enemy != null)                                      //Si el contador llega a cero y el enemigo no es nulo, aplicamos rotacion y direccion al enemigo
        {
            Vector2 direction = (Vector2)enemy.position - _rb.position;
            direction.Normalize();
            float rotateAmount = Vector3.Cross(direction, transform.up).z;
            _rb.angularVelocity = -rotateAmount * rotateSpeed;
            _rb.velocity = transform.up * speed;

            float distanceThisFrame = speed * Time.deltaTime;                       //Incrementamos la velocidad a cada segundo
            transform.Translate(direction.normalized * distanceThisFrame, Space.World);                 //Hacemos que se mueva hacia la direccion indicada a la velocidad de distanceThisFrame
        }
    }
    #endregion

    #region Metodo que se encarga de cumplir el funcionamiento de ataque
    public void IsAttacking()
    {

        if (amountMissiles <= 0)                                                                        //Si la cantidad de misiles es menor o igual 0, volvemos true a saveYou
        {
            saveYou = true;
        }

        _time += Time.deltaTime;                                                                        //Incrementamos el tiepo

        if (_time >= fireRate && amountMissiles > 0 && gameController.allEnemies.Count > 0)             //Si el tiempo es mayor que la cadencia de tiro o igual que fireRate y la cantidad de misiles es mayor a y la lista de enemigos de gamecontroller es mayor a 0
        {
            Instantiate(misil, instancePoint.position, Quaternion.identity);                            //Instanciamos los misiles en la posicion indicada y con su reséctiva rotacion
            _time = 0;                                                                                  //Reiniciamos el tiempo
            amountMissiles = amountMissiles - 1;                                                        //Actualizamos la cantidad de misiles restandole 1
        }
    }
    #endregion

    //Sobreescribimos el metodo 
    public override void TakingDamage(float damage)
    {
        base.TakingDamage(damage);                                                                       //Le pasamos la cantidad de daño
    }

    //Cuando detecta una colision del tipo trigger
    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "EnemyBullet")                                                              //Si colisiona con una bala enemiga, recibe 50 de daño
        {
            TakingDamage(50);
        }
        if (collision.tag == "Asteroid")                                                                 //Si colisiona con un asteriode, recibe 50 de daño
        {
            TakingDamage(50);
        }
    }

   

    //Cuando detecta una colision 
    protected void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Platform")                                                      //Si colicionamos con la plataforma
        {
            currentGravity = tempGravity;                                                                //Actualizamos la gravedad
            canRefuel = true;                                                                            //Puede recargar combustible    
            isFlying = false;                                                                            //Ya no esta volando    
            canPressR = true;                                                                            //Puede presionar R   
        }

        if (collision.gameObject.tag == "Enemy")                                                         //Si colisiona con el enemigo
        {
            IsDead();                                                                                    //Lammamos al metodo
        }
    }

    //Cuando deja de detectar la colision
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Platform")                                                      //Ha dejado la plataforma
        {
            canRefuel = false;                                                                           //No puuede recargar combustible
            currentGravity = gravity;                                                                    //Actualizamos la gravedad

            isFlying = true;                                                                             //Esta volando
            Fly();                                                                                       //Llamamos al metodo
        }
    }
}
