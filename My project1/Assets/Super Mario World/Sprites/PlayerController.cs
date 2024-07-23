using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class RigidbodyCollider : MonoBehaviour

{
    public float speed;

    Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float hInput = Input.GetAxis("Horizontal");

        rb.velocity = new Vector2(hInput * speed, rb.velocity.y);

        if(Input.GetButtonDown("Jump"))
        {
            rb.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
        }
    }
}
