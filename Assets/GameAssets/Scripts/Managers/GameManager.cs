using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }
    public Rocket rocket;
    public float respawnTime = 2.5f;
    public int healthPoints = 3;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }
    }
    public void RocketDestroyed()
    {
        healthPoints--;
        if(healthPoints <= 0)
        {
            GameOver();
        }
        else
        {
            StartCoroutine(Respawn());
        }
    }
    IEnumerator Respawn()
    {
        yield return new WaitForSeconds(respawnTime);
        rocket.transform.position = new Vector3(0,0.47f,0);
        rocket.gameObject.SetActive(true);
    }
    private void GameOver()
    {
        //TODO
        print("Game Over");
    }
}
