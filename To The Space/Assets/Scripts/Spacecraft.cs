using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class Spacecraft : SpaceShips
{
    GameController gameController;
    

    void Start()
    {
        gameController = FindObjectOfType<GameController>();
        
        LoadBySerilization();
        currentGravity = gravity;
        maxFuel = fuel;
        fuselageMaxIntegrity = fuselageIntegrity;
        barFuselage.SetQuantity(fuselageIntegrity);
        barFuselage.SetMaxQuantity(fuselageMaxIntegrity);
        barFuel.SetMaxQuantity(maxFuel);
        barFuel.SetQuantity(fuel);
    }
    
    void Update()
    {
        //Movimiento del jugador y recarga de combustible
        #region
        if (canPressR && Input.GetKey(KeyCode.R))
        {
            IsRefueling();
        }

        
        transform.Translate(Vector3.down * currentGravity * Time.deltaTime);     //Con esta linea aplicamos gravedad
        if (Input.GetKey(KeyCode.W))                                             //Si se mantiene presionada W la nave se mueve hacia arriba y consume combustible
        {

            gameController.scoring = true;
            transform.Translate(Vector3.up * speed * Time.deltaTime);
            consumingFuel = true;
            isFlying = true;
            Fly();
            if (gameController.currentDistance.position.y > gameController.tempDistance.position.y)
            {

                gameController.tempDistance.position = gameController.currentDistance.position;
            }
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            consumingFuel = false;                                               //Si se Suelta W la nave deja de consumir combustible
            isFlying = false;
            
            Falling();
            Fly();
        }

        if (Input.GetKey(KeyCode.A))                                             //Si se mantiene presionada A la nave se mueve hacia la izquierda
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime); 
        }
        if (Input.GetKey(KeyCode.D))                                             //Si se mantiene presionada D la nave se mueve hacia la derecha
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
        #endregion

       
    }

    private SaveSystem createSaveGameObject()
    {
        SaveSystem save = new SaveSystem();
        save.score = GameController.gc.score;

        save.playerPositionX = transform.position.x;
        save.playerPositionY = transform.position.y;
        save.tempDistanceY = GameController.gc.tempDistance.position.y;
        save.tempDistanceX = GameController.gc.tempDistance.position.x;

        //foreach(Enemies enemy in GameController.gc.allEnemies)
        //{
        //    save.isDead.Add(enemy.isDead);
        //    save.enemyPositionX.Add(enemy.enemyPositionX);
        //    save.enemyPositionY.Add(enemy.enemyPositionY);
        //}
        return save;
    }

    private void SaveBySerialization()
    {
        Debug.Log("Guardando");
        SaveSystem save = createSaveGameObject();//Creamos un guardado con todos los datos
        BinaryFormatter bf = new BinaryFormatter();
        //FileStream fileStream = File.Create(Application.persistentDataPath + "/Data.txt");
        FileStream fileStream = File.Create(Application.dataPath + "/Data.txt");
        bf.Serialize(fileStream,save);
        fileStream.Close();
    }

    private void LoadBySerilization()
    {
        if(File.Exists(Application.persistentDataPath + "/Data.txt"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            //FileStream fileStream = File.Open(Application.persistentDataPath + "/Data.txt", FileMode.Open);
            FileStream fileStream = File.Open(Application.dataPath + "/Data.txt", FileMode.Open);
            SaveSystem save = bf.Deserialize(fileStream) as SaveSystem;
            fileStream.Close();

            GameController.gc.score = save.score;
            transform.position = new Vector2(save.playerPositionX, save.playerPositionY);
            GameController.gc.tempDistance.position = new Vector2 (save.tempDistanceX, save.tempDistanceY);
            //for (int i = 0; i < save.isDead.Count; i++)
            //{
            //    if(GameController.gc.allEnemies[i] == null)
            //    {
            //        if (!save.isDead[i])//Si el enemigo esta muerto pero vivo antes que alcnazemos un checkpoint
            //        {
            //            float enemyPosX = save.enemyPositionX[i];
            //            float enemyPosY = save.enemyPositionY[i];
            //            Enemies newEnemy = Instantiate(GameController.gc.enemiesGameObject, new Vector2(enemyPosX, enemyPosY), Quaternion.identity);
            //            GameController.gc.allEnemies[i] = newEnemy;
            //        }
            //    }
            //    else
            //    {
            //        float enemyPosX = save.enemyPositionX[i];
            //        float enemyPosY = save.enemyPositionY[i];
            //        GameController.gc.allEnemies[i].transform.position = new Vector2(enemyPosX,enemyPosY);
            //    }
            //}

        }
    }

    protected void Falling()
    {
        gameController.scoring = false;
        if (gameController.tempDistance.position.y >= gameController.initialDistance.position.y)
        {
            gameController.initialDistance.position = gameController.tempDistance.position;
        }
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Checkpoint")                                       //Si colicionamos con un checkpoint
        {
            
            //texto que indica que se ha alcanzado un nuevo checkpoint
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        
    }

    protected void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Platform")                              //Si colicionamos con la plataforma
        {
            SaveBySerialization();
            currentGravity = tempGravity;
            canRefuel = true;
            isFlying = false;
            canPressR = true;
            consumingFuel = false;
        }

        if (collision.gameObject.tag == "Base")                              //Si colicionamos con la plataforma
        {
            Debug.Log("Estoy en la base");
            SaveBySerialization();
            currentGravity = tempGravity;
            currentGravity = tempGravity;
            canRefuel = true;
            isFlying = false;
            canPressR = true;
            consumingFuel = false;
        }
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.SetActive(false);
            IsDead();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Platform")
        {
            canRefuel = false;
            currentGravity = gravity;

            isFlying = true;
            Fly();                                                               //Llamamos al metodo
        }
        if (collision.gameObject.tag == "Base")
        {
            currentGravity = gravity;
        }
    }
}
