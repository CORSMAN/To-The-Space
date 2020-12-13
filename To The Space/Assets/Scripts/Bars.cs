using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bars : MonoBehaviour
{

    public Slider slider;//Variable para controlar el slider
    public Gradient gradient;//Variable para controlar el gradient
    public Image fill;//Variable para poder emplear los "efectos" del gradietn ala imagen
    public Image icon;
    public float transparency;

    public enum Mode { Neutral, Show, Hide };
    public Mode mode;

    private void Start()
    {
        mode = Mode.Neutral;

        icon.canvasRenderer.SetAlpha(1.0f);
    }
    private void Update()
    {
        if (slider.value < 50 && mode == Mode.Neutral)
        {
            mode = Mode.Hide;
        }
        if(slider.value > 50)
        {
            mode = Mode.Neutral;
            if (transparency >= 1)
            {
                transparency = 1;
            }
            if (transparency < 1f)
            {
                transparency += Time.deltaTime;
                icon.color = new Color(icon.color.r, icon.color.r, icon.color.b, transparency);
            }
        }

        if(mode == Mode.Hide)
        {


            if (transparency <= 0.01f)
            {
                transparency = 0.01f;
                mode = Mode.Show;
            }
            if (transparency > 0.01f)
            {
                transparency -= Time.deltaTime;
                icon.color = new Color(icon.color.r, icon.color.r, icon.color.b, transparency);
            }
        }

        if(mode == Mode.Show)
        {
            if (transparency >= 1)
            {
                transparency = 1;

                mode = Mode.Hide;
            }
            if(transparency < 1f)
            {
                transparency += Time.deltaTime;
                icon.color = new Color(icon.color.r, icon.color.r, icon.color.b, transparency);
            }
        }

        if (slider.value < 1)
        {
            slider.value = 0;
        }
    }
    /// <summary>
    /// Metodo que nos permite controlar la cantidad maxima
    /// </summary>
    /// <param name="quantity"></param>
    public void SetMaxQuantity(float quantity)
    {
        slider.maxValue = quantity;//Le pasamos la cantidad maxima
        slider.value = quantity;//Le pasamos el valor actual a value

        fill.color = gradient.Evaluate(1f);//obtenemos un color de nuestro gradient en un punto especifico

    }

    public void SetQuantity(float quantity)
    {
        slider.value = quantity;
        fill.color = gradient.Evaluate(slider.normalizedValue);//Como nuestra cantidad mazima puede variar y el gradient solo funcion de 0-1 , al normalizarlo "cambiamos" los valores a 0-1
    }
}
