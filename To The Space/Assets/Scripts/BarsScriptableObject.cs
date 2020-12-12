using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "bars", menuName = "Bars", order = 1)]
public class BarsScriptableObject : ScriptableObject
{

    public float capacity;                                                       //La declaramos estatica para que podamos modificarla desde cualquier otro script
    public float maxCapacity;                                                    //Variable que nos permitira controlar la capacidad maximacalled before the first frame update
}
