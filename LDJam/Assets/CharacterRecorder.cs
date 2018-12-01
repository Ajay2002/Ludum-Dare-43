using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterRecorder : MonoBehaviour
{
    #region  Variables
    public CharacterMovement character;
    public ShootingManager shooting;
    public float timer = 0f;
    public List<InputStroke> strokes = new List<InputStroke>();
    public Dictionary<float, InputStroke> strokeDict = new Dictionary<float, InputStroke>();
    public InputStroke current;
    public InputStroke waitFor;
    public bool recordingMode = false;
    public bool playingMode = false;

    private Vector3 recordingStart;

    #endregion
    
    private void FixedUpdate() {

        if (Input.GetKeyDown(KeyCode.R)) {
            ResetRecord();
        }

        if (Input.GetKeyDown(KeyCode.P)) {
            PlayReset();
        }

        if (recordingMode || playingMode) {
            timer += 0.0001f;
        }
        if (recordingMode) {
            InputGain();
        }
        if (playingMode == true) {
            Playing();
        }
    
    }

    int pos = 0;

    bool W,A,S,D,C;
    Vector3 mPos;
    float gAngle;

    public void Playing() {

        InputStroke s = null;
        strokeDict.TryGetValue(timer,out s);

        if (s!=null) {
            waitFor = s;

            if (waitFor.EVENTVERT == "WDown") {
                W = true;
            }
            else if (waitFor.EVENTVERT == "SDown") {
                S = true;
            }

            if (waitFor.EVENTVERT == "WUp") {
                W = false;
            }
            else if (waitFor.EVENTVERT == "SUp") {
                S = false;
            }

            if (waitFor.EVENTHORIZ == "ADown") {
                A = true;
            }
            else if (waitFor.EVENTHORIZ == "DDown") {
                D = true;
            }

            if (waitFor.EVENTHORIZ == "AUp") {
                A = false;
            }
            else if (waitFor.EVENTHORIZ == "DUp") {
                D = false;
            }
            
            if (waitFor.EVENTCLICK == "Down") {
                C = true;
                mPos = waitFor.MousePos;
            }
            else if (waitFor.EVENTCLICK == "Up") {
                C = false;
                mPos = waitFor.MousePos;
            }

            mPos = waitFor.MousePos;
            gAngle = waitFor.gunAngle;
        }

        character.InputStroke(W,A,S,D,C,mPos,gAngle);

    }

    InputStroke previous = null;
    private void InputGain() {
        bool eventRequired = false;
        InputStroke s = new InputStroke();
        
       
        if (Input.GetKeyDown(KeyCode.W)) {
            s.EVENTVERT = "WDown";
            s.MousePos = Input.mousePosition;
            eventRequired = true;
            s.gunAngle = character.gun.localEulerAngles.x;
        }
        else if (Input.GetKeyDown(KeyCode.S)) {
            s.EVENTVERT = "SDown";
            s.MousePos = Input.mousePosition;
            eventRequired = true;
            s.gunAngle = character.gun.localEulerAngles.x;
        }

        if (Input.GetKeyUp(KeyCode.W)) {
            s.EVENTVERT = "WUp";
            s.MousePos = Input.mousePosition;
            eventRequired = true;
            s.gunAngle = character.gun.localEulerAngles.x;
        }
        else if (Input.GetKeyUp(KeyCode.S)) {
            s.EVENTVERT = "SUp";
            s.MousePos = Input.mousePosition;
            eventRequired = true;
            s.gunAngle = character.gun.localEulerAngles.x;
        }

        if (Input.GetKeyDown(KeyCode.A)) {
            s.EVENTHORIZ = "ADown";
            s.MousePos = Input.mousePosition;
            eventRequired = true;
            s.gunAngle = character.gun.localEulerAngles.x;
        }
        else if (Input.GetKeyDown(KeyCode.D)) {
            s.EVENTHORIZ = "DDown";
            s.MousePos = Input.mousePosition;
            eventRequired = true;
            s.gunAngle = character.gun.localEulerAngles.x;
        }

        if (Input.GetKeyUp(KeyCode.A)) {
            s.EVENTHORIZ = "AUp";
            s.MousePos = Input.mousePosition;
            eventRequired = true;
            s.gunAngle = character.gun.localEulerAngles.x;
        }
        else if (Input.GetKeyUp(KeyCode.D)) {
            s.EVENTHORIZ = "DUp";
            s.MousePos = Input.mousePosition;
            eventRequired = true;
            s.gunAngle = character.gun.localEulerAngles.x;
        }

        if (Input.GetMouseButtonDown(0)) {
            s.EVENTCLICK="Down";eventRequired = true;
            s.MousePos = Input.mousePosition;
            s.gunAngle = character.gun.localEulerAngles.x;
        }

        if (Input.GetMouseButtonUp(0)) {
            s.EVENTCLICK="Up";eventRequired = true;
            s.MousePos = Input.mousePosition;
            s.gunAngle = character.gun.localEulerAngles.x;
        }

        s.AtTime = timer;

        if (strokes.Count > 0) {
            if (strokes[strokes.Count-1].EVENTHORIZ == s.EVENTHORIZ &&strokes[strokes.Count-1].EVENTCLICK == s.EVENTCLICK && strokes[strokes.Count-1].EVENTVERT == s.EVENTVERT) {

            }
            else {
                if (eventRequired) {
                    strokeDict.Add(timer,s);
                }
            }
        }
        else  {
            if (eventRequired) {
                strokeDict.Add(timer,s);
            }
        }

    }
    
    private void ResetRecord() {
        
        recordingMode = false;
        playingMode = false;
        timer = 0f;
        strokeDict.Clear();
        recordingStart = transform.position;
        current = null;
        pos = 0;
        waitFor = null;
        strokes.Clear();

        recordingMode = true;
    }

    private void PlayReset() {
        

        character.rBody.collisionDetectionMode = CollisionDetectionMode.Discrete;
        character.inp = Vector3.zero;
        shooting.inputLocked = true;
        recordingMode = false;
        playingMode = false;
        character.inputLock = true;
         timer = 0f;transform.position = recordingStart;
        
       
        playingMode = true;

    }

}

[System.Serializable]
public class InputStroke {

    public string EVENTVERT;
    public string EVENTHORIZ;
    public string EVENTCLICK;
    public Vector3 MousePos;
    public float gunAngle;

    public float AtTime = 0f;


}

//THATS IT FOR A FULL INPUT RECORDER!!!