using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalkState : IState
{
    private Player player;
    public PlayerWalkState(Player player)
    {
        this.player = player;
    }
    public void onEnter()
    {
        player.anim.SetBool("walk", true);
    }

    public void onExit()
    {
        player.anim.SetBool("walk", false);
    }

    public void onFixedUpdate()
    {
        player.move();
    }

    public void onUpdate()
    {
        if (!player.isMove)
            player.tranState(PlayerStateType.Idle);
        if (player.isJump)
            player.tranState(PlayerStateType.JumpUp);
        if (!player.groundCheck())
            player.tranState(PlayerStateType.Air);
    }
}
