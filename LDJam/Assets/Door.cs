using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public float completedY = 90;


    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "DoorBreaker")
        {
            transform.root.eulerAngles = new Vector3(transform.root.eulerAngles.x, completedY, transform.root.eulerAngles.z);
        }
        
    }

}
