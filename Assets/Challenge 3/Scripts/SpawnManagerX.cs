using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject diamondPrefab;
    public GameObject rocketPrefab;
    private float spawnDelay = 2.0f;
    private float spawnInterval = 1.5f;

    private PlayerController playerControllerScript;

    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();

        // Start spawning objects at regular intervals
        InvokeRepeating("SpawnObjects", spawnDelay, spawnInterval);
    }

    void SpawnObjects()
    {
        // Spawn objects only if the game is active
        if (!playerControllerScript.gameOver)
        {
            // Choose randomly between spawning a diamond or rocket
            GameObject objectToSpawn = (Random.value > 0.5f) ? diamondPrefab : rocketPrefab;
            Vector3 spawnLocation = new Vector3(30, Random.Range(3, 10), 0);

            Instantiate(objectToSpawn, spawnLocation, objectToSpawn.transform.rotation);
        }
    }
}
