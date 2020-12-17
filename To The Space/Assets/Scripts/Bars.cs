using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bars : MonoBehaviour
{

    public Slider slider;                                                                               //Variable para controlar el slider
    public Gradient gradient;                                                                           //Variable para controlar el gradient
    public Image fill;                                                                                  //Variable para poder emplear los "efectos" del gradient a la imagen
    public Image icon;                                                                                  //Imagen para poder controlar e interactuar con el icono que aparecera en la barra de vida
    public float transparency;

    public enum Mode { Neutral, Show, Hide };                                                           //Declaramos los estados
    public Mode mode;                                                                                   //Creamos una variable del tipo Mode

    private void Start()
    {
        mode = Mode.Neutral;                                                                            //Iniciamos mode coomo neutral

        icon.canvasRenderer.SetAlpha(1.0f);                                                             //Le decimos al icono que su alpha es de 1(se ve sin nada de transparencia)
    }

    private void Update()
    {
        #region Condiciones para que la imagen (icon) parpadee
        //Condicion para que comience a desaparecer
        if (slider.value < 80 && mode == Mode.Neutral)                                                  //Si el valor del slider es menor a 80 y Mode esta neutral
        {
            mode = Mode.Hide;                                                                           //Cambiamos a Mode a hide para que comience a ocultarlo
        }

        //Condicion para que no parpadee
        if(slider.value > 80)                                                                           //Si el valor del slider es mayor a 80
        {
            mode = Mode.Neutral;                                                                        //Cambiamos mode a neutral(que no parpadee
            if (transparency >= 1)                                                                      //Si la transparencia es mayor o igual a 1
            {
                transparency = 1;                                                                       //La igualamos a 1, esto es para evitar que sobrepase el limite
            }
            if (transparency < 1)                                                                       //Si la transparencia es menor a 1 
            {
                transparency += Time.deltaTime;                                                         //Incrementamos el valor de la transparencia
                icon.color = new Color(icon.color.r, icon.color.g, icon.color.b, transparency);         //Le decimos al icono que sus tres colores RGB se quedan igual y le pasamos su transparencia(alfa)
            }
        }

        //Condicion para empezar a desaparecer el icono
        if(mode == Mode.Hide)                                                                           //Si mode es iguala hide
        {
            if (transparency <= 0.01f)                                                                  //Si la transparencia es menor o igual a 0.01f                
            {
                transparency = 0.01f;                                                                   //La igualamos a 0.01, esto es para evitar que sobrepase el limite
                mode = Mode.Show;                                                                       //cambiamos mode a show  
            }
            if (transparency > 0.01f)                                                                   //Si la transparencia es mayor a 0.01 (osea que si se ve sin nada de transparencia)
            {
                transparency -= Time.deltaTime;                                                         //Comenzamos a disminuir la transparencia
                icon.color = new Color(icon.color.r, icon.color.g, icon.color.b, transparency);         //Le decimos al icono que sus tres colores RGB se quedan igual y le pasamos su transparencia(alfa)
            }
        }

        //Condicion para empezar a aparecer el icono
        if(mode == Mode.Show)                                                                           //Si mode es iguala show
        {
            if (transparency >= 1)                                                                      //Si la transparencia es mayor o igual a 1 
            {
                transparency = 1;                                                                       //La igualamos a 1, esto es para evitar que sobrepase el limite
                mode = Mode.Hide;                                                                       //cambiamos mode a hide
            }
            if(transparency < 1f)                                                                       //Si la transparencia es menor o igual a 1 
            {
                transparency += Time.deltaTime;                                                         //Comenzamos a incrementar la transparencia
                icon.color = new Color(icon.color.r, icon.color.g, icon.color.b, transparency);         //Le decimos al icono que sus tres colores RGB se quedan igual y le pasamos su transparencia(alfa)         
            }
        }
        #endregion

        if (slider.value < 1)                                                                            //Si el valor del slider es menor que 1  
        {
            slider.value = 0;                                                                            //Lo igualamos a 0, esto es para evitar problemas con los decimales
        }
    }
    /// <summary>
    /// Metodo que nos permite controlar la cantidad maxima
    /// </summary>
    /// <param name="quantity"></param>
    public void SetMaxQuantity(float quantity)
    {
        slider.maxValue = quantity;                                                                     //Le pasamos la cantidad maxima
        slider.value = quantity;                                                                        //Le pasamos el valor actual a value

        fill.color = gradient.Evaluate(1f);                                                             //obtenemos un color de nuestro gradient en un punto especifico

    }

    public void SetQuantity(float quantity)
    {
        slider.value = quantity;                                                                        //Le pasamos el valor de cantidad
        fill.color = gradient.Evaluate(slider.normalizedValue);                                         //Como nuestra cantidad maxima puede variar y el gradient solo funcion de 0-1 , al normalizarlo "cambiamos" los valores a 0-1
    }
}
