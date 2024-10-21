using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

//using UnityEditor.Experimental.GraphView;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;


public class playerJump : MonoBehaviour
{
    //audio
    public AudioSource dashSound;
    public AudioSource jumpSound;
    public AudioSource landSound;
    public AudioSource runSound;

    public Rigidbody2D rb;
    public Animator animator;
    public GameObject JumpSmoke;

    //groundcheck
    public BoxCollider2D groundCheck;
    public LayerMask groundMask;
    public bool isGrounded;

    //jump
    public float jumpStartTime;
    private float jumpTime;
    private bool isJumping;
    public float jumpForce;

    //dash
    private bool canDash = true;
    private bool isDashing;
    public float dashingPower;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 1f;

    //Sprite
    bool isFacingRight = true;

    //Xmovement
    float horizontalInput;
    float verticalInput;
    public float moveSpeed;
    public bool canRun;
    public bool isAttacking;
    float NewSpeed = 0f;

  



    // Start is called before the first frame update
    void Start()
    {
        canRun = true;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {

        if (isAttacking)
        {
            return;
        }
        
        if (isDashing)
        {
            return;
        }


        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        


        FlipSprite();
        Jump();
        
        if (Input.GetButtonDown("Dash") && canDash && isJumping==false)
        {

           
            StartCoroutine(Dash());
        }
    }

    void FixedUpdate()
    {
       
        //checks if player is dashing, if not returns to the following
        if (isDashing)
        {
            return;
        }
      
        CheckGround();
        playerWalk();
    }

    //jump script
    void Jump()
    {
        
        
        if (isGrounded == true && Input.GetButtonDown("Jump"))
        {
            GameObject newEnemy = Instantiate(JumpSmoke, transform.position, Quaternion.identity);
            
            animator.SetBool("isJumping", !isGrounded);
           jumpSound.Play();
            isJumping = true;
            jumpTime = jumpStartTime;
            rb.velocity = Vector2.up * jumpForce;

        }
        
        if (Input.GetButton("Jump") && isJumping == true)
        {

            if (jumpTime > 0)
            {
                rb.velocity = Vector2.up * jumpForce;
                jumpTime -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }

        if (Input.GetButtonUp("Jump"))
        {
            isJumping = false;
        }
    }

    //script to check if player is in contact with the ground so he can perform a jump
    void CheckGround()
    {
        
        isGrounded = Physics2D.OverlapAreaAll(groundCheck.bounds.min, groundCheck.bounds.max, groundMask).Length > 0;
        animator.SetBool("isJumping", !isGrounded);
        
        

    }
    
    private IEnumerator Dash()
    {
        //Vector2 direction = new Vector2(transform.localScale.x, verticalInput).normalized;
        Physics2D.IgnoreLayerCollision(6, 9, true);
        canDash = false;
        isDashing = true;
        animator.SetBool("isDashing", true);
        dashSound.Play();
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0.5f;
        rb.velocity = new Vector2 (transform.localScale.x * dashingPower, 0f);
        yield return new WaitForSeconds(dashingTime);
        rb.gravityScale = originalGravity;
        isDashing = false;
        animator.SetBool("isDashing", false);
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
        Physics2D.IgnoreLayerCollision(6, 9, false);

    }
    
    void FlipSprite()
    {
        if (isFacingRight && horizontalInput < 0f || !isFacingRight && horizontalInput > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 ls = transform.localScale;
            ls.x *= -1f;
            transform.localScale = ls;

            {

            }
        }



    }

    void CanRun()
    {
        canRun = true;    
    }

    void NoCanRun()
    {
        canRun = false;
    }
    
    void playerWalk()
    {
        if (canRun == true) 
        {
            
            rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);
            animator.SetFloat("xVelocity", Mathf.Abs(rb.velocity.x));
            animator.SetFloat("yVelocity", rb.velocity.y);
           

        }
        else
        {
            
            rb.velocity = new Vector2(horizontalInput * NewSpeed, rb.velocity.y);
        }

        

    }
    
    void isAttackingTrue()
    {
        isAttacking=true;
        
        
    }
    void isAttackingFalse()
    {
        isAttacking = false;


    }
    void hasLandedTrue()
    {
        landSound.Play();
    }
   void runningSound()
    {
        runSound.Play();
    }
}