using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L2PlayerController : MonoBehaviour
{
    private L2GameManager gameManager;
    private Rigidbody playerRb;
    private Animator playerAnim;
    private AudioSource playerAudio;
    public TimerController timerController;
    public AudioClip jumpSound;
    public AudioClip crouchSound;
    public AudioClip deathSound;
    public float jumpForce;
    public float gravityModifier;
    public bool isOnGround = true;
    public float speed;
    private float xRange = 5.0f;
    private float horizontalInput;
    private BoxCollider boxCollider;
    private Vector3 originalSize;
    private Vector3 originalCenter;
    private bool isCrouching = false;
    private Vector3 crouchedSize = new Vector3(0.8f, 1.4f, 0.5f);
    private Vector3 crouchedCenter = new Vector3(0f, 0.7f, 0f);
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<L2GameManager>();
        playerRb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        Physics.gravity = new Vector3(0, -9.81f * gravityModifier, 0);
        playerAudio = GetComponent<AudioSource>();
        boxCollider = GetComponent<BoxCollider>();
        if (boxCollider != null)
        {
            originalSize = boxCollider.size;
            originalCenter = boxCollider.center;
        }
        else
        {
            Debug.Log("BoxCollider não encontrado!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < -xRange)
        {
            transform.position = new Vector3(-xRange, transform.position.y, transform.position.z);
        }
        if (transform.position.x > xRange)
        {
            transform.position = new Vector3(xRange, transform.position.y, transform.position.z);
        }
        if (gameManager.isGameActive)
        {
            playerAnim.SetBool("Run_b", true);
            horizontalInput = Input.GetAxis("Horizontal");
            transform.Translate(Vector3.right * horizontalInput * Time.deltaTime * speed);
        }

        if (!timerController.isCountingDown)
        {
            playerAnim.SetBool("Run_b", false);
        }

        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S) && isOnGround && gameManager.isGameActive && !isCrouching)
            {
                playerAnim.SetTrigger("Crouch_trig");
                playerAudio.PlayOneShot(crouchSound, 1.0f);
            }
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W) && isOnGround && gameManager.isGameActive && !isCrouching)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
            playerAnim.SetTrigger("Jump_trig");
            playerAudio.PlayOneShot(jumpSound, 1.0f);
        }

    }
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Ground")) {
            isOnGround = true;
        }
        else if (other.gameObject.CompareTag("Obstacle")) {
            playerAnim.SetBool("Death_b", true);
            StartCoroutine(gameManager.GameOver());
            playerAudio.PlayOneShot(deathSound, 1.0f);
        }
    }

    public void Crouch()
    {
        isCrouching = true;
        boxCollider.size = crouchedSize;
        boxCollider.center = crouchedCenter;
    }
    public void EndCrouch()
    {
        isCrouching = false;
        boxCollider.size = originalSize;
        boxCollider.center = originalCenter;
    }
}
