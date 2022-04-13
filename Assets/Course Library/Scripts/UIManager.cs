using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;

public class UIManager : MonoBehaviour
{
    public List<TextMeshProUGUI> menuArray;
    public List<int> tempArray;
    public static UIManager Instance;
    [SerializeField] GameObject HighScoreScreen;
    [SerializeField] GameObject StartScreen;
    [SerializeField] TextMeshProUGUI waveNumDisplay;

    private void Awake() {
        if(Instance != null){
            Destroy(gameObject);
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        tempArray = new List<int>(5);
        Debug.Log("Initializing Temp Array");
        for(int i=0; i<5; i++){
            tempArray.Add(0);
        }
        Debug.Log("Initialized Temp Array");
        LoadScores();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void backToMenu(){
        HighScoreScreen.gameObject.SetActive(false);
        StartScreen.gameObject.SetActive(true);
        waveNumDisplay.gameObject.SetActive(true);
    }
    public void showHighScores(){
        HighScoreScreen.gameObject.SetActive(true);
        StartScreen.gameObject.SetActive(false);
        waveNumDisplay.gameObject.SetActive(false);
    }
    [System.Serializable]
    class SaveData{
        public List<int> gameScores = new List<int>(5);
    }
    public void LoadScores(){
        Debug.Log("Loading Scores");
        string path = Application.persistentDataPath + "/saveprofile.json";
        if(File.Exists(path)){
            Debug.Log("Path Exists" + path);
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            for(int i=0; i<5; i++){
                menuArray[i].text = (i+1) + ". " + data.gameScores[i];
                tempArray[i] = data.gameScores[i];
            }
            Debug.Log("Scores Loaded!");
        }
        else{
            Debug.Log("Filling Default Values");
            for(int i=0; i<5; i++){
                Debug.Log("Index: " + i + menuArray[i]);
                menuArray[i].text = (i+1) + ". " + 0;
            }
            Debug.Log("Empty Values Filled");
        }
        
    }
    public void SaveScore(int waveNumber){
        for(int i=0; i<5; i++){
            if(tempArray[i] < waveNumber){
                tempArray[i] = waveNumber;
                break;
            }
        }
        SaveData data = new SaveData();
        data.gameScores = tempArray;
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/saveprofile.json", json);
        Debug.Log("Score Saved!");
        
    }
}
