using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyFleeAI : MonoBehaviour 
{
    [Header("Referências")]
    [Tooltip("Arraste o objeto do jogador aqui")]
    public Transform player;  // Referência pública ao jogador

    [Header("Configurações de Fuga")]
    public float safeDistance = 10f;
    public float fleeSpeed = 5f;
    public float normalSpeed = 3.5f;
    public float detectionRange = 15f;

    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = normalSpeed;
        
        // Verificação de segurança
        if (player == null)
        {
            Debug.LogError("Referência ao jogador não atribuída!", this);
            enabled = false; // Desativa o script se não houver jogador
        }
    }

    void Update()
    {
        if (ShouldFlee())
        {
            FleeFromPlayer();
        }
        else
        {
            ReturnToNormal();
        }
    }

    bool ShouldFlee()
    {
        float distance = Vector3.Distance(transform.position, player.position);
        return distance < detectionRange;
    }

    void FleeFromPlayer()
    {
        Vector3 fleeDirection = (transform.position - player.position).normalized;
        Vector3 fleeTarget = transform.position + fleeDirection * safeDistance;
        
        // Garante que o destino está no NavMesh
        NavMeshHit hit;
        if (NavMesh.SamplePosition(fleeTarget, out hit, safeDistance, NavMesh.AllAreas))
        {
            agent.speed = fleeSpeed;
            agent.SetDestination(hit.position);
        }
    }

    void ReturnToNormal()
    {
        agent.speed = normalSpeed;
        // Adicione lógica de patrulha aqui se necessário
    }

    // Visualização no editor
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
        
        if (player != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, player.position);
        }
    }
}