using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatObstacles : MonoBehaviour
{
    private Vector3 startPos;
    private RepeatTerrain terrain;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        terrain = GameObject.Find("Terrain").GetComponent<RepeatTerrain>();
    }

    // Update is called once per frame
    void Update()
    {
        if (terrain.transform.position.z < terrain.startPos.z - terrain.repeatWidth){
            transform.position = startPos;
        }
    }
}
