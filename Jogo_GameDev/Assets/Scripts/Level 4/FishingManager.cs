using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FishingManager : MonoBehaviour
{
    public static FishingManager Instance;
    
    [Header("Fishing Settings")]
    public int piecesToFish = 3;
    public float fishingZoneRadius = 5f;
    public Transform fishingSpot;
    public GameObject bobberPrefab;
    public AudioClip castSound;
    public AudioClip biteSound;
    public ParticleSystem biteParticle;
    
    private int piecesFished = 0;
    private bool isFishing = false;
    private GameObject currentBobber;
    private bool canReel = false;
    
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }
    
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!isFishing)
            {
                TryCastFishingRod();
            }
            else if (canReel)
            {
                StartFishingMinigame();
            }
        }
    }
    
    private void TryCastFishingRod()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit))
        {
            if (Vector3.Distance(fishingSpot.position, hit.point) <= fishingZoneRadius)
            {
                StartFishing(hit.point);
            }
        }
    }
    
    private void StartFishing(Vector3 position)
    {
        isFishing = true;
        currentBobber = Instantiate(bobberPrefab, position, Quaternion.identity);
        AudioSource.PlayClipAtPoint(castSound, position);
        
        // Começa a esperar pela fisgada
        StartCoroutine(WaitForBite());
    }
    
    private IEnumerator WaitForBite()
    {
        float waitTime = Random.Range(2f, 5f);
        yield return new WaitForSeconds(waitTime);
        
        // Peixe mordeu
        canReel = true;
        AudioSource.PlayClipAtPoint(biteSound, currentBobber.transform.position);
        biteParticle.transform.position = currentBobber.transform.position;
        biteParticle.Play();
    }
    
    private void StartFishingMinigame()
    {
        canReel = false;
        biteParticle.Stop();
        FishingMinigame.Instance.StartMinigame();
    }
    
    public void OnFishingSuccess()
    {
        piecesFished++;
        Destroy(currentBobber);
        isFishing = false;
        
        if (piecesFished >= piecesToFish)
        {
            // Fase completa
            Debug.Log("Fase de pesca concluída!");
        }
    }
    
    public void OnFishingFailure()
    {
        Destroy(currentBobber);
        isFishing = false;
        canReel = false;
        // Pode adicionar feedback de falha aqui
    }
}