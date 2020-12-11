using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShips : MonoBehaviour
{
    protected GameObject ship;                              //GameObject que nos servira para el sprite de la nave
    protected float fuel;                                   //Combustible actual    
    protected float maxFuel;                                //Combustible maximo
    protected int fuselageIntegrity;                        //Integridad de la nave (puntos de salud)
    protected int motorTemperature;                         //Con esto controlaremos la temperatura del motor
    protected bool canRefuel;                               //Bool que nos servira para saber cuando puede recargar combustible
    protected bool isFlying;

    /// <summary>
    /// Metodo que nos ayudara a saber cuando la nave este volando
    /// y de esta manera controlaremos el repostaje y la cantidad de combustible
    /// </summary>
    protected void Fly()
    {
        if (isFlying)
        {
            Debug.Log("Volando");
            canRefuel = false;                                  //Lo volvemos falso
            fuel -= fuel * Time.deltaTime;                      //Conforme pasa el tiempo, el combustible se va consumiendo, aqui nos encargamos de irlo reduciendo
        }
    }

    /// <summary>
    /// Metodo que se encarga de hacer el repostaje
    /// </summary>
    protected void IsRefueling()
    {
        if (canRefuel && fuel < maxFuel)                    //Si canrefuel es verdadero y el combustible es menor que el maximo, repostamos
        {
                fuel += fuel * Time.deltaTime;              //Vamos incrementando la cantidad de combustible con el tiempo
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
        Destroy(gameObject);                                //Destruimos el gameobject
        //reproducir animacion de explosion
    }

}
