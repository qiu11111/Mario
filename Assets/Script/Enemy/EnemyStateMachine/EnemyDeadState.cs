using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeadState : IState
{
    private Enemy enemy;
    public EnemyDeadState(Enemy enemy)
    {
        this.enemy = enemy;
    }
    public void onEnter()
    {
        enemy.rd.gravityScale = 0f;
        enemy.GetComponent<BoxCollider2D>().enabled = false;
        enemy.anim.SetBool("dead", true);
    }

    public void onExit()
    {
        enemy.anim.SetBool("dead", false);
    }

    public void onFixedUpdate()
    {
        enemy.rd.velocity = Vector2.zero;
    }

    public void onUpdate()
    {
        
    }
    
}
