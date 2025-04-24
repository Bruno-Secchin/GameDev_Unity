using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectCollisions : MonoBehaviour
{
    private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Destroy ambos os objetos quando se chocam entre si:
    void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject);
        if (gameObject.CompareTag("Destroyer")){
            gameManager.UpdateScore(1);
        }
        else{
            gameManager.UpdateScore(0);
        }
    }
}
