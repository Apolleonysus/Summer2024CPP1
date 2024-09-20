using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer), typeof(Animator))]
[RequireComponent(typeof(AudioSource))]
public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; }

    // Player gameplay variables
    private Coroutine jumpForceChange;
    private Coroutine speedChange;

    // Movement Variables
    [SerializeField, Range(1, 20)] private float speed = 5;
    [SerializeField, Range(1, 20)] private float jumpForce = 10;
    [SerializeField, Range(0.01f, 1)] private float groundCheckRadius = 0.02f;
    [SerializeField] private LayerMask isGroundLayer;

    // GroundCheck Stuff
    [SerializeField] private Transform groundCheck; // Changed to serialized field for assignment
    private bool isGrounded = false;

    // Component References
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Animator anim;
    private AudioSource audioSource;

    // Audio Clip references
    [SerializeField] private AudioClip jumpClip;
    [SerializeField] private AudioClip stompClip;
    [SerializeField] private AudioClip deathClip; // Sound when the player dies

    // AudioMixerChannel reference
    public AudioMixerGroup SFXGroup;

    // Respawn Variables
    public Transform respawnPoint; // Reference to the respawn point

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // Component References Filled
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        audioSource.outputAudioMixerGroup = SFXGroup;

        // Checking values to ensure non garbage data
        if (speed <= 0)
        {
            speed = 5;
            Debug.LogWarning("Speed was set incorrectly. Resetting to default.");
        }

        if (jumpForce <= 0)
        {
            jumpForce = 10;
            Debug.LogWarning("JumpForce was set incorrectly. Resetting to default.");
        }

        // Ensure groundCheck is assigned
        if (groundCheck == null)
        {
            Debug.LogError("GroundCheck transform is not assigned. Please assign it in the Inspector.");
        }

        // Find the respawn point by tag if it's not set in the Inspector
        if (respawnPoint == null)
        {
            GameObject respawnObject = GameObject.FindWithTag("RespawnPoint");
            if (respawnObject != null)
            {
                respawnPoint = respawnObject.transform;
                Debug.Log("Respawn point found at: " + respawnPoint.position);
            }
            else
            {
                Debug.LogError("No GameObject with tag 'respawnPoint' found. Please add one in the scene.");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale <= 0) return;

        AnimatorClipInfo[] curPlayingClips = anim.GetCurrentAnimatorClipInfo(0);
        float hInput = Input.GetAxis("Horizontal");

        // Create a small overlap collider to check if we are touching the ground
        IsGrounded();

        // Animation check for our physics
        if (curPlayingClips.Length > 0)
        {
            if (curPlayingClips[0].clip.name == "Attack")
            {
                if (isGrounded)
                    rb.velocity = Vector2.zero;
            }
            else
                rb.velocity = new Vector2(hInput * speed, rb.velocity.y);
        }

        // Button Input Checks
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            audioSource.PlayOneShot(jumpClip);
        }

        if (Input.GetButtonDown("Fire1"))
        {
            if (!isGrounded && curPlayingClips[0].clip.name != "JumpAttack")
            {
                anim.SetTrigger("isJumpAttacking");
            }
            else if (!curPlayingClips[0].clip.name.Contains("Attack"))
            {
                anim.SetTrigger("isAttacking");
            }
        }

        // Sprite Flipping
        if (hInput != 0) sr.flipX = (hInput < 0);

        anim.SetFloat("hInput", Mathf.Abs(hInput));
        anim.SetBool("isGrounded", isGrounded);
    }

    void IsGrounded()
    {
        // Check if player is grounded using OverlapCircle
        bool wasGrounded = isGrounded;
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, isGroundLayer);

        // Optionally debug information
        if (wasGrounded != isGrounded)
        {
            Debug.Log("Grounded state changed: " + isGrounded);
        }
    }

    void IncreaseGravity()
    {
        rb.gravityScale = 10;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            GameManager.Instance.lives--;

            if (GameManager.Instance.lives > 0)
            {
                RespawnPlayer();
            }
            else
            {
                PlayDeathSound();
                // Handle game over logic
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Squish"))
        {
            collision.gameObject.GetComponentInParent<Enemy>().TakeDamage(9999);
            rb.velocity = Vector2.zero;
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            audioSource.PlayOneShot(stompClip);
        }
    }

    // Play the death sound effect
    private void PlayDeathSound()
    {
        audioSource.PlayOneShot(deathClip); // Play death sound effect
    }

    // Respawn the player at the designated respawn point
    private void RespawnPlayer()
    {
        if (respawnPoint != null)
        {
            Debug.Log("Respawning player to: " + respawnPoint.position);
            transform.position = respawnPoint.position;
            rb.velocity = Vector2.zero; // Reset velocity after respawn
            Debug.Log("Player respawned.");
        }
        else
        {
            Debug.LogError("Respawn point not set! Make sure a respawn point is assigned or tagged 'respawnPoint'.");
        }
    }
}
