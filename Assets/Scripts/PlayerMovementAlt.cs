using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
public class PlayerMovementAlt : MonoBehaviour
{

    float horizontalInput;
    float moveSpeed = 2;
    bool isFacingRight = true;
    public float jumpPower;
    


    private bool canDash = true;
    private bool isDashing;
    private float dashingPower = 7f;
    private float dashingTime = 0.1f;
    private float dashingCooldown = 1f;

    public float jumpStartTime;
    private float jumpTime;
    private bool isPerformingJump;


    public bool isGrounded;

    public BoxCollider2D groundCheck;

    public LayerMask groundMask;



    Rigidbody2D rb;
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if( isDashing)
        {
            return;
        }

        horizontalInput = Input.GetAxis("Horizontal");

        jump();

        FlipSprite();

        

        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {

            StartCoroutine(Dash());
        }
    }

    private void FixedUpdate()
    {
        CheckGround();
        if (isDashing)
        {
            return;
        }
        rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);
        animator.SetFloat("xVelocity", Mathf.Abs(rb.velocity.x));
        animator.SetFloat("yVelocity", rb.velocity.y);

        

        
    }

    void FlipSprite()
    {
        if(isFacingRight && horizontalInput < 0f  || !isFacingRight && horizontalInput > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 ls = transform.localScale;
            ls.x *= -1f;
            transform.localScale = ls;
            
            {
                
            }
        }

        

    }
    

    void CheckGround()
    {

        isGrounded = Physics2D.OverlapAreaAll(groundCheck.bounds.min, groundCheck.bounds.max, groundMask).Length > 0;
        animator.SetBool("isJumping", !isGrounded);

    }

    private IEnumerator Dash()
    {

        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        yield return new WaitForSeconds(dashingTime);
        rb.gravityScale = originalGravity;
        isDashing=false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }

    void jump()
    {
        if (isGrounded == true && Input.GetButtonDown("Jump"))
        {
            isPerformingJump = true;
            jumpTime = jumpStartTime;
            rb.velocity = Vector2.up * jumpPower;
            animator.SetBool("isJumping", !isGrounded);
        }

        if (Input.GetButton("Jump") && isPerformingJump == true)
        {
            if (jumpTime > 0)
            {
                rb.velocity = Vector2.up * jumpPower;
                jumpTime -= Time.deltaTime;
            }
            else
            {
                isPerformingJump = false;
            }
        }

        if (Input.GetButtonUp("Jump"))
        {
            isPerformingJump = false;
        }
    }

}

