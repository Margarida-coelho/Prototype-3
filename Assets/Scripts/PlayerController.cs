using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody playerRb;

    public float jumpForce;

    public float gravityMultiplier;

    public bool isOnGround = true;

    public bool gameOver = false;

    Animator playerAnim;

    public ParticleSystem explosionParticle;

    public ParticleSystem dirtParticle;

    public AudioClip jumpSound;

    public AudioClip crashSound;

    private AudioSource playerAudio;
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        Physics.gravity *= gravityMultiplier;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround && !gameOver)
        {
            Jump();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            dirtParticle.Play();
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            GameOver();
        }
    }

    void Jump()
    {
        playerAnim.SetTrigger("Jump_trig");
        playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        isOnGround = false;
        dirtParticle.Stop();
        playerAudio.PlayOneShot(jumpSound, 1f);
    }

    void GameOver()
    {
        gameOver = true;
        Debug.Log("Game Over!");
        playerAnim.SetBool("Death_b", true);
        playerAnim.SetInteger("DeathType_int", 1);
        explosionParticle.Play();
        dirtParticle.Stop();
        playerAudio.PlayOneShot(crashSound, 1f);
    }
}
