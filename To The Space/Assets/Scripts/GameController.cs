using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class GameController : MonoBehaviour
{


    public static GameController gc;
    public GameObject spaceCraft;

    public static bool isNewGame = true;
    public Transform initialDistance;//Transform para guardar la posicion del jugador, nos servira para hacer una comparacion mas a delante y asi saber si se suman puntos o no
    public Transform currentDistance;//Transform para ir guardando la distancia y comparar 
    public Transform tempDistance;
    public float scoreTime;//Flotante para controlar cada que tiempo se le sumara un punto al jugador
    public float score = 0;
    public string scoreString = "Score:" + "Mts.";
    public List<Enemies> allEnemies = new List<Enemies>();
    public Enemies enemiesGameObject;
    public bool gameOver = false;

    public Text txtScore;
    public bool scoring = false;

    private void Awake()
    {
        gc = this;
    }
    void Start()
    {
        SaveSystem saveSystem = new SaveSystem();
        
        if (isNewGame)
        {
            saveSystem.score = 0;
            saveSystem.playerPositionX = 0;
            saveSystem.playerPositionY = -3.219f;
            saveSystem.tempDistanceY = -3.219f;
            saveSystem.tempDistanceX = 0;
        }
        else
        {
            LoadBySerilization();
        }
        txtScore.text = scoreString + score.ToString("F1") + "Mts.";
        initialDistance.position = currentDistance.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (scoring && initialDistance.transform.position.y < currentDistance.transform.position.y)
        {
            score += Time.deltaTime;
            txtScore.text = scoreString + score.ToString("F1") + "Mts.";
        }
        if (gameOver)
        {

        }
    }
    public SaveSystem createSaveGameObject()
    {
        SaveSystem save = new SaveSystem();
        save.score = GameController.gc.score;

        save.playerPositionX = spaceCraft.transform.position.x;
        save.playerPositionY = spaceCraft.transform.position.y;
        save.tempDistanceY = GameController.gc.tempDistance.position.y;
        save.tempDistanceX = GameController.gc.tempDistance.position.x;
        return save;
    }

    public void SaveBySerialization()
    {
        Debug.Log("Guardando");
        SaveSystem save = createSaveGameObject();//Creamos un guardado con todos los datos
        BinaryFormatter bf = new BinaryFormatter();
        //FileStream fileStream = File.Create(Application.persistentDataPath + "/Data.txt");
        FileStream fileStream = File.Create(Application.dataPath + "/Data.txt");
        bf.Serialize(fileStream, save);
        fileStream.Close();
    }

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

            GameController.gc.score = save.score;
            spaceCraft.transform.position = new Vector2(save.playerPositionX, save.playerPositionY);
            GameController.gc.tempDistance.position = new Vector2(save.tempDistanceX, save.tempDistanceY);

        }
    }
}
