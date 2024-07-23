using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoncyBall : MonoBehaviour
{
    private Rigidbody body;
    private Vector3 lastVellocity;

    private void Awake()
    {
        body = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        lastVellocity =  body.velocity;
    }

    private void OnCollisionEnter(Collision collision)
    {
        var speed = lastVellocity.magnitude;
        var direction = Vector3.Reflect(lastVellocity.normalized, collision.contacts[0].normal);

        body.velocity = direction * Mathf.Max(speed, 0f);
    }
}
