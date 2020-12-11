using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShips : MonoBehaviour
{
    protected GameObject mainEngine;
    protected GameObject stabilizers;
    protected GameObject compartments;
    protected GameObject fuelTank;
    protected GameObject hatchCrew;
    protected GameObject thermalProtectionTiles;
    protected GameObject booth;
    protected float fuel;
    protected float maxFuel;
    protected int fuselageIntegrity;
    protected int motorTemperature;
    protected bool canRefuel = false;

    protected void IsFlying()
    {
        canRefuel = false;
        fuel -= fuel * Time.deltaTime;
    }

    protected void IsRefueling()
    {
        if (canRefuel && fuel < maxFuel)
        {
                fuel += fuel * Time.deltaTime;
        }
    }

    protected void IsLanding()
    {

    }

    protected void IsDead()
    {
        Destroy(gameObject);
        //reproducir animacion de explosion
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Checkpoint")
        {

        }
    }

    protected void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Aliado")
        {
            canRefuel = true;
        }
    }

}
