using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Enemy", menuName = "ScriptableObjects/EnemiesSO")]
public class EnemiesSO : ScriptableObject
{
    public string enemyName;                //Nombre del enemigo
    public string enemyNum;                 //Numero de enemigo
    public Color enemyColor;                //Color del enemigo
    public GameObject bullet;               //Gameobject para la bala
    public float stopDistance;              //Distancia a la que se detendra
    public float speed = 10;                //Velocidad a la que se movera
}
