using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer))]
public class RigidbodyCollider : MonoBehaviour
{
    [SerializeField, Range(1, 20)]
    private float speed = 5;
    [SerializeField, RangeAttribute(1, 20)]
    private float jumpForce = 10;
    [SerializeField, Range(0.01f, 1)]
    private float groundCheckRadius = 0.02f;
    [SerializeField] private LayerMask isGroundLayer;


    private Transform groundCheck;
    private bool isGrounded = false;

    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator anim;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        if (speed <= 0)
        {
            speed = 5;
            Debug.Log("Speed was set incorrectly");
        }


        if (!groundCheck)
        {
            GameObject obj = new GameObject();
            obj.transform.SetParent(transform);
            obj.transform.localPosition = Vector3.zero;
            obj.name = "GroundCheck";
            groundCheck = obj.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, isGroundLayer);
        
        float hInput = Input.GetAxis("Horizontal");


        rb.velocity = new Vector2(hInput * speed, rb.velocity.y);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        if (hInput != 0) sr.flipX = (hInput < 0);
    }

}
