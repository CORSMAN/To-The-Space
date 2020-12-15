using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class SaveSystem 
{
    public float score;
    public float playerPositionX;
    public float playerPositionY;
    public float tempDistanceY;
    public float tempDistanceX;

    ////Listas para guardar las posiciones enemigas
    //public List<float> enemyPositionX = new List<float>();
    //public List<float> enemyPositionY = new List<float>();
    //public List<bool> isDead = new List<bool>();
}
