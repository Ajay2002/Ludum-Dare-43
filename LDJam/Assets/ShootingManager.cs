using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingManager : MonoBehaviour
{
    public Transform bulletPrefab;
    public Transform gun;
    public Transform bulletSpawn;

    public bool inputLocked = false;
    public enum GunType {
        AK47,RPG,SHOTGUN,SEMI
    }
    public GunType type;
    private void Update() {
        if (!inputLocked) {
            if (type == GunType.AK47) {
                if (Input.GetMouseButtonDown(0)) {
                    Transform t = (Transform)Transform.Instantiate(bulletPrefab,bulletSpawn.position,Quaternion.identity);
                    t.GetComponent<BulletMovement>().SetOff(15,bulletSpawn.forward,5);
                }
            }
            if (type == GunType.SHOTGUN)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Transform t = (Transform)Transform.Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity);
                    Transform t1 = (Transform)Transform.Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity);
                    Transform t2 = (Transform)Transform.Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity);
                    t.GetComponent<BulletMovement>().SetOff(10, bulletSpawn.forward, 10);
                    t1.GetComponent<BulletMovement>().SetOff(10, (bulletSpawn.forward + bulletSpawn.right*0.4f).normalized, 10);
                    t2.GetComponent<BulletMovement>().SetOff(10, (bulletSpawn.forward - bulletSpawn.right*0.4f).normalized, 10);
                }
            }
            if (type == GunType.SEMI)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Transform t = (Transform)Transform.Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity);
                    Transform t1 = (Transform)Transform.Instantiate(bulletPrefab, bulletSpawn.position+bulletSpawn.forward*0.5f, Quaternion.identity);
                    Transform t2 = (Transform)Transform.Instantiate(bulletPrefab, bulletSpawn.position + bulletSpawn.forward, Quaternion.identity);
                    t.GetComponent<BulletMovement>().SetOff(10, bulletSpawn.forward, 25);
                    t1.GetComponent<BulletMovement>().SetOff(10, (bulletSpawn.forward*2 + bulletSpawn.right * 0f).normalized, 25);
                    t2.GetComponent<BulletMovement>().SetOff(10, (bulletSpawn.forward*3 - bulletSpawn.right * 0f).normalized, 25);
                }
            }
        }

    }

    public void InstantiateBullet() {
         if (type == GunType.AK47) {
            
                Transform t = (Transform)Transform.Instantiate(bulletPrefab,bulletSpawn.position,Quaternion.identity);
                t.GetComponent<BulletMovement>().SetOff(10,bulletSpawn.forward,5);
            
        }
        if (type == GunType.SHOTGUN)
        {
                Transform t = (Transform)Transform.Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity);
                Transform t1 = (Transform)Transform.Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity);
                Transform t2 = (Transform)Transform.Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity);
                t.GetComponent<BulletMovement>().SetOff(10, bulletSpawn.forward, 10);
                t1.GetComponent<BulletMovement>().SetOff(10, (bulletSpawn.forward + bulletSpawn.right).normalized, 10);
                t2.GetComponent<BulletMovement>().SetOff(10, (bulletSpawn.forward - bulletSpawn.right).normalized, 10);
        }
        if (type == GunType.SEMI)
        {
            Transform t = (Transform)Transform.Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity);
            Transform t1 = (Transform)Transform.Instantiate(bulletPrefab, bulletSpawn.position + bulletSpawn.forward * 0.5f, Quaternion.identity);
            Transform t2 = (Transform)Transform.Instantiate(bulletPrefab, bulletSpawn.position + bulletSpawn.forward, Quaternion.identity);
            t.GetComponent<BulletMovement>().SetOff(10, bulletSpawn.forward, 25);
            t1.GetComponent<BulletMovement>().SetOff(10, (bulletSpawn.forward * 2 + bulletSpawn.right * 0f).normalized, 25);
            t2.GetComponent<BulletMovement>().SetOff(10, (bulletSpawn.forward * 3 - bulletSpawn.right * 0f).normalized, 25);
         
        }
    }


}
