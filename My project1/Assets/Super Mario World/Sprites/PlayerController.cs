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

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Animator anim;

    private float hInput;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        if (speed <= 0)
        {
            speed = 5;
            Debug.Log("Speed was set incorrectly");
        }

        if (groundCheck == null)
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
        hInput = Input.GetAxis("Horizontal");

        if (!isGrounded)
        {
            if (rb.velocity.y <= 0)
            {
                isGrounded = CheckIfGrounded();
            }
        }
        else
        {
            isGrounded = CheckIfGrounded();
        }

        rb.velocity = new Vector2(hInput * speed, rb.velocity.y);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        if (hInput != 0)
        {
            sr.flipX = (hInput < 0);
        }

        anim.SetFloat("hInput", Mathf.Abs(hInput));
        anim.SetBool("isGrounded", isGrounded);
    }


    bool CheckIfGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, isGroundLayer);
    }
}
