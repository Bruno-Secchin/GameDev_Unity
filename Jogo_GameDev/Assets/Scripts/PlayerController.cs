using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject[ ] lixeiras;
    private Vector3 escalaPadrao = new Vector3(0.45f, 0.45f, 0.45f); // Escala normal
    private Vector3 escalaAumentada = new Vector3(0.5f, 0.5f, 0.5f); // Escala aumentada
    private float tempoDeEspera = 0.5f;
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Manager").GetComponent<GameManager>();
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

        // Espera o tempo definido
        yield return new WaitForSeconds(tempoDeEspera);

        // Volta à escala normal e reativa o filho
        lixeiras[indiceLixeira].transform.localScale = escalaPadrao;
        lixeiras[indiceLixeira].transform.GetChild(0).gameObject.SetActive(true);
        if (gameManager.GetStreak() % 10 == 0 && gameManager.GetStreak() != 0)
        {
            StartCoroutine(Frenesi()); // Skill que deixa as lixeiras abertas por um tempo após um streak
        }
    }

    IEnumerator Frenesi()
    {
        // Espera as lixeiras retornarem a escala padrao
        //yield return new WaitForSeconds(tempoDeEspera);

        // Aumenta a escala e desativa o filho
        lixeiras[0].transform.localScale = escalaAumentada;
        lixeiras[0].transform.GetChild(0).gameObject.SetActive(false);
        lixeiras[1].transform.localScale = escalaAumentada;
        lixeiras[1].transform.GetChild(0).gameObject.SetActive(false);
        lixeiras[2].transform.localScale = escalaAumentada;
        lixeiras[2].transform.GetChild(0).gameObject.SetActive(false);
        lixeiras[3].transform.localScale = escalaAumentada;
        lixeiras[3].transform.GetChild(0).gameObject.SetActive(false);

        // Espera o tempo definido
        yield return new WaitForSeconds(6.0f);

        // Volta à escala normal e reativa o filho
        lixeiras[0].transform.localScale = escalaPadrao;
        lixeiras[0].transform.GetChild(0).gameObject.SetActive(true);
        lixeiras[1].transform.localScale = escalaPadrao;
        lixeiras[1].transform.GetChild(0).gameObject.SetActive(true);
        lixeiras[2].transform.localScale = escalaPadrao;
        lixeiras[2].transform.GetChild(0).gameObject.SetActive(true);
        lixeiras[3].transform.localScale = escalaPadrao;
        lixeiras[3].transform.GetChild(0).gameObject.SetActive(true);
    }
}