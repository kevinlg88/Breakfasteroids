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

    [Header("Wrap")]
    public float screenTop;
    public float screenBottom;
    public float screenRight;
    public float screenLeft;
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
        WrapRocket();
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

    private void OnCollisionEnter(Collision other) 
    {
        if(other.gameObject.tag == "Asteroid")
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            this.gameObject.SetActive(false);
            GameManager.Instance.RocketDestroyed();
        }   
    }
    private void OnEnable() 
    {
        gameObject.layer = LayerMask.NameToLayer("IgnoreCollisions"); 
        Invoke(nameof(ReturnPlayerLayer),3.0f);  
    }
    private void ReturnPlayerLayer()
    {
        gameObject.layer = LayerMask.NameToLayer("Player");  
    }
    IEnumerator Shoot()
    {
        canShot = false;
        Bullet bullet = Instantiate(this.bulletPrefab,bulletPoint.transform.position,this.transform.rotation);
        yield return new WaitForSeconds(timeBetweenBullets);
        canShot = true;
    }

    private void WrapRocket()
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
