using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    public float speed = 5.0f;
    private GameObject focalPoint;
    public bool gameOver = false;
    private float threshHold = 30;
    public bool hasPowerup;
    private float powerupStrength = 15.0f;
    public GameObject powerupIndicator;
    private Rigidbody powerupRb;
    private bool powerupSpawner;
    public GameObject PowerUp;
    public GameObject GameOverScreen;
    private SpawnManager spawnManager;
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
        gameOver = false;
        powerupIndicator.SetActive(false);
        //powerupRb = powerupIndicator.GetComponent<Rigidbody>();
        StartCoroutine(PowerupSpawnTimer());
        GameOverScreen.gameObject.SetActive(false);
        //spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
        float forwardInput = Input.GetAxis("Vertical");
        playerRb.AddForce(focalPoint.transform.forward * speed * forwardInput);

        if(transform.position.y < -threshHold){
            Debug.Log("Game Over!");
            GameOverScreen.gameObject.SetActive(true);
            gameOver = true;
            playerRb.freezeRotation = true;
            playerRb.isKinematic = true;
            Vector3 adjPos = new Vector3(transform.position.x, -threshHold+2, transform.position.z);
            transform.position = adjPos;
        }

        if(hasPowerup==true){
            powerupIndicator.transform.position = transform.position + new Vector3(0, -0.5f, 0);
            powerupIndicator.transform.Rotate(Vector3.up, 20 * Time.deltaTime);
        }

        if(powerupSpawner == true){
            float randomX = Random.Range(-12, 12);
            float randomZ = Random.Range(-12, 12);
            Vector3 spawnRange = new Vector3(randomX, 0, randomZ);
            Instantiate(PowerUp, spawnRange, PowerUp.transform.rotation);
            powerupSpawner=false;
            
        }
        
    }
    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Powerup")){
            Debug.Log("Picked up a Powerup");
            Destroy(other.gameObject);
            hasPowerup = true;
            powerupIndicator.SetActive(true);
            StartCoroutine(PowerupCountdownRoutine());
        }
    }
    IEnumerator PowerupCountdownRoutine(){
        yield return new WaitForSeconds(7);
        hasPowerup = false;
        powerupIndicator.SetActive(false);
        StartCoroutine(PowerupSpawnTimer());
    }

    IEnumerator PowerupSpawnTimer(){
        yield return new WaitForSeconds(7);
        powerupSpawner = true;
    }

    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.CompareTag("Enemy") && hasPowerup){
            Debug.Log("Collided with " + other.gameObject.name + " with powerup set to " + hasPowerup);
            Rigidbody enemyRigidBody = other.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = (other.gameObject.transform.position - transform.position);

            enemyRigidBody.AddForce(awayFromPlayer * powerupStrength, ForceMode.Impulse);
        }
    }
}
