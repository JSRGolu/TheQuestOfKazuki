using UnityEngine;

public class DashState : BaseState
{
    public DashState(PlayerController playerController) : base(playerController) { }

    public override void Enter()
    {
        Debug.Log("Entered Dash State");
    }

    public override void Exit()
    {
        Debug.Log("Exiting Dash State");
    }

    public override void Update()
    {
        playerController.HandleSpeedDash();

        if (playerController.inputHandler.MoveInput.x > 0 || playerController.inputHandler.MoveInput.y > 0)
            playerController.ChangeState(new WalkState(playerController));

        else playerController.ChangeState(new IdleState(playerController));
    }
}
