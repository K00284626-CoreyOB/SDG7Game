using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] public int speed = 3;
    public float jumpForce = 5f;

    [Header("Mobile Controls")]
    public Joystick joystick;

    private Rigidbody2D rb;
    private bool isGrounded = false;
    private SpriteRenderer sr;

    public bool panelOpen = false;

    public int turbinesFixed = 0;

    public TextMeshProUGUI turbine1Text;
    public TextMeshProUGUI turbine2Text;

    public Animator playerAnimator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Keyboard + joystick movement
        float h = Input.GetAxis("Horizontal");

        if (joystick != null && Mathf.Abs(joystick.Horizontal) > 0f)
        {
            h = joystick.Horizontal;
            playerAnimator.SetBool("IsMoving", true);
        }
        else if(Mathf.Abs(joystick.Horizontal) <= 0f)
        {
            playerAnimator.SetBool("IsMoving", false);
        }

            rb.velocity = new Vector2(h * speed, rb.velocity.y);

        // Flip sprite based on movement direction
        if (h > 0)
        {
            sr.flipX = false;
        }
        else if (h < 0)
        {
            sr.flipX = true;
        }

        // Jump (keyboard for now)
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

    public void MobileJump()
    {
        if (isGrounded)
        {
            Jump();
        }
    }

    public void OpenPanel()
    {
       panelOpen = true;
    }

    public void ClosePanel()
    {
        panelOpen = false;
    }

    public bool PanelStatus()
    {
        return panelOpen;
    }

    public void FixTurbine()
    {
        turbinesFixed++;

        switch (turbinesFixed)
        {
            case 1:
                turbine1Text.gameObject.SetActive(true);
                break;
            case 2:
                turbine1Text.gameObject.SetActive(false);
                turbine2Text.gameObject.SetActive(true);
                break;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isGrounded = true;
    }
}