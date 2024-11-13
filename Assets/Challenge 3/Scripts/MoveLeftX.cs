using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    public float speed = 5.0f;
    private PlayerController playerControllerScript;
    private float leftBound = -15;

    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();

        // Set faster speed for rockets
        if (gameObject.CompareTag("Rocket"))
        {
            speed = 10.0f;
        }
    }

    void Update()
    {
        // Move left if the game is not over
        if (!playerControllerScript.gameOver)
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }

        // Destroy objects that go off-screen to the left
        if (transform.position.x < leftBound)
        {
            Destroy(gameObject);
        }
    }
}
