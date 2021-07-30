using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;

    public float lifeTime;
    private Rigidbody rb;
    private void Awake() 
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Start() {
        rb.AddForce(transform.forward * speed);
        Destroy(this.gameObject, lifeTime);
    }

    private void OnCollisionEnter(Collision other) {
        Destroy(this.gameObject);
    }
}
