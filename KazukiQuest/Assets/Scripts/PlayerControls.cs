using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    private InputHandler inputHandler;
    private CharacterController characterController;

    [SerializeField] private float walkSpeed;
    [SerializeField] private float sprintMultiplier;
    [SerializeField] private float jumpForce;
    [SerializeField] private float gravity = 9.8f;
    [SerializeField] private float mouseSensitivity;
    [SerializeField] private float upDownRange;

    private Vector3 currentMovement;

    private void Awake()
    {
        inputHandler = GetComponent<InputHandler>();
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        HandleMovement();
        HandleRotation();
    }

    private void HandleMovement()
    {

    }

    private void HandleJump()
    {

    }

    private void HandleRotation()
    {

    }
}
