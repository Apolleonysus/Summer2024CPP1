using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class RigidbodyCollider : MonoBehaviour
{
    [SerializeField, Range(1, 20)]
    private float speed = 5;
    [SerializeField, RangeAttribute(1, 20)]
    private float jumpForce = 10;

    private Trnasform groundCheck;

    Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

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
        float hInput = Input.GetAxis("Horizontal");

        rb.velocity = new Vector2(hInput * speed, rb.velocity.y);

        if(Input.GetButtonDown("Jump"))
        {
            rb.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
        }
    }
}
