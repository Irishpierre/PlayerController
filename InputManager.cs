using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{

    //Ref to PlayerControls.cs
    PlayerControls playerControls;

    //Side note: vector2 used to store information on two axis
    public Vector2 movementInput;
    public float verticalInput;
    public float horizontalInput;

    private void OnEnable()
    {
        //If player controls aren't filled in, set up default
        if(playerControls == null)
        {
            playerControls = new PlayerControls();
            //Record movement (left right up down)
            playerControls.PlayerMovement.Movement.performed += i => movementInput = i.ReadValue<Vector2>();
        }

        playerControls.Enable();
    }

    //Disable player controls on disable.
    private void OnDisable()
    {
        playerControls.Disable();
    }


    public void HandleAllInputs()
    {
        HandleMovementInput();
        //Handle Jump
        //Handle Sprint
        //Handle Action
        //etc...
    }


    private void HandleMovementInput()
    {
        verticalInput = movementInput.y;
        horizontalInput = movementInput.x;
    }
}
