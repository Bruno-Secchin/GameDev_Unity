using System.Collections;
using UnityEngine;

public class L3PlayerController : MonoBehaviour
{
    [Header("Components")]
    private Rigidbody playerRb;
    private Animator playerAnim;
    private AudioSource playerAudio;

    [Header("Audio Clips")]
    public AudioClip deathSound;

    [Header("Movement Settings")]
    public float gravityModifier = 1.5f;
    public float speed = 5f;

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

        if (transform.position.z < -15.0f)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -15.0f);
        }
        if (transform.position.z > 32.5f)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, 32.5f);
        }
        if (transform.position.x < -11.0f)
        {
            transform.position = new Vector3(-11.0f, transform.position.y, transform.position.z);
        }
        if (transform.position.x > 34.0f)
        {
            transform.position = new Vector3(34.0f, transform.position.y, transform.position.z);
        }

        HandleMovement();
    }

    private void HandleMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontalInput, 0, verticalInput);
        bool isMoving = movement.magnitude > 0.1f;

        playerAnim.SetBool("Run_b", isMoving);

        if (isMoving)
        {
            movement.Normalize();
            transform.Translate(movement * speed * Time.deltaTime, Space.World);

            Quaternion targetRotation = Quaternion.LookRotation(movement);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 10f * Time.deltaTime);
        }
    }
    public void Death()
    {
        playerAudio.PlayOneShot(deathSound, 1.0f);
        playerAnim.SetBool("Death_b", true);
    }
    public void Stop()
    {
        playerAnim.SetBool("Run_b", false);
    }
}