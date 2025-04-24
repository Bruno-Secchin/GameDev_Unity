using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutOfBounds : MonoBehaviour
{
    private float topBound = -6.0f;
    private float lowerBound = 23.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Destroi o objeto caso ultrapasse a margem superior da câmera:
        if (transform.position.z < topBound){
            Destroy(gameObject);
        }
        // Destroi o objeto caso ultrapasse a margem inferior da câmera:
        else if (transform.position.z > lowerBound){
            Destroy(gameObject);
            Debug.Log("Game Over!");
        }
    }
}
