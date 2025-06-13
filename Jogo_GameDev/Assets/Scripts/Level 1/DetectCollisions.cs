using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectCollisions : MonoBehaviour
{
    private GameManager gameManager;
    public ParticleSystem badParticle;
    public ParticleSystem dirtParticle;
    private AudioSource playerAudio;
    public AudioClip badSound;
    public AudioClip goodSound;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Manager").GetComponent<GameManager>();
        playerAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Destroy ambos os objetos quando se chocam entre si:
    void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject);
        if (gameObject.CompareTag("Destroyer")){
            gameManager.UpdateScore(1);
            StartCoroutine(OpenPipe());
            playerAudio.PlayOneShot(goodSound, 1.0f);
        }
        else{
            Instantiate(badParticle, transform.position, badParticle.transform.rotation);
            gameManager.UpdateScore(0);
            playerAudio.PlayOneShot(badSound, 1.0f);
        }
    }

    IEnumerator OpenPipe()
    {
        dirtParticle.transform.gameObject.SetActive(true);

        yield return new WaitForSeconds(1.0f);

        dirtParticle.transform.gameObject.SetActive(false);
    }
}
