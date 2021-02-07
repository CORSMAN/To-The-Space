using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class GameController : MonoBehaviour
{
    public GameObject spaceCraft;                                                          //GameObject que nos ayudara a obtener las posiciones del jugador y poder guardarla en otro metodo
    public GameObject loseFuselage;                                                        //GameObject que nos ayudara mostrar el mensaje que ha perdido por falta de combustible
    public GameObject controlsImage;                                                       //GameObject que nospermitira enseñar y quitar las instrucciones.

    public static bool isNewGame = true;                                                   //Bool para saber cuando es un nuevo juego y cargarle datos predefinidos
    public Transform initialDistance;                                                      //Transform para guardar la posicion del jugador, nos servira para hacer una comparacion mas a delante y asi saber si se suman puntos o no
    public Transform currentDistance;                                                      //Transform para ir guardando la distancia y comparar 
    public Transform tempDistance;                                                         //Transform para ir guardar la distancia temporal
    public float score = 0;                                                                //Flotante para controlar la puntuacion
    public List<Enemies> allEnemies = new List<Enemies>();                                 //Lista que alamcena todos los enemigos para llevar un control de estos y evitar errores con los misiles
    public bool gameOver = false;                                                          //Bool para saber cuando el jugador ha perdido
    public float showImg = 0;                                                              //Flotante para controlar en que tiempo desaparecera la instruccion

    public Text txtScore;                                                                  //Text para obtener los componentes del text dentro del juego y asi poder modificarlos
    public bool scoring = false;                                                           //Bool para controlar cuando el jugadpor pueder puntuar o no
        
    void Start()
    {
        SaveSystem saveSystem = new SaveSystem();

        //Si es un juego nuevo le pasamos estos valores
        if (isNewGame)                                                                     
        {
            saveSystem.score = 0;
            saveSystem.playerPositionX = 0;
            saveSystem.playerPositionY = -3.219f;
            saveSystem.tempDistanceY = -3.219f;
            saveSystem.tempDistanceX = 0;
        }
        //Sino Cargamos la partida previamente guardada
        else
        {
            LoadBySerilization();
        }

        txtScore.text = "Score:" +  score.ToString("F1") + "Mts.";                           //Al iniciar el juego mostramos el texto de puntuacion con estos valores
        initialDistance.position = currentDistance.position;                                 //Igualamos la distancia inicial a la actual
    }

    // Update is called once per frame
    
    void Update()
    {
        //Si podemos puntuar y la distancia inicial es mayor que la actual entonces sumamos puntos
        if (scoring && initialDistance.transform.position.y < currentDistance.transform.position.y)
        {
            score += Time.deltaTime;
            txtScore.text = "Score:"  + score.ToString("F1") + "Mts.";
        }
        //Si gameOver es verdader, mostramos el mensaje de que se ha perdido
        if (gameOver)
        {
            loseFuselage.gameObject.SetActive(true);
        }
        //Si controlsImg es verdadero, comenzamos a restarle a showImg hasta que se vuelve cero y desactivamos las instrucciones 
        if (isNewGame)
        {
            controlsImage.gameObject.SetActive(true);
            showImg += Time.deltaTime;
            if(showImg >= 7)
            {
                controlsImage.gameObject.SetActive(false);
                showImg = 0;
                isNewGame = false;
            }
        }
        else
        {
            controlsImage.gameObject.SetActive(false);
        }
    }
    //Metodo para almacenar los datos que queremos guardar(aqui se deven agregar todos aquellos datos que queremos guardar
    public SaveSystem createSaveGameObject()
    {
        SaveSystem save = new SaveSystem();
        save.score = score;

        save.playerPositionX = spaceCraft.transform.position.x;
        save.playerPositionY = spaceCraft.transform.position.y;
        save.tempDistanceY = tempDistance.position.y;
        save.tempDistanceX = tempDistance.position.x;
        return save;
    }

    //Metodo para hacer el guardado de datos
    public void SaveBySerialization()
    {
        SaveSystem save = createSaveGameObject();//Creamos un guardado con todos los datos
        BinaryFormatter bf = new BinaryFormatter();
        //FileStream fileStream = File.Create(Application.persistentDataPath + "/Data.txt");
        FileStream fileStream = File.Create(Application.dataPath + "/Data.txt");
        bf.Serialize(fileStream, save);
        fileStream.Close();
    }

    //Metodo para hacer la carga de datos
    public void LoadBySerilization()
    {
        if (File.Exists(Application.persistentDataPath + "/Data.txt"))
        {
            Debug.Log("loadby...");

            BinaryFormatter bf = new BinaryFormatter();
            //FileStream fileStream = File.Open(Application.persistentDataPath + "/Data.txt", FileMode.Open);
            FileStream fileStream = File.Open(Application.dataPath + "/Data.txt", FileMode.Open);
            SaveSystem save = bf.Deserialize(fileStream) as SaveSystem;
            fileStream.Close();

            score = save.score;
            spaceCraft.transform.position = new Vector2(save.playerPositionX, save.playerPositionY);
            tempDistance.position = new Vector2(save.tempDistanceX, save.tempDistanceY);

        }
    }
}
