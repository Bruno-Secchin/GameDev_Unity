using UnityEngine;

public class EnemyCollisionHandler : MonoBehaviour
{
    [Header("Referências")]
    public GameObject playerObject;  // Referência direta ao jogador
    public GameObject objectToSpawn;  // Objeto que substituirá o inimigo

    [Header("Configurações")]
    public bool destroyOnCollision = true;
    public float spawnOffset = 0.5f;

    [Header("Efeitos")]
    public ParticleSystem collisionEffect;
    public AudioClip collisionSound;

    private void OnCollisionEnter(Collision collision)
    {
        // Verifica se colidiu com o objeto do jogador referenciado
        if (collision.gameObject == playerObject)
        {
            HandleEnemyTransformation();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Versão alternativa para triggers
        if (other.gameObject == playerObject)
        {
            HandleEnemyTransformation();
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

    // Método para atribuir o player manualmente (opcional)
    public void SetPlayer(GameObject player)
    {
        playerObject = player;
    }
}