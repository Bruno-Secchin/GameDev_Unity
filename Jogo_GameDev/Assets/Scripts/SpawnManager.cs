using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[ ] lixoPrefabs;
    Vector3 spawnpos;
    private float startDelay = 2.0f;
    private float spawnInterval = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        // Define um intervalo para o Spawn de lixo:
        InvokeRepeating("SpawnRandom", startDelay, spawnInterval);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnRandom()
    {
        // Define uma posicao aleatoria de Spawn e instancia o Spawn:
        int lixoIndex = Random.Range(0, lixoPrefabs.Length);
        switch(lixoIndex){
            case 0:
                spawnpos = new Vector3((float)-0.048, 2, (float)-8.75);
                Instantiate(lixoPrefabs[lixoIndex], spawnpos, lixoPrefabs[lixoIndex].transform.rotation);
                break;
            case 1:
                spawnpos = new Vector3((float)0.3, 2, (float)-8.75);
                Instantiate(lixoPrefabs[lixoIndex], spawnpos, lixoPrefabs[lixoIndex].transform.rotation);
                break;
            case 2:
                spawnpos = new Vector3((float)0.597, 2, (float)-8.75);
                Instantiate(lixoPrefabs[lixoIndex], spawnpos, lixoPrefabs[lixoIndex].transform.rotation);
                break;
            case 3:
                spawnpos = new Vector3((float)1, 2, (float)-8.75);
                Instantiate(lixoPrefabs[lixoIndex], spawnpos, lixoPrefabs[lixoIndex].transform.rotation);
                break;

        }
    }
}
