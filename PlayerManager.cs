using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    //Ref Variables
    InputManager inputManager;
    PlayerMovement playerMovement;


    private void Awake()
    {
        //Access to ref variable game components
        inputManager = GetComponent<InputManager>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        inputManager.HandleAllInputs();
    }

    //All things movement go here
    private void FixedUpdate()
    {
        playerMovement.HandleAllMovement();
    }
}    
