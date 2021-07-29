using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    [Header("Prefabs")]
    public Bullet bulletPrefab; 
    [Header("Setup")]
    public float timeBetweenBullets;
    public float speed;
    public float rotatingSpeed;

    //#### State ####
    private float rotating; 
    private bool isThrusting;
    //###############
    private bool canShot = true;
    private GameObject bulletPoint;
    private Rigidbody rb;

    private void Awake() 
    {
        rb = GetComponent<Rigidbody>();
        bulletPoint = transform.Find("BulletPoint").gameObject;  
    }
    // Update is called once per frame
    void Update()
    {
        isThrusting = InputManager.Instance.isPressingForward;
        if(InputManager.Instance.isPressingLeft)
        {
            rotating = -1.0f;
        } 
        else if(InputManager.Instance.isPressingRight)
        {
            rotating = 1.0f;
        }
        else
        {
            rotating = 0.0f;
        }

        if(InputManager.Instance.isFiring && canShot)
        {
            StartCoroutine(Shoot());
        }
    }

    private void FixedUpdate() 
    {
        if(isThrusting)
        {
            rb.AddForce(this.transform.forward);
        }

        if(rotating != 0)
        {
            rb.AddTorque( new Vector3(0, rotating * rotatingSpeed, 0));
        }
    }

    IEnumerator Shoot()
    {
        canShot = false;
        Bullet bullet = Instantiate(this.bulletPrefab,bulletPoint.transform.position,this.transform.rotation);
        yield return new WaitForSeconds(timeBetweenBullets);
        canShot = true;
    }
}
