using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] public int speed = 3;
    public float jumpForce = 5f;

    private Rigidbody2D rb;
    private bool isGrounded = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Horizontal movement using physics (keeps collisions)
        float h = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(h * speed, rb.velocity.y);

        // Flip sprite based on movement direction
        if (h > 0)  // moving right
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (h < 0) // moving left
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }


        // Jump
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }
    }

    void Jump()
    {
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        isGrounded = false;
    }

    // Simple ground check
    private void OnCollisionEnter2D(Collision2D collision)
    {
        isGrounded = true;
    }
}
