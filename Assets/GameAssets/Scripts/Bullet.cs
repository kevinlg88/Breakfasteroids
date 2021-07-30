using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Setup")]
    public float speed;
    public float lifeTime;

    [Header("Boundaries")]
    public float screenTop;
    public float screenBottom;
    public float screenRight;
    public float screenLeft;
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
    private void Update() 
    {
        WrapBullet();
    }

    private void WrapBullet()
    {
        Vector3 newPos = transform.position;
        if(transform.position.z > screenTop)
        {
            newPos.z = screenBottom;
        }
        if(transform.position.z < screenBottom)
        {
            newPos.z = screenTop;
        }
        if(transform.position.x > screenRight)
        {
            newPos.x = screenLeft;
        }
        if(transform.position.x < screenLeft)
        {
            newPos.x = screenRight;
        }
        transform.position = newPos;
    }
}
