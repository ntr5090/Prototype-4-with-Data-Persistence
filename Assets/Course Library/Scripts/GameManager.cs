using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public SpawnManager spawnManager;
    [SerializeField] GameObject StartScreen;
    
    // Start is called before the first frame update
    void Start()
    {
        StartScreen.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void startingGame(){
        spawnManager.StartGame();
    }
    
}
