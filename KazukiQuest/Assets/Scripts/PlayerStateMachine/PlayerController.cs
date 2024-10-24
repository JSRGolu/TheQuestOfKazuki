using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float walkSpeed;
    [SerializeField] private float sprintMultiplier;
    [Header("Jumpping")]
    [SerializeField] private float jumpForce;
    [SerializeField] private float gravity;
    [Header("Mouse Sensitivity")]
    [SerializeField] private float mouseSensitivity;
    [SerializeField] private float upDownRange;
    [SerializeField] private bool invertYAxis;
    [Header("Dash Ability")]
    [SerializeField] private float dashMultiplier;
    [SerializeField] private float dashTime;
    [SerializeField] private float dashCooldown;

    private Vector3 currentMovement = Vector3.zero;
    private float verticalRotation;
    private float speed;
    private float nextDashTime;

    [HideInInspector] public InputHandler inputHandler;
    private BaseState currentState;
    [HideInInspector] public CharacterController characterController;
    private Camera mainCamera;

    private void Awake()
    {
        inputHandler = InputHandler.Instance;
        characterController = GetComponent<CharacterController>();
        mainCamera = Camera.main;
        currentState = new IdleState(this);
        currentState.Enter();
    }

    private void Update()
    {
        currentState.Update();
        HandleRotation();
        freefall();
    }

    public void ChangeState(BaseState newState)
    {
        currentState.Exit();
        currentState = newState;
        currentState.Enter();
    }

    public void HandleRotation()
    {
        float mouseXRotation = inputHandler.LookInput.x * mouseSensitivity;
        transform.Rotate(0, mouseXRotation, 0);
        verticalRotation -= inputHandler.LookInput.y * mouseSensitivity;
        verticalRotation = Mathf.Clamp(verticalRotation, -upDownRange, upDownRange);
        mainCamera.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);
    }

    public void HandleMovement()
    {
        speed = walkSpeed * (inputHandler.SprintValue > 0 ? sprintMultiplier : 1f);
        Vector3 inputDirection = new Vector3(inputHandler.MoveInput.x, 0f, inputHandler.MoveInput.y);
        Vector3 worldDirection = transform.forward * inputDirection.z + transform.right * inputDirection.x;
        worldDirection.Normalize();
        currentMovement.x = worldDirection.x * speed;
        currentMovement.z = worldDirection.z * speed;
        characterController.Move(currentMovement * Time.deltaTime);
    }

    public void StartJump()
    {
        currentMovement.y = jumpForce;
    }

    public void freefall()
    {
        currentMovement.y -= gravity * Time.deltaTime;
    }

    public void HandleSpeedDash()
    {
        if (Time.time > nextDashTime)
        {
            StartCoroutine(Dash());
            nextDashTime = Time.time + dashCooldown;
        }
    }

    IEnumerator Dash()
    {
        float startTime = Time.time;
        while (Time.time < startTime + dashTime)
        {
            characterController.Move(currentMovement * dashMultiplier * Time.deltaTime);
            yield return null;
        }
    }
}