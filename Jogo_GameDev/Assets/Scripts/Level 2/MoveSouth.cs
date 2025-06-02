using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSouth : MonoBehaviour
{
    public float speed;
    private L2GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<L2GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.isGameActive == true)
        {
            transform.Translate(Vector3.back * Time.deltaTime * speed);  
        }
    }
}
