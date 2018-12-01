using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class EnemyAI : MonoBehaviour
{
    public ShootingManager shooting;
    public float Health = 100f;
    public float fireRate = 0f;
    
    public int pointsAward = 1;
    
    float timer = 0f;
    float coolDownRate = 2f;
    int afterBullets = 20;

    int bulletsShot = 0;
    float coolTime = 0f;

    public void Injure(float damage) {
        Health -= damage;
        if (Health < 0) {

            GameManager.points += pointsAward;
            //Any death particles + Destroy (Sink)

            Destroy(this.gameObject,3f);
        }
        else {

            //Blood Particles whatever

        }
    }

    public bool aiming = false;
    public Transform aimTarg = null;
     void OnTriggerStay(Collider other)
    {
        
        if (other.transform.tag == "Player") {
             if (aimTarg != null) {
                    if (aimTarg.root.GetComponent<CharacterMovement>() != null) {
                        if (aimTarg.root.GetComponent<CharacterMovement>().Health < 0) {
                            aiming = false;
                            aimTarg = null;
                            return;
                        }
                    }
                    
                }
            if (aiming == true && other.transform == aimTarg || aiming == false) {
                
                aiming = true;
                aimTarg = other.transform;

                if (aimTarg != null) {
                    if (aimTarg.GetComponent<CharacterMovement>() != null) {
                        if (aimTarg.GetComponent<CharacterMovement>().Health < 0) {
                            aiming = false;
                            aimTarg = null;
                            return;
                        }
                    }
                    
                }

                Vector3 targetPostition = new Vector3(other.transform.position.x, 
                                                this.transform.root.position.y, 
                                                other.transform.position.z ) ;
                this.transform.root.LookAt( targetPostition ) ;
                
                timer += Time.deltaTime;
                if (timer > fireRate && coolTime <= 0) {
                    shooting.InstantiateBullet();
                    timer = 0f;
                    bulletsShot += 1;
                }

                if (coolTime > 0) {
                    coolTime -= Time.deltaTime;
                }
                else{
                
                }

                if (bulletsShot > afterBullets) {

                    bulletsShot = 0;
                    coolTime = coolDownRate;

                }
            }

        }


    }

    void OnTriggerExit(Collider other) {
        if (aimTarg == other.transform) {
            aiming = false;
            aimTarg = null;
        }
    }


}
