using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAirState : IState
{
    private Player player;
    public PlayerAirState(Player player)
    {
        this.player = player;
    }
    public void onEnter()
    {
        player.isAir = true;
        player.anim.SetBool("air", true);
    }

    public void onExit()
    {
        player.isAir = false;
        player.anim.SetBool("air", false);
    }

    public void onFixedUpdate()
    {
        player.jumpupMove();   
    }

    public void onUpdate()
    {
        if (player.groundCheck() && player.isJump)
            player.tranState(PlayerStateType.JumpUp);
        if (player.groundCheck())
            player.tranState(PlayerStateType.Idle);
        if (player.enemyCheck())
        {
            player.isJump = true;
            player.tranState(PlayerStateType.JumpUp);
            player.enemyCheck().collider.GetComponent<Enemy>().die();
        }


    }

    
}
