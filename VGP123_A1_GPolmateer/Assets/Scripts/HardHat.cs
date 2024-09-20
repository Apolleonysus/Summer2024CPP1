using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HardHat : MonoBehaviour
{
    [SerializeField] private float pokeUpDuration = 2f; // How long the head stays up
    [SerializeField] private float pokeDownDuration = 2f; // How long the head stays down
    [SerializeField] private GameObject projectilePrefab; // Reference to the projectile
    [SerializeField] private Transform firePoint; // Where the projectile spawns
    [SerializeField] private float projectileSpeed = 5f; // Speed of the projectile
    [SerializeField] private float shootDelay = 30f; // Time between shots when head is up

    private Animator animator;
    private bool isPokingUp = false;
    private bool canShoot = false;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        StartCoroutine(PokeHeadRoutine()); 
    }

    // Coroutine to handle head poking up and down
    private IEnumerator PokeHeadRoutine()
    {
        while (true)
        {
            // Head goes up
            isPokingUp = true;
            animator.SetBool("IsPokingUp", true); // Trigger animation for poking up
            canShoot = true; // Start shooting
            yield return new WaitForSeconds(pokeUpDuration);

            // Head goes down
            isPokingUp = false;
            animator.SetBool("IsPokingUp", false); // Trigger animation for poking down
            canShoot = false; // Stop shooting
            yield return new WaitForSeconds(pokeDownDuration);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Contact with player");
        }
    }
}
