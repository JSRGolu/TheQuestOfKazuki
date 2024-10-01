using UnityEngine;
using UnityEngine.InputSystem;
using KazukiQuest;
using UnityEngine.Rendering;

public class PlayerMovments : MonoBehaviour
{
    GameActionInputs inputActions;
    CharacterController characterController;

    private Vector2 adInputs;
    private bool jumpInput = false;

    public float straffeSpeed = 5f;
    public float forwardSpeed = 5f;
    public float maxDisplacement = 5f;

    private Vector3 velocity;
    public float jumpHeight = 2f;
    public float gravity = -9.8f;
    bool isGrounded;

    private void Awake()
    {
        inputActions = new GameActionInputs();
        characterController = GetComponent<CharacterController>();
    }

    private void OnEnable()
    {
        inputActions.Player.LeftRight.Enable();
        inputActions.Player.Jump.Enable();

        inputActions.Player.LeftRight.performed += OnLeftRightPerformed;
        inputActions.Player.LeftRight.canceled += OnLeftRightCancelled;

        inputActions.Player.Jump.performed += OnJumpPerformed;
        inputActions.Player.Jump.canceled += OnJumpCancelled;
    }

    private void OnDisable()
    {
        inputActions.Player.LeftRight.Disable();
        inputActions.Player.Jump.Disable();

        inputActions.Player.LeftRight.performed -= OnLeftRightPerformed;
        inputActions.Player.LeftRight.canceled -= OnLeftRightCancelled;

        inputActions.Player.Jump.performed -= OnJumpPerformed;
        inputActions.Player.Jump.canceled -= OnJumpCancelled;
    }

    private void OnLeftRightPerformed(InputAction.CallbackContext context)
    {
        adInputs = context.ReadValue<Vector2>();
    }

    private void OnLeftRightCancelled(InputAction.CallbackContext context)
    {
        adInputs = Vector2.zero;
    }

    private void OnJumpPerformed(InputAction.CallbackContext context)
    {
        jumpInput = true;
    }

    private void OnJumpCancelled(InputAction.CallbackContext context)
    {
        jumpInput = false;
    }

    private void Update()
    {
        GroundCheck();
        Move();
        Jump();
    }

    private void GroundCheck()
    {
        isGrounded = characterController.isGrounded;

        if (isGrounded)
        {
            velocity.y = -2f;
        }
    }

    private void Move()
    {
        Vector3 forwardDirection = Vector3.forward;
        characterController.Move(forwardDirection * forwardSpeed * Time.deltaTime);
        
        Vector3 movementDirection = new Vector3(adInputs.x, 0f, 0f).normalized;
        characterController.Move(movementDirection * straffeSpeed * Time.deltaTime);
    }

    private void Jump()
    {
        if (isGrounded && jumpInput)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        if (!isGrounded)
        {
            velocity.y += gravity * Time.deltaTime;
        }

        characterController.Move(velocity * Time.deltaTime);
    }
}
