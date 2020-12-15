using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Enemy", menuName = "ScriptableObjects/EnemiesSO")]
public class EnemiesSO : ScriptableObject
{
    public string enemyName;
    public string enemyNum;
    public Color enemyColor;
    public GameObject bullet;//Gameobject para la bala
    public float stopDistance;//Distancia ala que se detendra
    public float speed = 10;
}
