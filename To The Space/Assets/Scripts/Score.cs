using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    public Transform player;//Transform para guardar la posicion del jugador, nos servira para hacer una comparacion mas a delante y asi saber si se suman puntos o no
    public Transform currentDistance;//Transform para ir guardando la distancia y comparar 
    private float _time = 0;//Flotante privada para controlar el tiempo y comparar con scoreTime
    public float scoreTime;//Flotante para controlar cada que tiempo se le sumara un punto al jugador

    void Start()
    {
        currentDistance = player;
    }

    // Update is called once per frame
    void Update()
    {
        _time += Time.deltaTime;
        if (player.transform.position.y >= currentDistance.transform.position.y && _time >= scoreTime)
        {

        }
    }
}
