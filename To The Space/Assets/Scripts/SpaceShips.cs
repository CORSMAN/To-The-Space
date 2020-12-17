using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShips : MonoBehaviour
{
    [SerializeField] protected float fuel;                                                                  //Combustible actual    
    [SerializeField] protected float maxFuel;                                                               //Combustible maximo
    [SerializeField] protected float fuselageIntegrity;                                                     //Integridad de la nave (puntos de salud)
    [SerializeField] protected float fuselageMaxIntegrity;                                                  //Integridad maxima
    [SerializeField] protected float motorTemperature;                                                      //Con esto controlaremos la temperatura del motor
    protected bool canRefuel;                                                                               //Bool que nos servira para saber cuando puede recargar combustible
    protected bool isFlying;                                                                                //Bool que nos servira para saber cuando esta volando
    protected bool canPressR;                                                                               //Bool que nos servira para saber cuando se puede presionar la tecla R
    protected bool consumingFuel = false;                                                                   //bool que nos servira para detectar en que momento se esta consumiendo combustible
    protected float currentGravity;                                                                         //Flotante para controlar la gravedad actual
    protected float tempGravity = 0;                                                                        //Flotante para convertir la gravedad 0

    [SerializeField] protected float speed = 10;                                                            //Flotante para controlar la velocidad
    [SerializeField] protected float gravity = 2;                                                           //Flotante paara aplicar gravedad al momento de volar
    [SerializeField] protected GameObject barFuel;                                                          //Obtenemos este GameObject para poder modificar valores de la barra de combustible
    [SerializeField] protected GameObject barFuselage;                                                      //Obtenemos este GameObject para poder modificar valores de la barra de fuselage(HP)

    /// <summary>
    /// Metodo que nos ayudara a saber cuando la nave este volando
    /// y de esta manera controlaremos el repostaje y la cantidad de combustible
    /// </summary>
    protected void Fly()
    {
        if (isFlying)
        {
            canRefuel = false;                                                                              //Ya no puede recargar combustible
            if (consumingFuel)                                                                              //Si esta consumiendo combustible
            {
                fuel = fuel - 16f  * Time.deltaTime;                                                        //Conforme pasa el tiempo, el combustible se va consumiendo, aqui nos encargamos de irlo reduciendo
                barFuel.GetComponent<Bars>().SetQuantity(fuel);                                             //Actualizamos el valor de la barra de combustible
            }
        }
    }

    

    /// <summary>
    /// Metodo que se encarga de hacer el repostaje
    /// </summary>
    protected void IsRefueling()
    {
        if (canRefuel && fuel < maxFuel)                                                                     //Si canrefuel es verdadero y el combustible es menor que el maximo, repostamos
        {
            fuel += fuel * Time.deltaTime;                                                                   //Vamos incrementando la cantidad de combustible con el tiempo
            barFuel.GetComponent<Bars>().SetQuantity(fuel);                                                  //Actualizamos el valor de la barra de combustible
        }
    }

    /// <summary>
    /// Metodo para contorlar cuando recibedaño
    /// </summary>
    /// <param name="damage"></param>
    public virtual void TakingDamage(float damage)
    {
        fuselageIntegrity -= damage ;                                                                       //Conforme pasa el tiempo, el combustible se va consumiendo, aqui nos encargamos de irlo reduciendo
        barFuselage.GetComponent<Bars>().SetQuantity(fuselageIntegrity);                                    //Actualizamos el valor de la barra de combustible
    }

    /// <summary>
    /// Metodo que se encarga de destruir la nave cuando el jugador se queda sin integridad(HP)
    /// </summary>
    protected void IsDead()
    {
        Destroy(gameObject);                                                                                 //Destruimos el gameobject
    }
}
