using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    [SerializeField] private InputActionAsset playerControls;

    private InputAction moveAction;
    private InputAction lookAction;
    private InputAction sprintAction;
    private InputAction jumpAction;
    private InputAction dashAction;
    private InputAction glideAction;

    public Vector2 MoveInput { get; private set; }
    public Vector2 LookInput { get; private set; }
    public float SprintValue { get; private set; }
    public bool JumpTriggred { get; private set; }
    public bool dashTriggred { get; private set; }
    public float glideInput { get; private set; }

    public static InputHandler Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        moveAction = playerControls.FindActionMap("Player").FindAction("Move");
        lookAction = playerControls.FindActionMap("Player").FindAction("Look");
        sprintAction = playerControls.FindActionMap("Player").FindAction("Sprint");
        jumpAction = playerControls.FindActionMap("Player").FindAction("Jump");
        dashAction = playerControls.FindActionMap("Player").FindAction("Dash");
        glideAction = playerControls.FindActionMap("Player").FindAction("Glide");

        RegisterInputAction();
    }

    private void RegisterInputAction()
    {
        moveAction.performed += context => MoveInput = context.ReadValue<Vector2>();
        moveAction.canceled += context => MoveInput = Vector2.zero;

        lookAction.performed += context => LookInput = context.ReadValue<Vector2>();
        lookAction.canceled += context => LookInput = Vector2.zero;

        sprintAction.performed += context => SprintValue = context.ReadValue<float>();
        sprintAction.canceled += context => SprintValue = 0f;

        jumpAction.performed += context => JumpTriggred = true;
        jumpAction.canceled += context => JumpTriggred = false;

        dashAction.performed += context => dashTriggred = true;
        dashAction.canceled += context => dashTriggred = false;

        glideAction.performed += context => glideInput = context.ReadValue<float>();
        glideAction.canceled += context => glideInput = 0f;
    }

    private void OnEnable()
    {
        moveAction.Enable();
        lookAction.Enable();
        sprintAction.Enable();
        jumpAction.Enable();
        dashAction.Enable();
        glideAction.Enable();
    }

    private void OnDisable()
    {
        moveAction.Disable();
        lookAction.Disable();
        sprintAction.Disable();
        jumpAction.Disable();
        dashAction.Disable();
        glideAction.Disable();
    }
}
