using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    //private float spawnDelay = 1;
    //private float spawnFrequency = 3;
    private float randX;
    private float randZ;
    private float spawnRange = 9;
    private PlayerController playerControllerScript;
    public int enemyCount;
    private int waveNum = 1;
    public TextMeshProUGUI waveNumText;
    public bool gameStarted = false;
    [SerializeField] GameObject StartScreen;
    
    // Start is called before the first frame update
    public void StartGame()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        //InvokeRepeating("spawnEnemy", spawnDelay, spawnFrequency);
        
        spawnEnemyWave(1);
        Debug.Log("Wave " + waveNum);
        gameStarted=true;
        StartScreen.gameObject.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        if(gameStarted==true){
            if(playerControllerScript.gameOver==false){
                enemyCount = FindObjectsOfType<Enemy>().Length;
                if(enemyCount == 0){
                    waveNum++;
                    spawnEnemyWave(waveNum);
                    //Debug.Log("Wave " + waveNum);
                    waveNumText.SetText("Wave " + waveNum);

                }
            }
        }
    }
    private Vector3 generateSpawnPoint(){
        randX = Random.Range(-spawnRange, spawnRange);
        randZ = Random.Range(-spawnRange, spawnRange);
        Vector3 spawnPoint = new Vector3(randX, 0, randZ);
        return spawnPoint;
    }
    void spawnEnemyWave(int enemiesToSpawn){
        if(playerControllerScript.gameOver !=true){
            for(int i=0; i < enemiesToSpawn; i++){
                Instantiate(enemyPrefab, generateSpawnPoint(), enemyPrefab.transform.rotation);
            } 
        }
    }  
    public void RestartGame(){
        UIManager.Instance.SaveScore(waveNum);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    } 
    private void saveGameScore(){

    }

}
