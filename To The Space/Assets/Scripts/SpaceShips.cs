using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShips : MonoBehaviour
{
    protected GameObject ship;                              //GameObject que nos servira para el sprite de la nave
    protected float fuel = 200;                             //Combustible actual    
    protected float maxFuel = 200;                          //Combustible maximo
    protected int fuselageIntegrity;                        //Integridad de la nave (puntos de salud)
    protected int motorTemperature;                         //Con esto controlaremos la temperatura del motor
    protected bool canRefuel;                               //Bool que nos servira para saber cuando puede recargar combustible
    protected bool isFlying;                                //Bool que nos servira para saber cuando esta volando
    protected bool canPressR;                               //Bool que nos servira para saber cuando se puede presionar la tecla R
    protected bool consumingFuel = false;                           //bool que nos servira para detectar en que momento se esta consumiendo combustible
    [SerializeField] private Bars barFuel;

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

                Debug.Log("Volando");
                fuel -= fuel * Time.deltaTime;                      //Conforme pasa el tiempo, el combustible se va consumiendo, aqui nos encargamos de irlo reduciendo
                barFuel.capacity = fuel;
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
            Debug.Log("Se cumplio la condicion" + canRefuel + fuel + maxFuel);
            //fuel = Bars.maxCapacity;                      //Vamos incrementando la cantidad de combustible con el tiempo
            //Bars.capacity = fuel;
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
