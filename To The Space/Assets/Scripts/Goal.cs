using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    public GameObject go;
    private float _time;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        go.gameObject.SetActive(true);

        _time += Time.deltaTime;
        if (_time >= 2)
        {

            SceneManager.LoadScene(0);
            _time = 0;
        }
    }
}
