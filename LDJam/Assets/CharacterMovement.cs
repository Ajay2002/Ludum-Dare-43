using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterMovement : MonoBehaviour
{   
    
    public Image gealth;
    public bool isMainCharacter = true;
    public float Health = 100f;
    public float playerHeight;
    public float speed = 0f;
    public float maxSpeed = 4f;
    public float rotationSpeed = 0f;
    public Rigidbody rBody;
    public Animator animator;
    public Camera cam;
    public Transform aimSphere;
    public Transform model;
    public Transform gun;
    public Transform originalGun;
    public bool inputLock = false;
    public CharacterRecorder recorder;

    private SFX sfx;
    public AudioSource sourceDamage;
    public AudioSource sourceFootsteps;

    //02
    public MeshRenderer helment;

    //STAGE SEPERATION
    [Header("Stage 1")]
    public Transform gun1;
    public Color helmetColor;

    [Header("Stage 2")]
    public Transform gun2;
    public Color helmentColor2;

    [Header("Stage 3")]
    public Transform gun3;
    public Color helmentColor3;

    [Header("Stage 4")]
    public Transform gun4;
    public Color helmetColor4;

    float maxHealth = 0f;


    public void SetType (int index)
    {
        int cost = 0;
        if (index == 0)
        {
            cost = 0;
        }
        else if (index == 1)
        {
            cost = 30;
        }
        else if (index == 2)
        {
            cost = 100;
        }
        else if (index == 3)
        {
            cost = 5;
        }

        if (cost > GameManager.points)
        {
            GameObject.FindObjectOfType<UIManager>().InSufficientFunds();
        }
        else
        {
            GameManager.points -= cost;
            if (index == 0)
            {
                recorder.unitType = UnitType.Stage1;
            }
            else if (index == 1)
            {
                recorder.unitType = UnitType.Stage2;
            }
            else if (index == 2)
            {
                recorder.unitType = UnitType.Stage3;
            }
            else if (index == 3)
            {
                recorder.unitType = UnitType.DoorHandler;
            }

            if (recorder.unitType == UnitType.Stage1)
            {
                gun2.gameObject.SetActive(false);
                gun3.gameObject.SetActive(false);
                gun1.transform.GetComponent<MeshRenderer>().enabled = true;
                gun4.gameObject.SetActive(false);
                helment.materials[2].color = helmetColor;

                shootingManager.gun = gun1;
                gun = gun1;
                shootingManager.type = ShootingManager.GunType.AK47;

                Health = 60;
                speed = 0.01f;

            }
            else if (recorder.unitType == UnitType.Stage2)
            {
                gun2.gameObject.SetActive(true);
                gun3.gameObject.SetActive(false);
                gun1.transform.GetComponent<MeshRenderer>().enabled = false;
                gun4.gameObject.SetActive(false);
                helment.materials[2].color = helmentColor2;

                shootingManager.gun = gun2;
                gun = gun2;
                shootingManager.type = ShootingManager.GunType.SHOTGUN;

                Health = 170;
                speed = 0.015f;
            }
            else if (recorder.unitType == UnitType.Stage3)
            {
                gun2.gameObject.SetActive(false);
                gun3.gameObject.SetActive(true);
                gun1.transform.GetComponent<MeshRenderer>().enabled = false;
                gun4.gameObject.SetActive(false);
                helment.materials[2].color = helmentColor3;

                shootingManager.type = ShootingManager.GunType.SEMI;
                gun = gun3;
                shootingManager.gun = gun3;

                Health = 270;
                speed = 0.02f;
            }

            else if (recorder.unitType == UnitType.DoorHandler)
            {
                gun2.gameObject.SetActive(false);
                gun3.gameObject.SetActive(false);
                gun1.transform.GetComponent<MeshRenderer>().enabled = false;
                gun4.gameObject.SetActive(true);

                helment.materials[2].color = helmetColor4;

                shootingManager.type = ShootingManager.GunType.NONE;
                gun = gun4;
                shootingManager.gun = gun1;

                Health = 170;
                speed = 0.009f;

            }

            GameObject.FindObjectOfType<UIManager>().HideSelectionScreen();
            Time.timeScale = 1f;
            GameObject.FindObjectOfType<GameManager>().RespawnAll();
        }

    }

    private void Start() {

        sfx = GameObject.FindObjectOfType<SFX>();
        manUI = GameObject.FindObjectOfType<UIManager>();
        recorder = GetComponent<CharacterRecorder>();

        if (recorder.unitType == UnitType.Stage1)
        {
            gun2.gameObject.SetActive(false);
            gun3.gameObject.SetActive(false);
            gun4.gameObject.SetActive(false);
            gun1.transform.GetComponent<MeshRenderer>().enabled = true;
            helment.materials[2].color = helmetColor;

            shootingManager.type = ShootingManager.GunType.AK47;
            gun = gun1;
            shootingManager.gun = gun1;

            Health = 60;
            speed = 0.01f;

        }
        else if (recorder.unitType == UnitType.Stage2)
        {
            gun2.gameObject.SetActive(true);
            gun3.gameObject.SetActive(false);
            gun1.transform.GetComponent<MeshRenderer>().enabled = false;
            gun4.gameObject.SetActive(false);
            helment.materials[2].color = helmentColor2;
            Health = 120;
            speed = 0.015f;

            shootingManager.type = ShootingManager.GunType.SHOTGUN;
            gun = gun2;
            shootingManager.gun = gun2;

        }
        else if (recorder.unitType == UnitType.Stage3)
        {
            gun2.gameObject.SetActive(false);
            gun3.gameObject.SetActive(true);
            gun4.gameObject.SetActive(false);
            gun1.transform.GetComponent<MeshRenderer>().enabled = false;
            helment.materials[2].color = helmentColor3;

            shootingManager.type = ShootingManager.GunType.SEMI;
            gun = gun3;
            shootingManager.gun = gun3;

            Health = 270;
            speed = 0.02f;
        }
        else if (recorder.unitType == UnitType.DoorHandler)
        {
            gun2.gameObject.SetActive(false);
            gun3.gameObject.SetActive(false);
            gun1.transform.GetComponent<MeshRenderer>().enabled = false;
            gun4.gameObject.SetActive(true);

            helment.materials[2].color = helmetColor4;

            shootingManager.type = ShootingManager.GunType.NONE;
            gun = gun4;
            shootingManager.gun = gun1;

            Health = 170;
            speed = 0.009f;

        }

        if (isMainCharacter) {
            GameObject.FindObjectOfType<ConsistentOffset>().offsetTransform = this.transform.GetChild(0);
            recorder.AddEmptyBeg();
            GameManager.mainCharacter = this;
            
              maxHealth = Health;
        }
        if (cam == null) {
            cam = Camera.main;
        }

      
    }

   

    bool dead = false;
    float timer = 0f;
    public void Injure(float damage) {
         Health -= damage;
        if (Health < 0) {
            if (dead == false)
            timer = 0.5f;
            dead = true;
            inputLock = true;
            recorder.AddEmpty();

            if (isMainCharacter)
            {
            sourceDamage.volume += 2;
            sourceDamage.clip = sfx.PlayerDeath;
            sourceDamage.pitch = 0.9f;
            if (!sourceDamage.isPlaying)
                sourceDamage.Play();

            }

            Invoke("Invokable", 0.5f);
                //Blood Particles whatever
            //}
            //Any death particles + Destroy (Sink)


            //Return to menu etc...
        }
        else {

            if (isMainCharacter)
            {
                sourceDamage.clip = sfx.PlayerDeath;
                sourceDamage.pitch = 3;
                if (!sourceDamage.isPlaying)
                    sourceDamage.Play();
                //Blood Particles whatever
            }

        }
    }

    void Invokable()
    {
        if (isMainCharacter)
        {
            GameManager.called = false;

            GameManager.ResetScene();
        }
        else
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    UIManager manUI; 
    private float v = 1f;
    private void Update()
    {

        if (isMainCharacter) {
            v = Mathf.Lerp(v,Health/maxHealth,0.06f);
            gealth.color = Color.Lerp(Color.red,Color.green,v);

        }

        if (manUI.gameOver)
            inputLock = true;

        if (timer > 0 && dead == true)
        {
            timer -= Time.deltaTime;
            transform.localScale = Vector3.Lerp(transform.localScale, Vector3.zero, 0.2f);
        }
        
        if (timer <= 0 && dead == true)
        {
            if (isMainCharacter)
            {
                GameManager.called = false;

                GameManager.ResetScene();
            }
            else
            {
                transform.GetChild(0).gameObject.SetActive(false);
            }
        }
    }

    [HideInInspector]
    public Vector3 inp = Vector3.zero;

    private bool W,A,S,D;
    private bool PW,PA,PS,PD;
    
    private void InputGain() {
        if (!inputLock) {
            if (Input.GetKeyDown(KeyCode.W))
            {
               W = true;
               if (PW != W)
               recorder.KeyDownPressed(KeyCode.W);
            }
            
            if (Input.GetKeyDown(KeyCode.S))
            {
               S = true;
               if (PS != S)
                recorder.KeyDownPressed(KeyCode.S);
            }

            if (Input.GetKeyUp(KeyCode.W))
            {
                W = false;

                if (PW != W)
                recorder.KeyUpPressed(KeyCode.W);
            }
            
            if (Input.GetKeyUp(KeyCode.S))
            {
                S = false;

                if (PS != S)
                recorder.KeyUpPressed(KeyCode.S);
            }

            if (Input.GetKeyDown(KeyCode.A))
            {
                A = true;

                if (PA != A)
                 recorder.KeyDownPressed(KeyCode.A);
            }
            
            if (Input.GetKeyDown(KeyCode.D))
            {
                D = true;
                if (PD != D)
                 recorder.KeyDownPressed(KeyCode.D);
            }

            if (Input.GetKeyUp(KeyCode.A))
            {
               A = false;

               if (PA != A)
               recorder.KeyUpPressed(KeyCode.A);
            }
            
            if (Input.GetKeyUp(KeyCode.D))
            {
                D = false;

                if (PD != D)
                recorder.KeyUpPressed(KeyCode.D);
            }
            
            PW = W; PA = A; PS = S; PD = D;
            InputStroke2(W,A,S,D);

         }

    }

    private void FixedUpdate() {
         InputGain();

        

         //Get Key Check
         //Connect this directly to recorder
         //+ Don't allow multiple inputs
         
       /* if (!inputLock) {
            if (Input.GetKey(KeyCode.W)) {
                inp.z = Mathf.Lerp(inp.z,1,0.5f);
                W = true;
            }
            else {
                W = false;
            }
            
            if (Input.GetKey(KeyCode.S)) {
                inp.z = Mathf.Lerp(inp.z,-1,0.5f);
                S = true;
            }
            else {
                S = false;
            }

            if (Input.GetKey(KeyCode.A)) {
                A = true;
                inp.x = Mathf.Lerp(inp.x,-1,0.5f);
            }
            else 
            A = false;
            
            if (Input.GetKey(KeyCode.D)) {
                inp.x = Mathf.Lerp(inp.x,1,0.5f);
                D = true;
            }
            else
            D = false;

            InputChangeCheck();

            PW = W;
            PA = A;
            PS = S;
            PD = D;

            inp = Vector3.ClampMagnitude(inp,1);

            MovementCode(inp*speed);
            AimCode();

            inp = Vector3.zero;
            //inp = Vector3.Lerp(inp, Vector3.zero,0.3f);
        } */

    }

    private void InputChangeCheck() {
        if (PW != W) {
            if (W == false)
                recorder.KeyUpPressed(KeyCode.W);
            else 
                recorder.KeyDownPressed(KeyCode.W);
        }
        if (PS != S) {
            if (S == false)
                recorder.KeyUpPressed(KeyCode.S);
            else 
                recorder.KeyDownPressed(KeyCode.S);
        }
        if (PD != D) {
            if (D == false)
                recorder.KeyUpPressed(KeyCode.D);
            else 
                recorder.KeyDownPressed(KeyCode.D);
        }
        if (PA != A) {
            if (A == false)
                recorder.KeyUpPressed(KeyCode.A);
            else 
                recorder.KeyDownPressed(KeyCode.A);
        }
    }

    public ShootingManager shootingManager;

    private bool shot = false;
    public void InputStroke(bool W, bool A, bool S, bool D, bool C, Vector3 InpPos, Vector3 gunAngle) {
        if (!dead) {
            if (W) {
                inp.z = Mathf.Lerp(inp.z,1,0.5f);
            }
            
            if (S) {
                inp.z = Mathf.Lerp(inp.z,-1,0.5f);
            }

            if (A) {
                inp.x = Mathf.Lerp(inp.x,-1,0.5f);
            }
            
            if (D) {
                inp.x = Mathf.Lerp(inp.x,1,0.5f);
            }

            if (C && !shot) {
                shot = true;
                shootingManager.InstantiateBullet();
            }
            if (!C) {
                shot = false;
            }

            inp = Vector3.ClampMagnitude(inp,1);

            MovementCode(inp*speed);
            AimCode(InpPos,gunAngle);

            inp = Vector3.zero;
            //inp = Vector3.Lerp(inp, Vector3.zero, 0.3f);
        }
    }

     public void InputStroke2(bool W, bool A, bool S, bool D) {
        if (!dead) {
            if (W) {
                inp.z = Mathf.Lerp(inp.z,1,0.5f);
            }
            
            if (S) {
                inp.z = Mathf.Lerp(inp.z,-1,0.5f);
            }

            if (A) {
                inp.x = Mathf.Lerp(inp.x,-1,0.5f);
            }
            
            if (D) {
                inp.x = Mathf.Lerp(inp.x,1,0.5f);
            }

            inp = Vector3.ClampMagnitude(inp,1);

            MovementCode(inp*speed);
            AimCode();

            inp = Vector3.zero;
            //inp = Vector3.Lerp(inp, Vector3.zero, 0.3f);
        }
    }

    private void MovementCode(Vector3 input) {

      
        
        if (input.magnitude > 0f) {
            if (isMainCharacter)
            {
                if (sourceFootsteps.clip != sfx.FootStep)
                    sourceFootsteps.clip = sfx.FootStep;
                if (!sourceFootsteps.isPlaying)
                    sourceFootsteps.Play();
                
            }
            animator.SetBool("walking", true);
        }
        else {
            animator.SetBool("walking",false);
        }
        
     
        if (cam != null) { 
            input = cam.transform.TransformDirection(input);
            input.y*=0;
        }
        else {
            input = Camera.main.transform.TransformDirection(input);
            input.y*=0;
        }
    
        Vector3 retInput = model.TransformDirection(input);

       // rBody.velocity = input;
        transform.position += input;
       // transform.position = new Vector3(Mathf.RoundToInt(transform.position.x), transform.position.y, Mathf.RoundToInt(transform.position.z));
            
        Ray ray = new Ray(model.position+retInput*0.005f,-model.up);
        RaycastHit hit;
      //  Debug.DrawLine(ray.origin,ray.origin+ray.direction,Color.red,0.2f);
        if (Physics.SphereCast(ray,0.4f,out hit)) {
           // model.up = Vector3.Lerp(model.up,hit.normal,0.22f);
            model.position = Vector3.Lerp(model.position,new Vector3(model.position.x, hit.point.y+playerHeight, model.position.z),0.94f);
        }

        //GroundUpdate();


    }

    private void AimCode() {

       // gun.localEulerAngles = new Vector3(Mathf.Lerp(gun.localEulerAngles.x,gun.localEulerAngles.x-Input.mouseScrollDelta.y*0.4f,0.8f),gun.localEulerAngles.y,gun.localEulerAngles.z);

        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit)) {
            Vector3 hP = hit.point;
            aimSphere.position = hP;
            hP.y = model.position.y;
            model.LookAt(hP);
            gun.LookAt(hit.point);
            originalGun.LookAt(hit.point);

        }

    }

    private void AimCode(Vector3 inp, Vector3 gAngle) {
        originalGun. eulerAngles = gAngle;
        gun.eulerAngles = gAngle;
        model.eulerAngles = inp;
        /*Ray ray = cam.ScreenPointToRay(inp);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit)) {
            Vector3 hP = hit.point;
            hP.y = model.position.y;
            model.LookAt(hP);

        }*/

    }

    



}
