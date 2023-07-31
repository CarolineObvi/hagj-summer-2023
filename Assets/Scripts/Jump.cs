using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{

    [SerializeField] private InputController input = null;
    [SerializeField, Range(0f, 10f)] private float jumpHeight = 3f;
    [SerializeField, Range(0, 5)] private int maxAirJumps = 0;
    [SerializeField, Range(0f, 5f)] private float downwardMovementMultiplier = 3f;
    [SerializeField, Range(0f, 5f)] private float upwardMovementMultiplier = 1.7f;

    [SerializeField] private Collider2D standingCollider;

    private Rigidbody2D body;
    private GroundCheck groundCheck;
    private Vector2 velocity;

    private int jumpPhase;
    private float defaultGravityScale;

    private bool desiredJump;
    private bool onGround;
    
    //ANIMATION
    private Animator animator;

    // Start is called before the first frame update
    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        groundCheck = GetComponent<GroundCheck>();

        defaultGravityScale = 1f;

        animator = GetComponentInChildren<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        desiredJump |= input.RetrieveJumpInput();
    }

    void FixedUpdate()
    {
        onGround = groundCheck.GetOnGround();
        velocity = body.velocity;

        if (onGround)
        {   
            animator.SetBool("isJump", false);
            body.gravityScale = 0;
            jumpPhase = 0;
        }

        if (!standingCollider.enabled)
        {
            desiredJump = false;
            
        }

        if (desiredJump)
        {
            animator.SetBool("isJump", true);
            desiredJump = false;
            JumpAction();
        }

        if (input.RetrieveJumpHoldInput() && body.velocity.y > 0)
        {
            
            body.gravityScale = upwardMovementMultiplier;
            animator.SetBool("isJump", true);
        }

        else if ((!input.RetrieveJumpHoldInput() || body.velocity.y < 0) && !onGround)
        {
            body.gravityScale = downwardMovementMultiplier;
            animator.SetBool("isJump", false);
        }

        /*else if (body.velocity.y == 0)
        {
            body.gravityScale = defaultGravityScale;
        }*/

        body.velocity = velocity;
    }

    private void JumpAction()
    {
        if(onGround || jumpPhase < maxAirJumps)
        {   
            jumpPhase += 1;
            float jumpSpeed = Mathf.Sqrt(-2f * Physics2D.gravity.y * jumpHeight);

            if (velocity.y > 0f)
            {
                jumpSpeed = Mathf.Max(jumpSpeed - velocity.y, 0f);
            }

            velocity.y += jumpSpeed;
            
        }
    }
}
