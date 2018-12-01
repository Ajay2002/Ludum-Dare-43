using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    float damageBullet = 10f;
    public void SetOff(float speed,Vector3 direction, float damage) {
        GetComponent<Rigidbody>().velocity = direction*speed;
        damageBullet = damage;
    }

    float selfDestruct = 0f;
    private void Update() {
        selfDestruct += Time.deltaTime;
        if (selfDestruct > 4) {
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
