using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{

    [SerializeField] private InputController input = null;
    [SerializeField, Range(0f, 100f)] private float maxSpeed = 4f;
    [SerializeField] private Collider2D standingCollider;
    private float originalMaxSpeed;
    [SerializeField, Range(0f, 10f)] private float maxCrouchSpeed = 2f;
    [SerializeField, Range(0f, 100f)] private float maxAcceleration = 35f;
    [SerializeField, Range(0f, 100f)] private float maxAirAcceleration = 20f;

    private Vector2 direction;
    private Vector2 velocity;
    private Vector2 targetVelocity;
    private Rigidbody2D body;
    public GroundCheck groundCheck;
    //Noise Variable (Idle = 0, Crouch = 1, Walk = 2, Run = 3)
    public int noiseProduced;
    public bool isCrouching;

    private float maxSpeedChange;
    private float acceleration;
    private bool onGround;

    //ANIMATION
    private Animator animator;
    private AudioSource audioSource;
    
    // Start is called before the first frame update
    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        groundCheck = GetComponent<GroundCheck>();
        originalMaxSpeed = maxSpeed;
        animator = GetComponentInChildren<Animator>();
        animator.SetFloat("Speed", 0f);
        //GetComponentInChildren<AudioSource>().Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if (!standingCollider.enabled)
        {
            maxSpeed = maxCrouchSpeed;
            animator.SetBool("isCrouch", true);
           // Debug.Log("PRESSED CTRL");
        }
        else
        {
            maxSpeed = originalMaxSpeed;
            animator.SetBool("isCrouch", false);
        }

        direction.x = input.RetrieveMoveInput();
        if (direction == new Vector2(-1,0)) 
        {
            this.transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else 
        {
            this.transform.localScale = new Vector3(1f, 1f, 1f);
        }
        targetVelocity = new Vector2(direction.x, 0f) * Mathf.Max(maxSpeed - groundCheck.GetFriction(), 0f);

        //Noise

         if (Input.GetKeyDown(KeyCode.C))
            isCrouching = true;

         if (Input.GetKeyUp(KeyCode.C))
            isCrouching = false;

        if (isCrouching == true)
            noiseProduced = 1;
    }

    void FixedUpdate()
    {
        onGround = groundCheck.GetOnGround();
        velocity = body.velocity;
        acceleration = onGround ? maxAcceleration : maxAirAcceleration;
        maxSpeedChange = acceleration * Time.deltaTime;
        velocity.x = Mathf.MoveTowards(velocity.x, targetVelocity.x, maxSpeedChange);

        if (velocity.x > 0 || velocity.x < 0) 
        {
        animator.SetFloat("Speed", 0.5f);
        }

        else 
        {
        animator.SetFloat("Speed", 0f);
        GetComponentInChildren<AudioSource>().Play();
        }

        body.velocity = velocity;
    }


}
