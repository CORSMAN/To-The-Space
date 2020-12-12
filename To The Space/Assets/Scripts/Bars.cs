using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bars : MonoBehaviour
{
    [SerializeField] private BarsScriptableObject barFuel;


    public RectTransform recTransform;                                              //Este rectranform nos ayudara a interactuar con la barra de combustible
    public float capacity;                                                       //La declaramos estatica para que podamos modificarla desde cualquier otro script
    public float maxCapacity;                                                    //Variable que nos permitira controlar la capacidad maximacalled before the first frame update

    void Start()
    {
        capacity = barFuel.capacity;
        maxCapacity = barFuel.maxCapacity;
        recTransform = GetComponent<RectTransform>();
    }


    void Update()
    {
        float updateBar = capacity;   //Creamos una variable temporal. moverse hacia cierto numero a una velocidad especifica 
        recTransform.sizeDelta = new Vector2(14.5f, Mathf.Clamp(updateBar, 0.0f, maxCapacity));//(valor en X, modificamos valor en Y con un limite negativo y uno positivo
        if(capacity < 1)
        {
            capacity = 0;
            //recTransform.sizeDelta = new Vector2(14.5f,0);//(valor en X, modificamos valor en Y con un limite negativo y uno positivo

        }
    }

}
