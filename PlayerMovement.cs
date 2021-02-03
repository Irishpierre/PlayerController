using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Ref to InputHandler script as variable
    private InputHandler _input;
    //MoveSpeed variable
    [SerializeField] private float moveSpeed;
    //Reference to our camera...Helps in Isometric games
    [SerializeField] private Camera _camera;
    //Rotate speed field
    [SerializeField] private float rotateSpeed;
    //Rotate Toward Mouse check
    [SerializeField] private bool rotateTowardMouse;

    private void Awake()
    {
        //Allowed access to component InputHandler
        _input = GetComponent<InputHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        var targetVector = new Vector3(_input.InputVector.x, 0, _input.InputVector.y);

        //Move in the direction we're aiming
        var movementVector = MoveTowardTarget(targetVector);


        //Rotate in direction we're traveling
        RotateTowardMovementVector(movementVector);

        if(!rotateTowardMouse)
        {
            RotateTowardMovementVector(movementVector);
        }
        else
        {
            RotateTowardMouseVector();
        }

    }

    private void RotateTowardMouseVector()
    {
        //Raycast to location of mouse arrow for ref
        Ray ray = _camera.ScreenPointToRay(_input.MousePosition);

        //Know when ray hits
        if (Physics.Raycast(ray, out RaycastHit hitInfo, maxDistance: 300f))
        {
            var target = hitInfo.point;
            target.y = transform.position.y; //This stops us from accidently looking up
            transform.LookAt(target);
        }

        
    }

    private void RotateTowardMovementVector(Vector3 movementVector)
    {

        //Check so we don't rotate back to 0,0 when key isnt pressed

        if(movementVector.magnitude == 0) { return; }

        //Method Quaternion to local rotate. Creates rotation based off of rotate from inputs relative to camera
        var rotation = Quaternion.LookRotation(movementVector);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, rotateSpeed);
    }

    private Vector3 MoveTowardTarget(Vector3 targetVector)
    {
        //Define our speed. Time.deltaTime helps smooth movement
        var speed = moveSpeed * Time.deltaTime;

        //Movement and rotation seperate from camera rotate
        //Use Eueler Angles (3D angles in X, Y Z) with Quaternion (imaginary angles)
        targetVector = Quaternion.Euler(0, _camera.gameObject.transform.eulerAngles.y, 0) * targetVector;

        //Target position
        var targetPos = transform.position + targetVector * speed;
        transform.position = targetPos;
        return targetVector;
    }
    
}
