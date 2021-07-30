using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }
    [Header("Rocket")]
    public Rocket rocket;
    public GameObject hpHud;
    public float respawnTime = 2.5f;
    private int healthPoints = 3;

    [Header("Game")]
    public TextMeshProUGUI scoreHud;
    public GameObject gameOverHud;
    private int score = 0;
    [HideInInspector]
    public bool isGameOver = false;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }
    }

    private void Start() 
    {
        AudioManager.Instance.Play("Background");
    }
    public void AsteroidDestroyed(Asteroid asteroid)
    {
        if(asteroid.size < asteroid.minSize + 0.60f)
        {
            score += 100;
        }
        else if(asteroid.size < asteroid.size + 0.60f)
        {
            score += 60;
        }
        else
        {
            score += 30;
        }
        UpdateScoreHud();
    }

    public void RocketDestroyed()
    {
        UpdateRocketHpHud();
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
    private void UpdateScoreHud()
    {
        scoreHud.text = "Score: " + score;
    }
    private void UpdateRocketHpHud()
    {
        hpHud.transform.GetChild(healthPoints - 1).gameObject.SetActive(false);
    }
    private void GameOver()
    {
        isGameOver = true;
        gameOverHud.SetActive(true);
    }
}
