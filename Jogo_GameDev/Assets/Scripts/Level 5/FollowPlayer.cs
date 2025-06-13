using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform playerTransform; // Arraste o Player aqui no Inspector
    public float moveSpeed = 2.1f;
    public float rotationSpeed = 5f;
    private L5GameManager gameManager;
    public bool playerCatched = false;
    public bool inLight = false;
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<L5GameManager>();
    }

    void Update()
    {
        if (playerTransform == null) return;
        if (gameManager.isGameActive && !inLight)
        {
            // Direção em que o inimigo deve olhar e andar
            Vector3 direction = (playerTransform.position - transform.position).normalized;

            // Rotação suave para olhar na direção do player
            if (direction != Vector3.zero)
            {
                Quaternion lookRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);
            }

            // Movimento em direção ao player
            transform.position += transform.forward * moveSpeed * Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerCatched = true;
        }
        if (other.CompareTag("Light"))
        {
            inLight = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Light"))
        {
            inLight = false;
        }
    }
}
