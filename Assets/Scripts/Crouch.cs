using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crouch : MonoBehaviour
{
    [SerializeField] private InputController input = null;
    [SerializeField] private Collider2D standingCollider;
    [SerializeField] private Transform overheadCheckCollider;
    [SerializeField] private LayerMask obstacleLayer;
    private Move move;
    const float overheadCheckRadius = 0.5f;
    private bool desiredCrouch;

    // Start is called before the first frame update
    void Start()
    {
        move = GetComponent<Move>();
    }

    // Update is called once per frame
    void Update()
    {
        desiredCrouch |= input.RetrieveCrouchHoldInput();
        
    }

    void FixedUpdate()
    {
        if (input.RetrieveCrouchHoldInput() && move.groundCheck.GetOnGround())
        {
            standingCollider.enabled = false;
        }
        else if (!Physics2D.OverlapCircle(overheadCheckCollider.position, overheadCheckRadius, obstacleLayer) || !move.groundCheck.GetOnGround())
        {
            standingCollider.enabled = true;
        }
        //standingCollider.enabled = !input.RetrieveCrouchHoldInput();
    }
}
