using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Ref InputManager
    InputManager inputManager;

    //Ref variables
    Vector3 moveDirection;
    Transform cameraObject;
    Rigidbody playerRb;

    public float movementSpeed = 7.0f;
    public float rotationSpeed = 15.0f;

    private void Awake()
    {
        inputManager = GetComponent<InputManager>();
        playerRb = GetComponent<Rigidbody>();
        cameraObject = Camera.main.transform;
    }



    public void HandleAllMovement()
    {
        HandleMovement();
        HandleRotation();
    }
    
    
    //Move
    private void HandleMovement()
    {
        //Camera control with our movement
        moveDirection = cameraObject.forward * inputManager.verticalInput;
        moveDirection = moveDirection + cameraObject.right * inputManager.horizontalInput;
        moveDirection.Normalize(); //Round to 1
        moveDirection.y = 0; //Stops us walking into the air
        moveDirection = moveDirection * movementSpeed;


        Vector3 movementVelocity = moveDirection;
        playerRb.velocity = movementVelocity; //move our player based on the equation we just made
    }

    //Rotate

    private void HandleRotation()
    {
        Vector3 targetDirection = Vector3.zero;
        //Always face the direction you're about to run
        targetDirection = cameraObject.forward * inputManager.verticalInput;
        targetDirection = targetDirection + cameraObject.right * inputManager.horizontalInput;
        targetDirection.Normalize();
        targetDirection.y = 0;

        //Stops snap rotate
        if (targetDirection == Vector3.zero)
            targetDirection = transform.forward;

        //Calculate rotations
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        //Rotation point between A and B
        Quaternion playerRotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        transform.rotation = playerRotation;
    }

}
