using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public static List<CharacterRecorder> characterRecords = new List<CharacterRecorder>(); 
    public Transform Stage1;
    public string SpawnPoint;
    
    public static int respawnCount = 0;
    public UnitType spawnType;

    public static int points = 0;
    void Awake()
    {
        //Check if instance already exists
        if (instance == null)
            
            //if not, set instance to this
            instance = this;
        
        //If instance already exists and it's not this:
        else if (instance != this)
            
            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);    
        
        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);
        
        if (respawnCount == 0)
        {
            //BEGIN + INSTRUCTIONS
            
        }
        else
        {
            Time.timeScale = 0;
            GameObject.FindObjectOfType<UIManager>().DisplaySelectionScreen();
        }
        
        
    }
    
    public void RespawnAll() {
        
       for (int i = 0; i < characterRecords.Count; i++) {
           
         //  if (characterRecords[i].unitType == UnitType.Stage1)
          // {
               Transform t = (Transform)Transform.Instantiate(Stage1,GameObject.Find(SpawnPoint).transform.position,GameObject.Find(SpawnPoint).transform.rotation);
               CharacterRecorder r = t.GetComponent<CharacterRecorder>();
               //r.strokeDict = characterRecords[i].strokeDict;
                  r.strokes = characterRecords[i].strokes;
                r.unitType = characterRecords[i].unitType;
               t.GetComponent<CharacterMovement>().isMainCharacter = false;
               r.PlayReset();
           //}

       }
    }

    public static void ResetScene() {
        respawnCount += 1;
        CharacterRecorder[] rec = FindObjectsOfType<CharacterRecorder>();
        characterRecords.Clear();
        for (int i = 0; i < rec.Length; i++) {
            characterRecords.Add(rec[i]);
        }
        Application.LoadLevel(0);
    }
        
}
