using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerController", menuName = "InputController/PlayerController")]

public class PlayerController : InputController
{
    public override bool RetrieveJumpInput()
    {
        return Input.GetButtonDown("Jump");
    }

    public override bool RetrieveJumpHoldInput()
    {
        return Input.GetButton("Jump");
    }

    public override float RetrieveMoveInput()
    {
        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            //Debug.Log("Moving!");
        }

        return Input.GetAxisRaw("Horizontal");
    }

    public override bool RetrieveCrouchHoldInput()
    {
        if (Input.GetButton("Crouch"))
        {
            //Debug.Log("Crouching!");
        }

        return Input.GetButton("Crouch");
    }
}
