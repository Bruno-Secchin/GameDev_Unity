using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject[ ] lixeiras;
    private Vector3 escalaPadrao = new Vector3(0.45f, 0.45f, 0.45f); // Escala normal
    private Vector3 escalaAumentada = new Vector3(0.5f, 0.5f, 0.5f); // Escala aumentada
    private float tempoDeEspera = 0.3f; // ENTENDER O TEMPO DE ESPERA


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            StartCoroutine(AtivarEfeito(0)); // Índice 0 = Lixeira da tecla D
        }
        else if (Input.GetKeyDown(KeyCode.F))
        {
            StartCoroutine(AtivarEfeito(1)); // Índice 1 = Lixeira da tecla F
        }
        else if (Input.GetKeyDown(KeyCode.J))
        {
            StartCoroutine(AtivarEfeito(2)); // Índice 2 = Lixeira da tecla J
        }
        else if (Input.GetKeyDown(KeyCode.K))
        {
            StartCoroutine(AtivarEfeito(3)); // Índice 3 = Lixeira da tecla K
        }
    }

    IEnumerator AtivarEfeito(int indiceLixeira)
    {
        // Aumenta a escala e desativa o filho
        lixeiras[indiceLixeira].transform.localScale = escalaAumentada;
        lixeiras[indiceLixeira].transform.GetChild(0).gameObject.SetActive(false);

        // Espera 1 segundo
        yield return new WaitForSeconds(tempoDeEspera);

        // Volta à escala normal e reativa o filho
        lixeiras[indiceLixeira].transform.localScale = escalaPadrao;
        lixeiras[indiceLixeira].transform.GetChild(0).gameObject.SetActive(true);
    }
}