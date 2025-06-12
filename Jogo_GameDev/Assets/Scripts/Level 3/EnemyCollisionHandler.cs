using UnityEngine;

public class EnemyCollisionHandler : MonoBehaviour
{
    [Header("Referências")]
    public GameObject playerObject;  // Referência direta ao jogador
    public GameObject objectToSpawn;  // Objeto que substituirá o inimigo

    [Header("Configurações")]
    public bool destroyOnCollision = true;
    public float spawnOffset = 0.5f;
    public int scoreValue = 100; // Pontos concedidos ao derrotar este inimigo

    [Header("Efeitos")]
    public ParticleSystem collisionEffect;
    public AudioClip collisionSound;
    

    private void OnCollisionEnter(Collision collision)
    {
        // Verifica se colidiu com o objeto do jogador referenciado
        if (collision.gameObject == playerObject)
        {
            HandleEnemyTransformation();
            AddScoreToPlayer();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Versão alternativa para triggers
        if (other.gameObject == playerObject)
        {
            HandleEnemyTransformation();
            AddScoreToPlayer();
        }
    }

    private void HandleEnemyTransformation()
    {
        if (objectToSpawn == null)
        {
            Debug.LogWarning("Nenhum objeto definido para spawnar!");
            return;
        }

        Vector3 spawnPosition = transform.position + Vector3.up * spawnOffset;
        Instantiate(objectToSpawn, spawnPosition, transform.rotation);
        PlayEffects();

        if (destroyOnCollision)
        {
            Destroy(gameObject);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    private void PlayEffects()
    {
        if (collisionEffect != null)
        {
            Instantiate(collisionEffect, transform.position, Quaternion.identity);
        }

        AudioSource audioSource = GetComponent<AudioSource>();
        if (audioSource != null && collisionSound != null)
        {
            audioSource.PlayOneShot(collisionSound);
        }
    }

    private void AddScoreToPlayer()
    {
        if (L3GameManager.Instance != null)
        {
            L3GameManager.Instance.AddScore(scoreValue);
        }
        else
        {
            Debug.LogWarning("GameManager não encontrado para adicionar pontuação!");
        }
    }

    // Método para atribuir o player manualmente (opcional)
    public void SetPlayer(GameObject player)
    {
        playerObject = player;
    }
}