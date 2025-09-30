using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    public float jumpForce = 20f;
    public float gravityModifier = 2f;
    private bool isOnGround = true;
    private bool isCrouching = false;

    private PlayerInputActions inputActions;
    private GameManager gameManager; // Σύνδεση με το GameManager
    private Animator playerAnim;
    public ParticleSystem explosionParticle;
    public ParticleSystem dirtParticle;
    public AudioClip jumpSound;
    public AudioClip deathSound;
    public AudioClip coinSound;
    private AudioSource playerAudio;
    private AudioSource audioSource;
    

    void Awake()
    {
        inputActions = new PlayerInputActions();
        inputActions.Player.Jump.performed += ctx => Jump();
        inputActions.Player.Crouch.started += ctx => CrouchStart();
        inputActions.Player.Crouch.canceled += ctx => CrouchEnd();
    }

    void OnEnable()
    {
        inputActions.Player.Enable();
    }

    void OnDisable()
    {
        if (inputActions != null)
        {
            inputActions.Player.Disable();
        }
    }


    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        playerAudio = GetComponent<AudioSource>();
        playerRb = GetComponent<Rigidbody>();
        Physics.gravity = new Vector3(0, -9.81f * gravityModifier, 0);
        gameManager = FindFirstObjectByType<GameManager>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        playerAnim = GetComponent<Animator>();
        if (explosionParticle != null)
        {
            explosionParticle.gameObject.SetActive(false); 
        }
    }

    void Jump()
    {
        if (isOnGround && !isCrouching)
        {
            playerAnim.SetTrigger("Jump_trig");
            playerRb.AddForce(Vector3.up * jumpForce* 2f, ForceMode.Impulse);
            isOnGround = false;
            if (jumpSound != null && playerAudio != null)
            {
                playerAudio.PlayOneShot(jumpSound);
                Debug.Log("Jump sound played!");

            }

            if (dirtParticle != null && dirtParticle.isPlaying)
            {
                dirtParticle.Stop();
            }
        }

    }


    void CrouchStart()
    {
        isCrouching = true;
        transform.localScale = new Vector3(1, 0.5f, 1);
    }

    void CrouchEnd()
    {
        isCrouching = false;
        transform.localScale = new Vector3(1, 1, 1);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            if (dirtParticle != null && !dirtParticle.isPlaying)
            {
                dirtParticle.Play();
            }
        }

        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("Crash! Explosion should play");

            if (explosionParticle != null)
            {
                explosionParticle.gameObject.SetActive(true);
                explosionParticle.Play();
            }

            if (dirtParticle != null && dirtParticle.isPlaying)
            {
                dirtParticle.Stop();
            }

            gameManager.GameOver();
            if (deathSound != null && playerAudio != null)
            {
                playerAudio.PlayOneShot(deathSound);
            }
            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 1);
        }


    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            gameManager.UpdateScore(5);
            Destroy(other.gameObject);

            if (coinSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(coinSound);
            }
        }
    }




}
