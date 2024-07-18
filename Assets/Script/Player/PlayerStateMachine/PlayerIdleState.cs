using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : IState
{
    private Player player;

    public PlayerIdleState(Player player)
    {
        this.player = player;
    }
    public void onEnter()
    {
        player.rd.velocity = Vector2.zero;
        player.anim.SetBool("idle", true);
    }

    public void onExit()
    {
        player.anim.SetBool("idle", false);
    }

    public void onFixedUpdate()
    {
        
    }

    public void onUpdate()
    {
        if (player.isMove)
            player.tranState(PlayerStateType.Walk);
        if (player.isJump)
            player.tranState(PlayerStateType.JumpUp);
    }
}
