using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    [Header("Referências")]
    public Transform player; // Referência ao transform do jogador

    [Header("Configurações de Posição")]
    public Vector3 offsetPosition = new Vector3(0f, 6.89f, -5f); // Offset em relação ao jogador

    [Header("Configurações de Rotação")]
    public Vector3 cameraRotation = new Vector3(32f, 0f, 0f); // Rotação fixa da câmera

    [Header("Suavização")]
    public float smoothSpeed = 5f; // Velocidade de suavização do movimento

    private void Start()
    {
        // Configura a rotação inicial da câmera
        transform.rotation = Quaternion.Euler(cameraRotation);
        
        // Verifica se o jogador foi atribuído
        if (player == null)
        {
            Debug.LogError("Nenhum jogador atribuído à câmera!");
        }
    }

    private void LateUpdate()
    {
        if (player != null)
        {
            // Calcula a posição desejada da câmera
            Vector3 desiredPosition = player.position + offsetPosition;
            
            // Suaviza o movimento da câmera
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
            transform.position = smoothedPosition;
        }
    }

    // Método para configurar o jogador manualmente (útil se instanciar o jogador em runtime)
    public void SetPlayer(Transform playerTransform)
    {
        player = playerTransform;
    }
}