using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpUpState : IState
{
    private Player player;
    public PlayerJumpUpState(Player player)
    {
        this.player = player;
    }
    public void onEnter()
    {
        player.rd.AddForce(Vector2.up * player.jumpForce,ForceMode2D.Impulse);
        //player.rd.velocity = new Vector2(player.rd.velocity.x, player.jumpForce);
        player.anim.SetBool("jump",true);
    }

    public void onExit()
    {
        player.isJump = false;
        player.anim.SetBool("jump", false);
    }

    public void onFixedUpdate()
    {
        player.jumpupMove();
    }

    public void onUpdate()
    {
        if (player.rd.velocity.y <= 0)
            player.tranState(PlayerStateType.Air);
        RaycastHit2D hit = player.brickCheck();
        if (hit.collider != null)
        {
            hit.collider.GetComponent<BrickController>().dis();
            player.rd.velocity = new Vector2(player.rd.velocity.x, 0);
        }
        if (player.enemyCheck())
        {
            player.isJump = true;
         player.enemyCheck().collider.GetComponent<Enemy>().die();
        }
        if (player.obj1Check())
        {
            player.obj1Check().collider.GetComponent<Obj1Controller>().use();
        }
    }
}
