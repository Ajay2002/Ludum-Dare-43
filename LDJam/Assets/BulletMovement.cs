using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    public void SetOff(float speed,Vector3 direction) {
        GetComponent<Rigidbody>().velocity = direction*speed;
    }

    
    void OnTriggerEnter(Collider other)
    {
        Destroy(this.gameObject);
    }
}
