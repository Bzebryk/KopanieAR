using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    void Update()
    {
        if (transform.position.y < -10.0f) {
            transform.localPosition = new Vector3(0, 10, 0);
            Rigidbody body = GetComponent<Rigidbody>();
            body.velocity = Vector3.zero;
        }
    }
}
