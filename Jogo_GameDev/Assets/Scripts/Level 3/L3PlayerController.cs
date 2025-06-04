using System.Collections;
using UnityEngine;

public class L3PlayerController : MonoBehaviour
{
    [Header("Components")]
    private Rigidbody playerRb;
    private Animator playerAnim;
    private AudioSource playerAudio;

    [Header("Audio Clips")]
    public AudioClip jumpSound;
    public AudioClip deathSound;

    [Header("Movement Settings")]
    public float jumpForce = 10f;
    public float gravityModifier = 1.5f;
    public float speed = 5f;
    public float xRange = 5.0f;
    public float zRangeForward = 10.0f;
    public float zRangeBackward = -5.0f;
    public bool isMoving = false;

    [Header("Status")]
    public bool isOnGround = true;

    private void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();

        Physics.gravity = new Vector3(0, -9.81f * gravityModifier, 0);
    }

    private void Update()
    {
        if (!L3GameManager.Instance.isGameActive) return;


        HandleMovement();
        HandleJump();
        ConstrainPosition();
    }

    private void HandleMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontalInput, 0, verticalInput).normalized;
        transform.Translate(movement * speed * Time.deltaTime, Space.World);

        isMoving = movement.magnitude > 0.1f;

        playerAnim.SetBool("Run_b", isMoving);

        if (movement.magnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movement);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 10f * Time.deltaTime);
        }
    }

    private void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
            playerAnim.SetTrigger("Jump_trig");
            playerAudio.PlayOneShot(jumpSound, 1.0f);
        }
    }

    private void ConstrainPosition()
    {
        float clampedX = Mathf.Clamp(transform.position.x, -xRange, xRange);
        float clampedZ = Mathf.Clamp(transform.position.z, zRangeBackward, zRangeForward);
        transform.position = new Vector3(clampedX, transform.position.y, clampedZ);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            isOnGround = true;
        }
        else if (other.CompareTag("Obstacle"))
        {
            HandleObstacleCollision();
        }
    }

    private void HandleObstacleCollision()
    {
        playerAnim.SetBool("Death_b", true);
        playerAnim.SetInteger("DeathType_int", 1);
        playerAudio.PlayOneShot(deathSound, 1.0f);
        StartCoroutine(L3GameManager.Instance.GameOver());
    }
}