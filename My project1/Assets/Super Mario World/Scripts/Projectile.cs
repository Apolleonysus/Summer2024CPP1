using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(Rigidbody2D))]
//public class Projectile : MonoBehaviour
//{
//    [SerializeField, Range(1, 50)] private float lifetime;

//    //speed values on the x and y axis - this is set by the shoot script
//    [HideInInspector]
//    public float xVel;
//    [HideInInspector]
//    public float yVel;


//    // Start is called before the first frame update
//    void Start()
//    {
//        if (lifetime <= 0) lifetime = 2.0f;

//        GetComponent<Rigidbody2D>().velocity = new Vector2(xVel, yVel);
//        Destroy(gameObject, lifetime);

//    }

//    private void OnTriggerEnter2D(Collider2D other)
//    {
//        if (other.gameObject.CompareTag("OneUp"))
//        Destroy(this.gameObject);
//    }

//    private void OnCollisionEnter2D(Collision2D collision)
//    {
//            Destroy(this.gameObject);
//    }
//}
[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
    [SerializeField, Range(1, 50)] private float lifetime;

    // Start is called before the first frame update
    void Start()
    {
        if (lifetime <= 0) lifetime = 2.0f;
        Destroy(gameObject, lifetime);
    }

    public void SetVelocity(float xVel, float yVel)
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(xVel, yVel);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
            Destroy(gameObject);

        if (collision.gameObject.CompareTag("Enemy") && CompareTag("PlayerProjectile"))
        {
            collision.gameObject.GetComponent<Enemy>().TakeDamage(10);
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Player") && CompareTag("EnemyProjectile"))
        {
            GameManager.Instance.lives--;
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("PlayerProjectile") && CompareTag("EnemyProjectile"))
        {
            Destroy(gameObject);
            Destroy(collision.gameObject);
        }
    }
}
//private void OnTriggerEnter2D(Collider2D other)
//{
//    if (other.gameObject.CompareTag("OneUp"))
//        Destroy(this.gameObject);
//}

//my code 
//private void OnCollisionEnter2D(Collision2D collision)
//{
//    Destroy(this.gameObject);
//}
//MY SCRIPT BEFORE UPDATING TO HISHAM'S SCRIPT
//if (collision.gameObject.CompareTag("Player"))
//{
//    Destroy(gameObject);
//}

//if collision.gameObject.CompareTag("Player") && CompareTag("EnemyProjectile"))
//    //do logic when player is hit by enemy projectile