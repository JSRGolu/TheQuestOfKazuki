using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    [SerializeField] private float walkSpeed;
    [SerializeField] private float sprintMultiplier;
    [SerializeField] private float jumpForce;
    [SerializeField] private float gravity = 9.8f;
    [SerializeField] private float mouseSensitivity;
    [SerializeField] private float upDownRange;
    [SerializeField] private bool invertYAxis;

    private Vector3 currentMovement = Vector3.zero;
    private float verticalRotation;

    private InputHandler inputHandler;
    private CharacterController characterController;
    private Camera mainCamera;

    private void Awake()
    {
        inputHandler = InputHandler.Instance;
        characterController = GetComponent<CharacterController>();
        mainCamera = Camera.main;
    }

    private void Update()
    {
        HandleMovement();
        HandleRotation();
    }

    private void HandleRotation()
    {
        float mouseXRotation = inputHandler.LookInput.x * mouseSensitivity;
        transform.Rotate(0, mouseXRotation, 0);
        verticalRotation -= inputHandler.LookInput.y * mouseSensitivity;
        verticalRotation = Mathf.Clamp(verticalRotation, -upDownRange, upDownRange);
        mainCamera.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);
    }

    private void HandleMovement()
    {
        float speed = walkSpeed * (inputHandler.SprintValue > 0 ? sprintMultiplier : 1f);
        Vector3 inputDirection = new Vector3(inputHandler.MoveInput.x, 0f, inputHandler.MoveInput.y);
        Vector3 worldDirection = transform.forward * inputDirection.z + transform.right * inputDirection.x;
        worldDirection.Normalize();
        currentMovement.x = worldDirection.x * speed;
        currentMovement.z = worldDirection.z * speed;
        HandleJump();
        characterController.Move(currentMovement * Time.deltaTime);
    }

    private void HandleJump()
    {
        if(characterController.isGrounded)
        {
            currentMovement.y = -0.5f;
            if(inputHandler.JumpTriggred)
            {
                currentMovement.y = jumpForce;
            }
        }
        else
        {
            currentMovement.y -= gravity * Time.deltaTime;
        }
    }
}
