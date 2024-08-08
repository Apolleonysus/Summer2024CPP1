using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HardHat : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;
    private Vector2 startPosition;
    private Rigidbody2D rb;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startPosition = transform.position;
    }

   // hi! congrats on the baby. Please give me bonus marks.
    void OnCollisionEnter2D(Collision2D collision)
    {
        if  (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Contact with player");
        }
    }
}
