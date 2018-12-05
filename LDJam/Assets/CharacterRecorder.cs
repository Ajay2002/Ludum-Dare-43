using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

public class CharacterRecorder : MonoBehaviour
{
    #region  Variables
 
    public UnitType unitType;
    public CharacterMovement character;
    public ShootingManager shooting;

    public Stopwatch timerStopwatch = new Stopwatch();
    public List<InputStroke> strokes = new List<InputStroke>();
    public Dictionary<float, InputStroke> strokeDict = new Dictionary<float, InputStroke>();
    public InputStroke current;
    public InputStroke waitFor;
    public bool recordingMode = false;
    public bool playingMode = false;

    private Vector3 recordingStart;
    private Vector3 recordingStartEuler;
    private Vector3 recordingStartEulerGun;

    private GameManager manager;

    

    #endregion
    
    private void Awake() {
        ResetRecord();
        manager = GameObject.FindObjectOfType<GameManager>();
    }
    private void FixedUpdate() {
        /* 
        if (Input.GetKeyDown(KeyCode.R)) {
            ResetRecord();
        }

        if (Input.GetKeyDown(KeyCode.P)) {
            PlayReset();
*/
        if (playingMode) {
            Playing();
        }

    }



    IEnumerator Play() {
        while (playingMode == true) {
            Playing();
            yield return new WaitForSeconds(0.0002f);
        }
        StopCoroutine("Play");

        yield return null;
    }

 

    int pos = 0;

    bool W,A,S,D,C;
    Vector3 mPos;
    Vector3 gAngle;


    public void KeyDownPressed(KeyCode c) {
        float AtTime = timerStopwatch.ElapsedMilliseconds;
        InputStroke s = new InputStroke();

        if (c == KeyCode.W) {
            s.EVENTVERT = "WDown";
        }

        if (c == KeyCode.S) {
            s.EVENTVERT = "SDown";
        }

        if (c == KeyCode.A) {
            s.EVENTHORIZ = "ADown";
        }

        if (c == KeyCode.D) {
            s.EVENTHORIZ = "DDown";
        }

        s.AtTime = AtTime;
        s.eulerAngle = character.model.eulerAngles;
        s.gunAngle = character.gun.eulerAngles;

        strokes.Add(s);
    }

    public void KeyUpPressed(KeyCode c) {
        float AtTime = timerStopwatch.ElapsedMilliseconds;
        InputStroke s = new InputStroke();

        if (c == KeyCode.W) {
            s.EVENTVERT = "WUp";
        }

        if (c == KeyCode.S) {
            s.EVENTVERT = "SUp";
        }

        if (c == KeyCode.A) {
            s.EVENTHORIZ = "AUp";
        }

        if (c == KeyCode.D) {
            s.EVENTHORIZ = "DUp";
        }

        s.AtTime = AtTime;
        s.eulerAngle = character.model.eulerAngles;
        s.gunAngle = character.gun.eulerAngles;
        strokes.Add(s);
    }

    public void ClickEvent (bool down) {
        float AtTime = timerStopwatch.ElapsedMilliseconds;
        string st = "Up";
        InputStroke s = new InputStroke();

        if (down)
        st = "Down";

        s.EVENTCLICK = st;
        s.eulerAngle = character.model.eulerAngles;
        s.gunAngle = character.gun.eulerAngles;
        s.AtTime = AtTime;

        strokes.Add(s);

    }

    public void Playing() {
        if (character.Health > 0 && strokes.Count > 0)  {
       // InputStroke s = null;
        //strokeDict.TryGetValue(timer,out s);
        
        if (timerStopwatch.ElapsedMilliseconds>=waitFor.AtTime || Mathf.Approximately(timerStopwatch.ElapsedMilliseconds,waitFor.AtTime)) {
            //waitFor = s;

            if (waitFor.ENDTRUE == "TRUE")
            {
                character.Injure(10000);
            }

            if (waitFor.EVENTVERT == "WDown") {
                W = true;
            }
            
            if (waitFor.EVENTVERT == "SDown") {
                S = true;
            }

            if (waitFor.EVENTVERT == "WUp") {
                W = false;
            }
            
            if (waitFor.EVENTVERT == "SUp") {
                S = false;
            }

            if (waitFor.EVENTHORIZ == "ADown") {
                A = true;
            }
            
            if (waitFor.EVENTHORIZ == "DDown") {
                D = true;
            }

            if (waitFor.EVENTHORIZ == "AUp") {
                A = false;
            }
            
            if (waitFor.EVENTHORIZ == "DUp") {
                D = false;
            }
            
            if (waitFor.EVENTCLICK == "Down") {
                C = true;
                mPos = waitFor.eulerAngle;
            }
            
            if (waitFor.EVENTCLICK == "Up") {
                C = false;
                mPos = waitFor.eulerAngle;
            }

            mPos = waitFor.eulerAngle;
            gAngle = waitFor.gunAngle;

            if (pos + 1 < strokes.Count)
            {
                pos += 1;
                waitFor = strokes[pos];
            }

        }

        character.InputStroke(W,A,S,D,C,mPos,gAngle);
        }
    }

    
    private void ResetRecord() {
        
        recordingMode = false;
        playingMode = false;
        strokeDict.Clear();
        recordingStart = transform.position;
        recordingStartEuler = character.model.eulerAngles;
        recordingStartEulerGun = character.gun.eulerAngles;
        current = null;
        timerStopwatch.Reset();
        pos = 0;
        waitFor = null;
        
        strokes.Clear();

        timerStopwatch.Start();
        recordingMode = true;
        //StartCoroutine("Record");
    }

    public void AddEmpty()
    {
        InputStroke s = new InputStroke();
        s.EVENTCLICK = "Up";
        s.EVENTHORIZ = "AUp";
        s.EVENTVERT = "WUp";

        InputStroke s2 = new InputStroke();
        s.EVENTCLICK = "Up";
        s.EVENTHORIZ = "DUp";
        s.EVENTVERT = "SUp";
        
        s2.ENDTRUE = "TRUE";

        strokes.Add(s);
        strokes.Add(s2);


    }

    public void AddEmptyBeg()
    {
        InputStroke s = new InputStroke();
        s.EVENTCLICK = "Up";
        s.EVENTHORIZ = "AUp";
        s.EVENTVERT = "WUp";

        InputStroke s2 = new InputStroke();
        s.EVENTCLICK = "Up";
        s.EVENTHORIZ = "DUp";
        s.EVENTVERT = "SUp";
        
        strokes.Add(s);
        strokes.Add(s2);


    }

    public void PlayReset() {
        
        
        character.rBody.collisionDetectionMode = CollisionDetectionMode.Discrete;
        character.inp = Vector3.zero;
        shooting.inputLocked = true;
        recordingMode = false;
        playingMode = false;

        if (strokes.Count > 0)
        waitFor = strokes[pos];

        character.inputLock = true;
        timerStopwatch.Reset();
        
         transform.position = recordingStart;
         character.model.eulerAngles = recordingStartEuler;
         character.gun.eulerAngles = recordingStartEulerGun;
        character.originalGun.eulerAngles = recordingStartEulerGun;
       timerStopwatch.Start();
        playingMode = true;
       // StartCoroutine("Play");

    }

}
