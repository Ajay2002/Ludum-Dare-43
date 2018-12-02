using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    float damageBullet = 10f;
    Vector3 dir;
    public float damage;
    float speed;
    public void SetOff(float speed,Vector3 direction, float damage) {
        //GetComponent<Rigidbody>().velocity = direction*speed;
        this.damage = damage;
        this.speed = speed;
        dir = direction;
        damageBullet = damage;
    }

    float selfDestruct = 0f;
    private void FixedUpdate() {

        transform.position += dir * speed;

        selfDestruct += Time.deltaTime;
        if (selfDestruct > 1.3f) {
            Destroy(this.gameObject);
        }
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") {
            other.transform.root.GetComponent<CharacterMovement>().Injure(damageBullet);
        }

        if (other.tag == "Enemy") {
            other.transform.root.GetComponent<EnemyAI>().Injure(damageBullet);
        }

        Destroy(this.gameObject);
    }
}
