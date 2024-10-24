using UnityEngine;

public class WalkState : BaseState
{
    public WalkState(PlayerController playerController) : base(playerController) { }

    public override void Enter()
    {
        Debug.Log("Entered Walk State");
    }

    public override void Exit()
    {
        Debug.Log("Exiting Walk State");
    }

    public override void Update()
    {
        playerController.HandleMovement();

        if (playerController.inputHandler.JumpTriggred && playerController.characterController.isGrounded)
            playerController.ChangeState(new JumpState(playerController));

        else if (playerController.inputHandler.dashTriggred)
            playerController.ChangeState(new DashState(playerController));

        else if (playerController.inputHandler.MoveInput.x == 0 || playerController.inputHandler.MoveInput.y == 0)
            playerController.ChangeState(new IdleState(playerController));
    }
}
