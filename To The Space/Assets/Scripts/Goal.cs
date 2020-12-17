using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    public GameObject win;                                                          //GameObject que nos ayudara a controlar cuando activar el mensaje de que has ganado
    Spacecraft spaceCraft;                                                          //Variable para modificar datos de SpaceCRaft
    GameController gameController;                                                  //Variable para modificar datos de GameController
    // Start is called before the first frame update
    void Start()
    {
        spaceCraft = FindObjectOfType<Spacecraft>();                                //Buscamos y obtenemos todos sus componentes
        gameController = FindObjectOfType<GameController>();                        //Buscamos y obtenemos todos sus componentes
    }

    private void OnTriggerEnter2D(Collider2D col)                                   //Detectamos colisiones
    {

        if (col.gameObject.tag == "Player")                                         //Si colisiona con el player modificamos todos estos datos 
        {
            spaceCraft.enoughFuel = false;
            spaceCraft.gameOver = true;
            spaceCraft.win = true;
            gameController.scoring = false;
            win.gameObject.SetActive(true);
        }
    }
}
