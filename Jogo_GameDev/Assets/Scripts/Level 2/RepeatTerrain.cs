using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatTerrain : MonoBehaviour
{
    public Vector3 startPos;
    public float repeatWidth;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        repeatWidth = GetComponent<BoxCollider>().size.z / 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.z < startPos.z - repeatWidth){
            transform.position = startPos;
        }
    }
}
