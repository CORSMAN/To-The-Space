using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour
{
    Material mat;                                                           //Cremos una variable del tipo material
    public float paralax = 2f;                                              //Velocidad 
    Vector2 background;                                                     //Creamos un vector2
    Transform cam;                                                          //Creamos una variable del tipo transform para obtener la posicion de la camara
    // Start is called before the first frame update
    void Start()
    {
        SpriteRenderer sp = GetComponent<SpriteRenderer>();                 //Creamos una variable del tipo spriterenderer y obtenemos su componentes
        mat = sp.material;                                                  //Le asignamos el material de sp
        cam = Camera.main.transform;                                        //Le asignamos la main camera
        background = mat.mainTextureOffset;                                 //Le asignamos la posicion
    }

    // Update is called once per frame
    void Update()
    {
        MoveBackground();                                                   //Llamamos al metodo
    }

    void MoveBackground()
    {
        //background.y = cam.position.x / transform.localScale.x / paralax; //Si queremos que se mueva en el eje X
        background.y = cam.position.y / transform.localScale.y / paralax;   //Como solo nos interesa el eje X, solo utilizamos Y. A mas paralax, mas lento se mueve
        mat.mainTextureOffset = background;                                 //Le asignamos el valor de background
    }
}
