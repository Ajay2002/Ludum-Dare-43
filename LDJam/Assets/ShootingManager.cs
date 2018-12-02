using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingManager : MonoBehaviour
{
    public Transform bulletPrefab;
    public Transform gun;
    public Transform bulletSpawn;

    public ParticleSystem muzzleFlash;
    public ParticleSystem bullets;

    public bool inputLocked = false;
    public enum GunType {
        AK47,RPG,SHOTGUN,SEMI,NONE
    }
    public GunType type;

    float timer = 0f;
    float maxTime = 0f;

    float speedSetOff = 0.01f;

    private void Update() {
        if (!inputLocked) {
            if (type == GunType.AK47) {
                if (Input.GetMouseButtonDown(0)) {
                    Transform t = (Transform)Transform.Instantiate(bulletPrefab,bulletSpawn.position,Quaternion.identity);
                    t.GetComponent<BulletMovement>().SetOff(speedSetOff,bulletSpawn.forward,5);

                    muzzleFlash.Play();
                    bullets.Play();
                    maxTime = 0.1f;
                }
            }
            if (type == GunType.SHOTGUN)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Transform t = (Transform)Transform.Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity);
                    Transform t1 = (Transform)Transform.Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity);
                    Transform t2 = (Transform)Transform.Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity);
                    t.GetComponent<BulletMovement>().SetOff(speedSetOff*2, bulletSpawn.forward, 10);
                    t1.GetComponent<BulletMovement>().SetOff(speedSetOff*2, (bulletSpawn.forward + bulletSpawn.right*0.4f).normalized, 10);
                    t2.GetComponent<BulletMovement>().SetOff(speedSetOff*2, (bulletSpawn.forward - bulletSpawn.right*0.4f).normalized, 10);

                    muzzleFlash.Play();
                    bullets.Play();
                    maxTime = 0.1f;

                }
            }
            if (type == GunType.SEMI)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Transform t = (Transform)Transform.Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity);
                    Transform t1 = (Transform)Transform.Instantiate(bulletPrefab, bulletSpawn.position+bulletSpawn.forward*0.5f, Quaternion.identity);
                    Transform t2 = (Transform)Transform.Instantiate(bulletPrefab, bulletSpawn.position + bulletSpawn.forward, Quaternion.identity);
                    t.GetComponent<BulletMovement>().SetOff(speedSetOff, bulletSpawn.forward, 15);
                    t1.GetComponent<BulletMovement>().SetOff(speedSetOff, (bulletSpawn.forward*2 + bulletSpawn.right * 0f).normalized, 15);
                    t2.GetComponent<BulletMovement>().SetOff(speedSetOff, (bulletSpawn.forward*3 - bulletSpawn.right * 0f).normalized, 15);

                    muzzleFlash.Play();
                    bullets.Play();
                    maxTime = 0.3f;

                }
            }
        }

        if (maxTime > 0)
        {
            maxTime -= Time.deltaTime;
        }
        if (maxTime <= 0)
        {
            muzzleFlash.Stop();
            bullets.Stop();
        }

    }

    public void InstantiateBullet() {
         if (type == GunType.AK47) {
            
                Transform t = (Transform)Transform.Instantiate(bulletPrefab,bulletSpawn.position,Quaternion.identity);
                t.GetComponent<BulletMovement>().SetOff(speedSetOff, bulletSpawn.forward,5);
            muzzleFlash.Play();
            bullets.Play();
            maxTime = 0.1f;

        }
        if (type == GunType.SHOTGUN)
        {
          
            Transform t = (Transform)Transform.Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity);
          //  print(t.name + " "+ t.position);
          //  print(t.name + " "+ t.position);
          //  print(t.name + " "+ t.position);
            Transform t1 = (Transform)Transform.Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity);
            Transform t2 = (Transform)Transform.Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity);
            t.GetComponent<BulletMovement>().SetOff(speedSetOff * 2, bulletSpawn.forward, 10);
            t1.GetComponent<BulletMovement>().SetOff(speedSetOff * 2, (bulletSpawn.forward + bulletSpawn.right).normalized, 10);
            t2.GetComponent<BulletMovement>().SetOff(speedSetOff * 2, (bulletSpawn.forward - bulletSpawn.right).normalized, 10);
            muzzleFlash.Play();
            bullets.Play();
            maxTime = 0.1f;

        }
        if (type == GunType.SEMI)
        {
            Transform t = (Transform)Transform.Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity);
            Transform t1 = (Transform)Transform.Instantiate(bulletPrefab, bulletSpawn.position + bulletSpawn.forward * 0.5f, Quaternion.identity);
            Transform t2 = (Transform)Transform.Instantiate(bulletPrefab, bulletSpawn.position + bulletSpawn.forward, Quaternion.identity);
            t.GetComponent<BulletMovement>().SetOff(speedSetOff, bulletSpawn.forward, 25);
            t1.GetComponent<BulletMovement>().SetOff(speedSetOff, (bulletSpawn.forward * 2 + bulletSpawn.right * 0f).normalized, 25);
            t2.GetComponent<BulletMovement>().SetOff(speedSetOff, (bulletSpawn.forward * 3 - bulletSpawn.right * 0f).normalized, 25);
            muzzleFlash.Play();
            bullets.Play();
            maxTime = 0.3f;

        }
    }


}
