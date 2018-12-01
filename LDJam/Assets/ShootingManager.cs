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
                    t.GetComponent<BulletMovement>().SetOff(10,bulletSpawn.forward);
                }
            }
        }

    }

    public void InstantiateBullet() {
         if (type == GunType.AK47) {
            
                Transform t = (Transform)Transform.Instantiate(bulletPrefab,bulletSpawn.position,Quaternion.identity);
                t.GetComponent<BulletMovement>().SetOff(10,bulletSpawn.forward);
            
        }
    }


}
