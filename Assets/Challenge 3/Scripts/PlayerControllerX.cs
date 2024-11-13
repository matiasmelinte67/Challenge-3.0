using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool gameOver = false;
    public float floatForce = 5f; // Force applied when floating up
    private float gravityModifier = 1.5f; // Gravity adjustment for better control
    private Rigidbody playerRb;

    public ParticleSystem explosionParticle; // Effect for game over
    public ParticleSystem fireworksParticle; // Effect for collecting items

    private AudioSource playerAudio;
    public AudioClip moneySound; // Sound for collecting diamonds
    public AudioClip explodeSound; // Sound for hitting rockets

    private bool isLowEnough = true; // Limit for floating height

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        Physics.gravity *= gravityModifier; // Adjust gravity
        playerAudio = GetComponent<AudioSource>();

        // Start with an initial upward force to lift the bird off the ground
        playerRb.AddForce(Vector3.up * 5, ForceMode.Impulse);
    }

    void Update()
    {
        // Float up while space is pressed, only if the bird is within bounds and the game isn't over
        if (Input.GetKey(KeyCode.Space) && isLowEnough && !gameOver)
        {
            playerRb.AddForce(Vector3.up * floatForce);
        }

        // Keep bird within upper boundary
        if (transform.position.y > 12)
        {
            isLowEnough = false;
        }
        else
        {
            isLowEnough = true;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Rocket"))
        {
            explosionParticle.Play();
            playerAudio.PlayOneShot(explodeSound, 1.0f);
            gameOver = true;
            Debug.Log("Game Over!");
            Destroy(other.gameObject); // Destroy the rocket upon collision
        }
        else if (other.gameObject.CompareTag("Diamond"))
        {
            fireworksParticle.Play();
            playerAudio.PlayOneShot(moneySound, 1.0f);
            Destroy(other.gameObject); // Destroy the diamond upon collection
        }
    }
}
