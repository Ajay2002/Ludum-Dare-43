using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingManager : MonoBehaviour
{
    public Transform bulletPrefab;
    public Transform gun;
    public CharacterRecorder recorder;
    public bool isCharacter = false;
    public Transform bulletSpawn;

    public AudioSource source;
    public SFX sfx;

    public ParticleSystem muzzleFlash;

    private void Awake()
    {
        sfx = GameObject.FindObjectOfType<SFX>();
         if (GetComponent<EnemyAI>() != null) {
            inputLocked = true;
        }
    }

    public bool inputLocked = false;
    public enum GunType {
        AK47,RPG,SHOTGUN,SEMI,NONE
    }
    public GunType type;

    float timer = 0f;
    float maxTime = 0f;

    float speedSetOff = 0.01f;

    float inputDelay = 0.07f;
    float inpTimer = 0;

    bool pressed = false;
    //Connect this to character recorder as ewll
    private void Update() {
        if (!inputLocked && inpTimer <= 0) {
            if (type == GunType.AK47) {
                if (Input.GetMouseButtonDown(0)) {
                    
                    if (!pressed) {
                        //////totalClickCount += 1;
                        pressed = true;

                        recorder.ClickEvent(true);

                        Transform t = (Transform)Transform.Instantiate(bulletPrefab,bulletSpawn.position,bulletSpawn.rotation);
                        t.GetComponent<BulletMovement>().SetOff(speedSetOff,bulletSpawn.forward,5);
                        if (source.clip != sfx.Shot2)
                            source.clip = sfx.Shot2;
                        source.Play();

                        inpTimer = inputDelay;
                        muzzleFlash.Play();
                    //  bullets.Play();
                        maxTime = 0.1f;
                    }


                }
            }
            if (type == GunType.SHOTGUN)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    if (!pressed) {
                        pressed = true;
                        
                        recorder.ClickEvent(true);
                        Transform t = (Transform)Transform.Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
                        Transform t1 = (Transform)Transform.Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
                        Transform t2 = (Transform)Transform.Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
                        t.GetComponent<BulletMovement>().SetOff(speedSetOff*2, bulletSpawn.forward, 10);
                        t1.GetComponent<BulletMovement>().SetOff(speedSetOff*2, (bulletSpawn.forward + bulletSpawn.right*0.4f).normalized, 10);
                        t2.GetComponent<BulletMovement>().SetOff(speedSetOff*2, (bulletSpawn.forward - bulletSpawn.right*0.4f).normalized, 10);

                        if (source.clip != sfx.Shot3)
                        source.clip = sfx.Shot3;
                        source.Play();

                        muzzleFlash.Play();
                        //bullets.Play();
                        maxTime = 0.1f;
                        inpTimer = inputDelay;
                    }

                }
            }
            if (type == GunType.SEMI)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    if (!pressed) {
                    pressed = true;

                    recorder.ClickEvent(true);
                    Transform t = (Transform)Transform.Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
                    Transform t1 = (Transform)Transform.Instantiate(bulletPrefab, bulletSpawn.position+bulletSpawn.forward*0.5f, bulletSpawn.rotation);
                    Transform t2 = (Transform)Transform.Instantiate(bulletPrefab, bulletSpawn.position + bulletSpawn.forward, bulletSpawn.rotation);
                    t.GetComponent<BulletMovement>().SetOff(speedSetOff, bulletSpawn.forward, 7);
                    t1.GetComponent<BulletMovement>().SetOff(speedSetOff, (bulletSpawn.forward*2 + bulletSpawn.right * 0f).normalized, 7);
                    t2.GetComponent<BulletMovement>().SetOff(speedSetOff, (bulletSpawn.forward*3 - bulletSpawn.right * 0f).normalized, 7);
                    if (source.clip != sfx.Shot2)
                        source.clip = sfx.Shot2;
                    source.Play();
                    muzzleFlash.Play();
                   // bullets.Play();
                    maxTime = 0.3f;inpTimer = inputDelay;
                    }

                }
            }
        }

        if (Input.GetMouseButtonUp(0) && !inputLocked && pressed && inpTimer <= 0) {
            recorder.ClickEvent(false);
            pressed = false;
        }

        if (inpTimer > 0) {
            inpTimer -= Time.deltaTime;
        }

        if (inpTimer <= 0 ) {
            inpTimer = 0;
        }

        if (maxTime > 0)
        {
            maxTime -= Time.deltaTime;
        }
        if (maxTime <= 0)
        {
            muzzleFlash.Stop();
            //bullets.Stop();
        }

    }

    public void InstantiateBullet() {
         if (type == GunType.AK47) {
            
            //totalRplayCount += 1;
                Transform t = (Transform)Transform.Instantiate(bulletPrefab,bulletSpawn.position,bulletSpawn.rotation);
                t.GetComponent<BulletMovement>().SetOff(speedSetOff, bulletSpawn.forward,5);
            muzzleFlash.Play();
            //bullets.Play();
            maxTime = 0.1f;

           // if ( source != null && source.clip != sfx.Shot2)
           //     source.clip = sfx.Shot2;
           // if (source != null)
           //     source.Play();

        }
        if (type == GunType.SHOTGUN)
        {
          
           // Transform t = (Transform)Transform.Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
          //  print(t.name + " "+ t.position);
          //  print(t.name + " "+ t.position);
          //  print(t.name + " "+ t.position);
            Transform t = (Transform)Transform.Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
            Transform t1 = (Transform)Transform.Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
            Transform t2 = (Transform)Transform.Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
            t.GetComponent<BulletMovement>().SetOff(speedSetOff*2, bulletSpawn.forward, 10);
            t1.GetComponent<BulletMovement>().SetOff(speedSetOff*2, (bulletSpawn.forward + bulletSpawn.right*0.4f).normalized, 10);
            t2.GetComponent<BulletMovement>().SetOff(speedSetOff*2, (bulletSpawn.forward - bulletSpawn.right*0.4f).normalized, 10);
            muzzleFlash.Play();
            //bullets.Play();
            maxTime = 0.1f;

            //if (source != null &&  source.clip != sfx.Shot3)
            //    source.clip = sfx.Shot3;

            //if (source != null )
           // source.Play();

        }
        if (type == GunType.SEMI)
        {
           Transform t = (Transform)Transform.Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
                    Transform t1 = (Transform)Transform.Instantiate(bulletPrefab, bulletSpawn.position+bulletSpawn.forward*0.5f, bulletSpawn.rotation);
                    Transform t2 = (Transform)Transform.Instantiate(bulletPrefab, bulletSpawn.position + bulletSpawn.forward, bulletSpawn.rotation);
                    t.GetComponent<BulletMovement>().SetOff(speedSetOff, bulletSpawn.forward, 7);
                    t1.GetComponent<BulletMovement>().SetOff(speedSetOff, (bulletSpawn.forward*2 + bulletSpawn.right * 0f).normalized, 7);
                    t2.GetComponent<BulletMovement>().SetOff(speedSetOff, (bulletSpawn.forward*3 - bulletSpawn.right * 0f).normalized, 7);
            muzzleFlash.Play();
           // bullets.Play();
            maxTime = 0.3f;

           // if (source != null && source.clip != sfx.Shot2)
           //     source.clip = sfx.Shot2;
          //  if (source != null)
           //     source.Play();

        }
    }


}
