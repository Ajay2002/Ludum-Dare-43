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
    
    public float timer = 0f;
    public float coolDownRate = 2f;
    public int afterBullets = 20;

    public int bulletsShot = 0;
    public float coolTime = 0f;

    public void Injure(float damage) {
        Health -= damage;
        if (Health < 0) {

            GameManager.points += pointsAward;
            //Any death particles + Destroy (Sink)
            transform.GetChild(0).GetComponent<BoxCollider>().enabled = false;
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

                    Ray ray = new Ray(shooting.bulletSpawn.position, aimTarg.position - shooting.bulletSpawn.position);
                    RaycastHit hit;
                   // Debug.DrawRay(ray.origin, ray.direction, Color.red, 0.2f);
                    if (Physics.Raycast(ray, out hit))
                    {
                     //   print(hit.transform.name + "vs" + aimTarg.name);
                        if (hit.transform != aimTarg && hit.transform.tag != "Player")
                        {
                            aiming = false;
                            aimTarg = null;
                            return;
                        }

                    }
                    else
                    {
                        aiming = false;
                        aimTarg = null;
                        return;
                    }
                    
                }

                Vector3 targetPostition = new Vector3(other.transform.position.x, 
                                                this.transform.root.position.y, 
                                                other.transform.position.z ) ;
                this.transform.root.LookAt( targetPostition ) ;
                shooting.gun.LookAt(other.transform.position);
                
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
