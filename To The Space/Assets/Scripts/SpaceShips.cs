using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShips : MonoBehaviour
{
    //[SerializeField] protected GameObject ship;                              //GameObject que nos servira para el sprite de la nave
    [SerializeField] protected float fuel;                             //Combustible actual    
    [SerializeField] protected float maxFuel;                          //Combustible maximo
    [SerializeField] protected int fuselageIntegrity;                        //Integridad de la nave (puntos de salud)
    [SerializeField] protected int fuselageMaxIntegrity;                     //Integridad maxima
    [SerializeField] protected int motorTemperature;                         //Con esto controlaremos la temperatura del motor
    protected bool canRefuel;                               //Bool que nos servira para saber cuando puede recargar combustible
    protected bool isFlying;                                //Bool que nos servira para saber cuando esta volando
    protected bool canPressR;                               //Bool que nos servira para saber cuando se puede presionar la tecla R
    protected bool consumingFuel = false;                           //bool que nos servira para detectar en que momento se esta consumiendo combustible
    protected float currentGravity;                                                        //Flotante para controlar la gravedad actual
    protected float tempGravity = 0;                                                       //Flotante para convertir la gravedad 0

    [SerializeField] protected float speed = 10;                                                          //Flotante para controlar la velocidad
    [SerializeField] protected float gravity = 2;                                                        //Flotante paara aplicar gravedad al momento de volar
    [SerializeField] protected Bars barFuel;
    [SerializeField] protected Bars barFuselage;

    /// <summary>
    /// Metodo que nos ayudara a saber cuando la nave este volando
    /// y de esta manera controlaremos el repostaje y la cantidad de combustible
    /// </summary>
    protected void Fly()
    {
        if (isFlying)
        {
            canRefuel = false;                                  //Lo volvemos falso
            if (consumingFuel)
            {
                fuel -= fuel * Time.deltaTime;                      //Conforme pasa el tiempo, el combustible se va consumiendo, aqui nos encargamos de irlo reduciendo
                barFuel.SetQuantity(fuel);
            }
        }
    }

    

    /// <summary>
    /// Metodo que se encarga de hacer el repostaje
    /// </summary>
    protected void IsRefueling()
    {
        if (canRefuel && fuel < maxFuel)                        //Si canrefuel es verdadero y el combustible es menor que el maximo, repostamos
        {
            fuel += fuel * Time.deltaTime;                      //Vamos incrementando la cantidad de combustible con el tiempo
            barFuel.SetQuantity(fuel);
        }
    }

    protected void IsLanding()
    {
        //comienza animacion de cohetes
    }

    /// <summary>
    /// Metodo que se encarga de destruir la nave cuando el jugador se queda sin integridad(HP)
    /// </summary>
    protected void IsDead()
    {
        Destroy(gameObject);                                    //Destruimos el gameobject
        //reproducir animacion de explosion
    }

}
