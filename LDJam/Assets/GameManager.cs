using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Diagnostics;
public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public static List<CharacterRecorder> characterRecords = new List<CharacterRecorder>(); 
    public Transform Stage1;
    public string SpawnPoint;
    
    public static int respawnCount = 0;
    public UnitType spawnType;

    public List<DeathAt> enemyDeaths = new List<DeathAt>();

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
            GameManager.StartBodyClock();
            instance.beganMovement = false;
            enemyDeaths.Clear();
            containingNames.Clear();
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
        instance.beganMovement = false;
        EnemyAI[] ais = GameObject.FindObjectsOfType<EnemyAI>();
        enemyCount = ais.Length;
       
        completed = false;
        listPos = 0;
    }

    public void OneDied()
    {
        enemyCount -= 1;
        if (enemyCount <= 0)
        {
           enemyDeaths.Clear();
            GameObject.FindObjectOfType<UIManager>().GameOver(true, points, respawnCount);
            
            points = 0;
            respawnCount = 0; spawnType = UnitType.Stage1;
            characterRecords.Clear();
            containingNames.Clear();
            enemyDeaths.Clear();
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
//               print("RESPAWN!");
           //}

       }

        EnemyAI[] ais = GameObject.FindObjectsOfType<EnemyAI>();
        enemyCount = ais.Length;

        GameManager.StartBodyClock();
       
    }

    public bool beganMovement;
    public static void StartBodyClock() {
        instance.beganMovement = true;
        instance.w = new Stopwatch();
        instance.w.Reset();
        instance.w.Start();
    }

    public static CharacterMovement mainCharacter;
    public List<string> containingNames = new List<string>();
    public static void DeathHere (string GOName) {
        if (!instance.containingNames.Contains(GOName)) {
            print ("Death : " + instance.w.ElapsedMilliseconds + " of "  + GOName);
            DeathAt d = new DeathAt();
            d.time = instance.w.ElapsedMilliseconds;
            d.GOName = GOName;
            instance.containingNames.Add(d.GOName);
            instance.enemyDeaths.Add(d);
        }
        else {
             for (int i = 0; i < instance.enemyDeaths.Count; i++) {
                 if (instance.enemyDeaths[i].GOName == GOName) {
                     if (instance.enemyDeaths[i].time > instance.w.ElapsedMilliseconds) {
                         instance.enemyDeaths[i].time = instance.w.ElapsedMilliseconds;
                     }
                 }
            }
        }
    }

    public static bool called = false;
    public static void ResetScene() {
        if (!called)
        {
            respawnCount += 1;
            GameObject.FindObjectOfType<UIManager>().Tries.text = respawnCount + "/ 70";
            CharacterRecorder[] rec = FindObjectsOfType<CharacterRecorder>();
//            print(rec.Length);
            characterRecords.Clear();
            for (int i = 0; i < rec.Length; i++)
            {

                characterRecords.Add(rec[i]);
            }

            //70
            if (respawnCount < 70)
            {
                instance.beganMovement = false;
                instance.w.Stop();
                instance.w.Reset();
//                print("RESPAWN NEXT SEGMENT");
                Application.LoadLevel(GameObject.FindObjectOfType<UIManager>().moveToNext);
            }
            else
            {
                instance.beganMovement = false;
                instance.enemyDeaths.Clear();
                GameObject.FindObjectOfType<UIManager>().GameOver(false, points, respawnCount);
                points = 0;
                respawnCount = 0;
                characterRecords.Clear();
                instance.containingNames.Clear();
                print("YOU DIED GAME OVER");
            }
            called = true;
        }
    }

    private void FixedUpdate() {
        if (beganMovement == true) {
            CheckForDeaths();
        }
    }


      int listPos = 0;
      Stopwatch w;

      bool completed = false;
    private void CheckForDeaths() {
        if (instance.enemyDeaths.Count > 0) {

            DeathAt d = instance.enemyDeaths[listPos];
            if ((w.ElapsedMilliseconds>=d.time || Mathf.Approximately(w.ElapsedMilliseconds,d.time)) && !completed) {

                if (GameObject.Find(d.GOName) != null) {
                    GameObject.Find(d.GOName).GetComponent<EnemyAI>().Injure(1000);
                }
                print ("Killed : " + d.GOName + " at " + d.time + " due to un-natural causes...");

                completed = true;

                if (listPos + 1 < instance.enemyDeaths.Count)
                {
                    listPos += 1;
                    completed = false;
                }
                else {
                    completed = true;
                }


            }
            

        }
    }
        
}

public class DeathAt {
    public float time;
    public string GOName;
}