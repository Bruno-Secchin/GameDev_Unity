using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L2PlayerController : MonoBehaviour
{
    private L2GameManager gameManager;
    private Rigidbody playerRb;
    private Animator playerAnim;
    private AudioSource playerAudio;
    public AudioClip jumpSound;
    public AudioClip crounchSound;
    public AudioClip crashSound;
    public float jumpForce;
    public float gravityModifier;
    public bool isOnGround = true;
    public bool gameOver = false;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<L2GameManager>();
        playerRb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        Physics.gravity *= gravityModifier;
        playerAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.isGameActive){
            playerAnim.SetBool("Run_b", true);
        }
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround && gameManager.isGameActive){
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
            playerAnim.SetTrigger("Jump_trig");
            playerAudio.PlayOneShot(jumpSound, 1.0f);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) && isOnGround && gameManager.isGameActive){
            playerAnim.SetTrigger("Crounch_trig");
            playerAudio.PlayOneShot(crounchSound, 1.0f);
        }

    }
    private void OnCollisionEnter(Collision collision){
        if ( collision.gameObject.CompareTag("Ground")){
            isOnGround = true;
        }
        else if (collision.gameObject.CompareTag("Obstacle")){
            playerAnim.SetBool("Death_b", true);
            gameManager.GameOver();
            Debug.Log("Game Over!");
            // playerAnim.SetInteger("DeathType_int", 1);
            playerAudio.PlayOneShot(crashSound, 1.0f);
        }
    }
}
