using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crouch : MonoBehaviour
{
    [SerializeField] private InputController input = null;
    [SerializeField] private Collider2D standingCollider;
    [SerializeField] private Move move;
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
        standingCollider.enabled = !input.RetrieveCrouchHoldInput();
    }
}
