using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour
{
    Material mat;
    public float paralax = 2f;
    Vector2 background;
    Transform cam;
    // Start is called before the first frame update
    void Start()
    {
        SpriteRenderer sp = GetComponent<SpriteRenderer>();
        mat = sp.material;
        cam = Camera.main.transform;
        background = mat.mainTextureOffset;//Le asignamos la posicion
    }

    // Update is called once per frame
    void Update()
    {
        MoveBackground();
    }

    void MoveBackground()
    {
        background.y = cam.position.x / transform.localScale.x / paralax;//a mas paralax, mas lento se mueve
        background.y = cam.position.y / transform.localScale.y / paralax;
        mat.mainTextureOffset = background;
    }
}
