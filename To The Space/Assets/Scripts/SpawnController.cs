using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    private float _time = 2;
    public GameObject asteroid;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Spawning();
    }
    void Spawning()
    {
        _time += Time.deltaTime;
        if (_time >= 2)
        {
            Instantiate(asteroid,transform.position,Quaternion.identity);
            _time = 0;
        }
    }
}
