using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolState : IState
{
    private Enemy enemy;

    public EnemyPatrolState(Enemy enemy)
    {
        this.enemy = enemy;
    }
    public void onEnter()
    {
        enemy.anim.SetBool("patrol", true);
    }

    public void onExit()
    {
        enemy.anim.SetBool("patrol", false);
    }

    public void onFixedUpdate()
    {
        enemy.move();
    }

    public void onUpdate()
    {
        if (enemy.wallCheck())
        {
            enemy.changeFaceDir();
        }
        if (enemy.enemyCheck())
        {
            enemy.changeFaceDir();
        }
        if (enemy.isDead)
        {
            enemy.tranState(EnemyStateType.Dead);
        }
        enemy.attackPlayer();
    }
}
