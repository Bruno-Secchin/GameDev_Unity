using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : MonoBehaviour
{
    private Rigidbody trashRb;
    private float maxTorque = 8;
    private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        trashRb = GetComponent<Rigidbody>();
        trashRb.AddTorque(0, RandomTorque(), 0, ForceMode.Impulse);
        gameManager = GameObject.Find("Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    float RandomTorque()
    {
        return Random.Range(-maxTorque, maxTorque);
    }
}
