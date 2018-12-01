using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float playerHeight;
    public float speed = 0f;
    public float maxSpeed = 4f;
    public float rotationSpeed = 0f;
    public Rigidbody rBody;
    public Animator animator;
    public Camera cam;
    public Transform model;
public Transform gun;
    public bool inputLock = false;
    private void Awake() {
        
    }
    [HideInInspector]
    public Vector3 inp = Vector3.zero;
    private void FixedUpdate() {
         
         
        if (!inputLock) {
            if (Input.GetKey(KeyCode.W)) {
                inp.z = Mathf.Lerp(inp.z,1,0.4f);
            }
            else if (Input.GetKey(KeyCode.S)) {
                inp.z = Mathf.Lerp(inp.z,-1,0.4f);
            }

            if (Input.GetKey(KeyCode.A)) {
                inp.x = Mathf.Lerp(inp.x,-1,0.4f);
            }
            else if (Input.GetKey(KeyCode.D)) {
                inp.x = Mathf.Lerp(inp.x,1,0.4f);
            }

            inp = Vector3.ClampMagnitude(inp,1);

            MovementCode(inp*speed);
            AimCode();

            inp = Vector3.Lerp(inp, Vector3.zero,0.1f);
        }

    }

    public ShootingManager shootingManager;

    private bool shot = false;
    public void InputStroke(bool W, bool A, bool S, bool D, bool C, Vector3 InpPos, float gunAngle) {
        if (W) {
            inp.z = Mathf.Lerp(inp.z,1,0.4f);
        }
        else if (S) {
            inp.z = Mathf.Lerp(inp.z,-1,0.4f);
        }

        if (A) {
            inp.x = Mathf.Lerp(inp.x,-1,0.4f);
        }
        else if (D) {
            inp.x = Mathf.Lerp(inp.x,1,0.4f);
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

        inp = Vector3.Lerp(inp, Vector3.zero,0.1f);
    }

    private void MovementCode(Vector3 input) {

      
        
        if (input.magnitude > 0.1f) {
            animator.SetBool("walking",true);
        }
        else {
            animator.SetBool("walking",false);
        }
        
     
        
        input = cam.transform.TransformDirection(input);
        input.y*=0;
    
        Vector3 retInput = model.TransformDirection(input);

        //rBody.velocity = input;
        transform.position += input;

            
        Ray ray = new Ray(model.position+retInput*0.005f,-model.up);
        RaycastHit hit;
        Debug.DrawLine(ray.origin,ray.origin+ray.direction,Color.red,0.2f);
        if (Physics.SphereCast(ray,0.4f,out hit)) {
           // model.up = Vector3.Lerp(model.up,hit.normal,0.22f);
            model.position = Vector3.Lerp(model.position,new Vector3(model.position.x, hit.point.y+playerHeight, model.position.z),0.94f);
        }

        //GroundUpdate();


    }

    private void AimCode() {

        gun.localEulerAngles = new Vector3(Mathf.Lerp(gun.localEulerAngles.x,gun.localEulerAngles.x-Input.mouseScrollDelta.y*0.4f,0.8f),gun.localEulerAngles.y,gun.localEulerAngles.z);

        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit)) {
            Vector3 hP = hit.point;
           
            hP.y = model.position.y;
            model.LookAt(hP);


        }

    }

    private void AimCode(Vector3 inp, float gAngle) {
        gun.localEulerAngles = new Vector3(gAngle,gun.localEulerAngles.y,gun.localEulerAngles.z);
        Ray ray = cam.ScreenPointToRay(inp);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit)) {
            Vector3 hP = hit.point;
            hP.y = model.position.y;
            model.LookAt(hP);

        }

    }



}
