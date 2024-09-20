using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    private SpriteRenderer playerSpriteRenderer;  // Reference to the player's SpriteRenderer

    [Range(0, 10)]
    public float xVel;
    [Range(0, 10)]
    public float yVel;

    public Transform spawnPointLeft;
    public Transform spawnPointRight;

    public Projectile projectilePrefab;

    // Start is called before the first frame update
    void Start()
    {
        // Find the SpriteRenderer component in the parent (Player) GameObject
        playerSpriteRenderer = GetComponentInParent<SpriteRenderer>();

        if (xVel == 0 && yVel == 0)
            xVel = 7.0f;

        if (!spawnPointLeft || !spawnPointRight || !projectilePrefab)
            Debug.LogWarning("Please set default values on the shoot script for object " + gameObject.name);
    }

    public void Fire()
    {
        // Debug to check if Fire is being called
        Debug.Log("Fire method called!");

        if (!projectilePrefab)
        {
            Debug.LogError("Projectile prefab is not assigned!");
            return;
        }

        if (!playerSpriteRenderer.flipX)
        {
            if (!spawnPointRight)
            {
                Debug.LogError("SpawnPointRight not assigned!");
                return;
            }

            Projectile curProjectile = Instantiate(projectilePrefab, spawnPointRight.position, Quaternion.identity);
            curProjectile.SetVelocity(xVel, yVel);
            Debug.Log("Projectile spawned from the right!");
        }
        else
        {
            if (!spawnPointLeft)
            {
                Debug.LogError("SpawnPointLeft not assigned!");
                return;
            }

            Projectile curProjectile = Instantiate(projectilePrefab, spawnPointLeft.position, Quaternion.identity);
            curProjectile.SetVelocity(-xVel, yVel); // Negative xVel for left direction
            Debug.Log("Projectile spawned from the left!");
        }
    }
}
