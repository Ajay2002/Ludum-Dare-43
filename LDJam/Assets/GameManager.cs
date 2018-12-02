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

    public int enemyCount = 0;

    public bool gameOver = false;

    public static int points = 0;
    void Awake()
    {
        gameOver = false;
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
            print("BEGIN & INSTRUCTIONS SEGMENT");
            
        }
        else
        {
            Time.timeScale = 0;
            GameObject.FindObjectOfType<UIManager>().DisplaySelectionScreen();
        }

        GameObject.FindObjectOfType<UIManager>().Tries.text = respawnCount + " / 70 max attempts";

        EnemyAI[] ais = GameObject.FindObjectsOfType<EnemyAI>();
        enemyCount = ais.Length;
        
    }

    public void ReCount()
    {
        EnemyAI[] ais = GameObject.FindObjectsOfType<EnemyAI>();
        enemyCount = ais.Length;
    }

    public void OneDied()
    {
        enemyCount -= 1;
        if (enemyCount <= 0)
        {
           
            GameObject.FindObjectOfType<UIManager>().GameOver(true, points, respawnCount);
            points = 0;
            respawnCount = 0; spawnType = UnitType.Stage1;
            characterRecords.Clear();
        }
    }
    
    public void RespawnAll() {

        called = false;
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

        EnemyAI[] ais = GameObject.FindObjectsOfType<EnemyAI>();
        enemyCount = ais.Length;

    }

    public static bool called = false;
    public static void ResetScene() {
        if (!called)
        {
            respawnCount += 1;
            GameObject.FindObjectOfType<UIManager>().Tries.text = respawnCount + "/ 70";
            CharacterRecorder[] rec = FindObjectsOfType<CharacterRecorder>();
            characterRecords.Clear();
            for (int i = 0; i < rec.Length; i++)
            {

                characterRecords.Add(rec[i]);
            }

            //70
            if (respawnCount < 70)
            {
                print("RESPAWN NEXT SEGMENT");
                Application.LoadLevel(2);
            }
            else
            {
                
                GameObject.FindObjectOfType<UIManager>().GameOver(false, points, respawnCount);
                points = 0;
                respawnCount = 0;
                characterRecords.Clear();
                print("YOU DIED GAME OVER");
            }
            called = true;
        }
    }
        
}
