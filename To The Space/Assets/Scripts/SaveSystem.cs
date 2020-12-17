using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class SaveSystem 
{
    public float score;                     //Flotante para guardar la puntuacion
    public float playerPositionX;           //Flotante para guardar la posicion del player en x
    public float playerPositionY;           //Flotante para guardar la posicion del player en y
    public float tempDistanceY;             //Flotante para guardar la posicion en y que se utilizara cuando sea una nueva partida
    public float tempDistanceX;             //Flotante para guardar la posicion en x que se utilizara cuando sea una nueva partida
}
