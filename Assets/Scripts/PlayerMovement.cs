using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public Rigidbody2D rb2D;

   

    public float airSpeed;

    public float groundSpeed;
    
    [Range(0f, 1f)]
    public float groundDecay;

    public bool grounded;

    public BoxCollider2D groundCheck;

    public LayerMask groundMask;

    float xInput;
    
    float yInput;

    public float acceleration;
   

    // Start is called before the first frame update
    void Start()
    {
        
    }

    
    void Update()
    {

        CheckInput();

        

        HandleJump();

    }


    void MoveWithInput()
    {
        if (Mathf.Abs(xInput) > 0)
        {
            float increment = xInput * acceleration;
            float newSpeed = Mathf.Clamp(rb2D.velocity.x + increment, -groundSpeed , groundSpeed );
            rb2D.velocity = new Vector2(newSpeed, rb2D.velocity.y);

            float direction = Mathf.Sign(xInput);
            transform.localScale = new Vector3(direction, 1, 1);
            
          
        }
        
    }


    void HandleJump() 
    {
        if (Input.GetButtonDown("Jump") && grounded)
        {
            rb2D.velocity = new Vector2(rb2D.velocity.x, airSpeed);

          
        }
    }
   
    void CheckInput()
    {
        xInput = Input.GetAxis("Horizontal");
        yInput = Input.GetAxis("Vertical");
        
    }


   void FixedUpdate()
    {

        CheckGround();
        ApplyFriction();
        MoveWithInput();

        

    }

    //Tell if Grounded or not
    void CheckGround()
    {

        grounded = Physics2D.OverlapAreaAll(groundCheck.bounds.min, groundCheck.bounds.max, groundMask).Length > 0;
        
    }

    void ApplyFriction()
    {
        if (grounded && xInput == 0 && rb2D.velocity.y <= 0)
        {

            rb2D.velocity *= groundDecay;
        }

    }

}
