using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public Color[] colors;

    [Header("Setup")]
    public float size;
    public float minSize;
    public float maxSize;
    public float speed;

    [Header("Boundaries")]
    public float screenTop;
    public float screenBottom;
    public float screenRight;
    public float screenLeft;
    private Renderer render;
    private Rigidbody rb;

    private void Awake() 
    {
        render = transform.Find("Asteroid gameobject").gameObject.GetComponent<Renderer>();
        rb = GetComponent<Rigidbody>();    
    }
    void Start()
    {
        int randomNumber = Random.Range(0, colors.Length);
        render.material.color = this.colors[randomNumber];
        transform.eulerAngles = new Vector3(0.0f, Random.value * 360.0f);
        transform.localScale = Vector3.one * size;
        rb.mass = size;
    }

    private void Update() {
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


    private void OnCollisionEnter(Collision other) 
    {
        if(other.gameObject.tag == "Bullet")
        {
            if((size * 0.5f) >= minSize)
            {
                CreateSplit();
                CreateSplit();
            }
            Destroy(this.gameObject);
        }
    }

    private void CreateSplit()
    {
        Vector3 position = transform.position;
        position += Random.insideUnitSphere * 0.5f;
        position.y = transform.position.y;
        Asteroid half = Instantiate(this, position, this.transform.rotation);
        half.size = this.size * 0.5f;
        Vector3 randomDirection = Random.insideUnitSphere.normalized * speed;
        randomDirection.y = 0;
        half.SetTrajectory(randomDirection);
    }
    public void SetTrajectory(Vector3 direction)
    {
        rb.AddForce(direction * speed);
    }

}
