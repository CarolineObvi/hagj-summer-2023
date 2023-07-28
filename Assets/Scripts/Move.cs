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

    private float maxSpeedChange;
    private float acceleration;
    private bool onGround;
    
    // Start is called before the first frame update
    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        groundCheck = GetComponent<GroundCheck>();
        originalMaxSpeed = maxSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (!standingCollider.enabled)
        {
            maxSpeed = maxCrouchSpeed;
        }
        else
        {
            maxSpeed = originalMaxSpeed;
        }

        direction.x = input.RetrieveMoveInput();
        targetVelocity = new Vector2(direction.x, 0f) * Mathf.Max(maxSpeed - groundCheck.GetFriction(), 0f);
    }

    void FixedUpdate()
    {
        onGround = groundCheck.GetOnGround();
        velocity = body.velocity;

        acceleration = onGround ? maxAcceleration : maxAirAcceleration;
        maxSpeedChange = acceleration * Time.deltaTime;
        velocity.x = Mathf.MoveTowards(velocity.x, targetVelocity.x, maxSpeedChange);

        if (groundCheck.GetOnGround() && input.RetrieveMoveInput() == 0)
        {
            body.gravityScale = 0f;
        }
        else
        {
            body.constraints = RigidbodyConstraints2D.FreezeRotation;
        }

        body.velocity = velocity;
    }
}
