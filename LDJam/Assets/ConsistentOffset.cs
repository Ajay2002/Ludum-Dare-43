using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsistentOffset : MonoBehaviour
{

    public Transform offsetTransform;
    public Transform gun;

    public Vector3 offsetVector;
    public float speed;

    private void LateUpdate() {
          if (transform != null)
              transform.position = offsetTransform.position + offsetVector;

        //transform.position = offsetTransform.position - gun.forward * 3;
    }


}
