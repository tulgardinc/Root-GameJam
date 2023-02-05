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
    [SerializeField] float climbJumpVert;
    [SerializeField] float climbJumpHorz;
    [SerializeField] LayerMask altLayer;

    [SerializeField] Animator animatorCharacter;



    float input;
    bool facingRight = true;
    bool isGrounded = false;
    [HideInInspector] public bool isClimbing = false;
    bool isOnTri = false;
    bool isOnCol = false;
    float tempInt;
    BoxCollider2D test;
    GameObject climbCandidate;
    int currentExtraJumpCount;

    void Start()
    {
        currentExtraJumpCount = extraJumpCount;
        test = GetComponent<BoxCollider2D>();
    }

    private void CheckIfCanClimb()
    {
        var climbCol = Physics2D.OverlapCircle(transform.position, 1.5f, LayerMask.GetMask("RootVertical"));
        climbCandidate = climbCol ? climbCol.gameObject : null;
    }

    void Update()
    {
        CheckIfCanClimb();

        input = Input.GetAxisRaw("Horizontal");

        if (Input.GetAxis("Horizontal") != 0)
        {
            animatorCharacter.SetBool("isWalking", true);
        }
        else
        {
            animatorCharacter.SetBool("isWalking", false);
        }

        if (isGrounded)
        {
            currentExtraJumpCount = extraJumpCount;
            animatorCharacter.SetBool("isJumping", false);
        }

        if (Input.GetButtonDown("Jump"))
        {
            animatorCharacter.SetBool("isJumping", true);
            if (!isClimbing)
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
            else
            {
                isClimbing = false;
                rb.AddForce(new Vector2(input * climbJumpHorz, climbJumpVert));
            }
        }
        if (!animatorCharacter.GetBool("isPicked") && climbCandidate != null)
        {
            if (Input.GetKeyDown(KeyCode.E) && !isClimbing)
            {
                rb.gravityScale = 0;
                isClimbing = true;
                rb.velocity = Vector3.zero;

            }
            else if (Input.GetKeyDown(KeyCode.E) && isClimbing)
            {
                rb.gravityScale = 1;
                isClimbing = false;

            }
            if (isClimbing)
            {
                transform.position = new Vector2(climbCandidate.transform.position.x, transform.position.y);
                Climb();
                animatorCharacter.SetBool("isClimbing", true);
            }
            else
            {
                animatorCharacter.SetBool("isClimbing", false);
            }


        }
        if (isOnCol)
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                test.isTrigger = true;
            }
        }

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
    }

    void FixedUpdate()
    {
        // Extra falling speed
        if (!isGrounded && !isClimbing)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y - fallSpeed);
        }
        rb.velocity = new Vector2(input * speed * Time.deltaTime, rb.velocity.y);

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
        if (collision.transform.tag.Equals("standable"))
        {
            isOnCol = true;

        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        isOnCol = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Enter");
        isOnTri = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("Exit");
        isOnTri = false;
        if (collision.transform.tag.Equals("climbable"))
        {
            rb.gravityScale = 1;
            isClimbing = false;
            animatorCharacter.SetBool("isClimbing", false);

        }

        if (collision.transform.tag.Equals("standable"))
        {
            isOnCol = false;
            test.isTrigger = false;

        }

    }



    void Climb()
    {
        if (Input.GetKey(KeyCode.W))
        {
            this.transform.position = new Vector2(transform.position.x, transform.position.y + climbSpeed * Time.deltaTime);

            //rb.velocity = new vector2(rb.velocity.x , rb.velocity.y + climbspeed);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            this.transform.position = new Vector2(transform.position.x, transform.position.y - climbSpeed * Time.deltaTime);

            //rb.velocity = new vector2(rb.velocity.x , rb.velocity.y + climbspeed);
        }


    }
}
