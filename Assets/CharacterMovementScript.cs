using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovementScript : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Rigidbody2D rb;

    [Header("Movement")]
    [SerializeField] float speed;
    [SerializeField] float jumpForce;
    [SerializeField] float fallSpeed;

    [Header("Ground Check")]
    [SerializeField] float checkRadius;
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask whatIsGround;

    [Header("Special")]
    [SerializeField] int extraJumpCount;
    [SerializeField] float climbSpeed;


    float input;
    bool facingRight = true;
    bool isGrounded = false;
    bool isClimbing = false;
    int currentExtraJumpCount;



    void Start()
    {
        currentExtraJumpCount = extraJumpCount;
    }

    void Update()
    {
        input = Input.GetAxisRaw("Horizontal");

        if (isGrounded)
        {
            currentExtraJumpCount = extraJumpCount;
        }

        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded || currentExtraJumpCount > 0)
            {
                Jump();
            }
            else if (currentExtraJumpCount > 0)
            {
                currentExtraJumpCount--;
                Jump();
            }
        }
    }

    void FixedUpdate()
    {

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
        
        // Extra falling speed
        if (!isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y - fallSpeed);
        }

        rb.velocity = new Vector2(input * speed, rb.velocity.y);

        if (input != 0 && facingRight != input > 0)
        {
            Flip();
        }



    }

    void Jump()
    {
        rb.velocity = Vector2.up * jumpForce;
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 Scaler = this.transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.tag.Equals("climbable"))
        {
            Climb();

        }
    }

    void Climb()
    {
        if(Input.GetKey(KeyCode.E))
        {
            this.transform.position = new Vector2(transform.position.x, transform.position.y + climbSpeed);
            rb.bodyType = RigidbodyType2D.Static;
            //rb.velocity = new vector2(rb.velocity.x , rb.velocity.y + climbspeed);
        }
        else
        {
            rb.bodyType = RigidbodyType2D.Dynamic;

        }


    }
}