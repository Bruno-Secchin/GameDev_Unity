using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L5PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    private Animator playerAnim;
    private AudioSource playerAudio;
    private L5GameManager gameManager;
    public AudioClip deathSound;
    public float speed;
    private float xRange = 20.9f;
    private float zRange = 8.9f;
    public FollowPlayer enemy;
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        gameManager = GameObject.Find("GameManager").GetComponent<L5GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.z < -zRange)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -zRange);
        }
        if (transform.position.z > zRange)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, zRange);
        }
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
            if (enemy.playerCatched)
            {
                playerAnim.SetBool("Death_b", true);
                StartCoroutine(gameManager.GameOver());
                playerAudio.PlayOneShot(deathSound, 1.0f);
            }
        }
    }

    public void StopRun()
    {
        playerAnim.SetBool("Run_b", false);
    }
}
