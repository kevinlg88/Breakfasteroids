using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAsteroid : MonoBehaviour
{
    [Header("Prefab")]
    public Asteroid asteroidPrefab;

    [Header("Setup")]
    public float trajectoryVariance = 15.0f;
    public float spawnRate;
    public int spawnAmount;
    public float spawnDistance = 15.0f;
    void Start()
    {
        InvokeRepeating(nameof(Spawn), spawnRate, spawnRate);
    }

    private void Spawn()
    {
        for(int i = 0; i < spawnAmount; i++)
        {
            Vector3 spawnDirection = Random.insideUnitSphere.normalized * spawnDistance;
            spawnDirection.y = 0;
            Vector3 spawnPoint = this.transform.position + spawnDirection;
            float variance = Random.Range(-trajectoryVariance, trajectoryVariance);
            Quaternion rotation = Quaternion.AngleAxis(variance, Vector3.up);
            Asteroid asteroid = Instantiate(asteroidPrefab, spawnPoint, rotation);
            asteroid.size = Random.Range(asteroid.minSize, asteroid.maxSize);
            asteroid.SetTrajectory(rotation * spawnDirection);
        }
    }
}
