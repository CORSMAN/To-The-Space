using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    public static GameController gc;

    public Transform initialDistance;//Transform para guardar la posicion del jugador, nos servira para hacer una comparacion mas a delante y asi saber si se suman puntos o no
    public Transform currentDistance;//Transform para ir guardando la distancia y comparar 
    public Transform tempDistance;
    public float scoreTime;//Flotante para controlar cada que tiempo se le sumara un punto al jugador
    public float score = 0;
    public string scoreString = "Score:" + "Mts.";
    public List<Enemies> allEnemies = new List<Enemies>();
    public Enemies enemiesGameObject;

    public Text txtScore;
    public bool scoring = false;

    private void Awake()
    {
        gc = this;
    }
    void Start()
    {
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
    }
}
