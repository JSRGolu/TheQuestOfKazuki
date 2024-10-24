using UnityEngine;

public class JumpState : BaseState
{
    public JumpState(PlayerController playerController) : base(playerController) { }

    public override void Enter()
    {
        Debug.Log("Entered Jump State");
        playerController.StartJump();
    }

    public override void Exit()
    {
        Debug.Log("Exiting Jump State");
    }

    public override void Update()
    {
        playerController.HandleMovement();

        if (playerController.characterController.isGrounded)
        {
            if (playerController.inputHandler.MoveInput.x > 0 || playerController.inputHandler.MoveInput.y > 0)
                playerController.ChangeState(new WalkState(playerController));

            else if (playerController.inputHandler.dashTriggred)
                playerController.ChangeState(new DashState(playerController));

            else playerController.ChangeState(new IdleState(playerController));
        }
    }
}
