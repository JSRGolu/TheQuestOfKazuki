using UnityEngine;

public class IdleState : BaseState
{
    public IdleState(PlayerController playerController) : base(playerController) { }

    public override void Enter()
    {
        Debug.Log("Entered Idle State");
    }

    public override void Exit()
    {
        Debug.Log("Exiting Idle State");
    }

    public override void Update()
    {
        if(playerController.inputHandler.MoveInput.x != 0 || playerController.inputHandler.MoveInput.y != 0)
            playerController.ChangeState(new WalkState(playerController));

        else if (playerController.inputHandler.JumpTriggred)
            playerController.ChangeState(new JumpState(playerController));
    }
}
