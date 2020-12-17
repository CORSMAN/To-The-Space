using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    private float _time = 2;                                                        //El tiempo que debe pasar para instanciar a un nuevo asteroide
    public GameObject asteroid;                                                     //GameObject para guardar a los asteroides a instanciar

    void Update()
    {
        Spawning();                                                                 //Llamamos al metodo
    }
    #region Metodo para instanciar a los asteroides 
    void Spawning()
    {
        _time += Time.deltaTime;                                                    //Incrementamos el tiempo
        if (_time >= 2)                                                             //Si el tiempo es mayor que 2
        {
            Instantiate(asteroid,transform.position,Quaternion.identity);           //Instanciamos los asteroides en esta posicion y con su respecctiva rotacion    
            _time = 0;                                                              //Reiniciamos el tiempo
        }
    }
    #endregion
}
