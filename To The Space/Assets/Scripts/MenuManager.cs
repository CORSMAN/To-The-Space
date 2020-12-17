using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void MainMenu()                      //Cuando hace click en el boton, cargamos la escena del menu principal
    {
        SceneManager.LoadScene(0);
    }

    public void PlayButton()                    //Cuando hace click en el boton, cargamos la escena del juego y le indicamos al GameController que es una nueva partida
    {

        GameController.isNewGame = true;
        SceneManager.LoadScene(1);
    }

    public void LoadButton()                    //Cuando hace click en el boton, cargamos la escena del juego y tambien le decimos a GameController que no es una nueva partidad
    {
        GameController.isNewGame = false;

        SceneManager.LoadScene(1);
    }

    public void Exit()                          //Cuando le da click al boton, se cierra el juego
    {
        Application.Quit();
    }
}
